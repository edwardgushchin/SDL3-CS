#!/usr/bin/env python3
"""Exercise shared-library substitution and Apple archive relinking."""

import ctypes
import hashlib
import os
import shutil
import subprocess
import sys
import tempfile
from pathlib import Path


ROOT = Path(__file__).resolve().parents[2] / "SDL3-CS.NativePackages"
APPLE = (
    ("iOS", "ios-arm64", "arm64-apple-ios", "arm64", "ios"),
    ("iOS", "iossimulator-arm64", "arm64-apple-ios-simulator", "arm64", "ios-simulator"),
    ("iOS", "iossimulator-x64", "x86_64-apple-ios-simulator", "x86_64", "ios-simulator"),
    ("tvOS", "tvos-arm64", "arm64-apple-tvos", "arm64", "tvos"),
    ("tvOS", "tvossimulator-arm64", "arm64-apple-tvos-simulator", "arm64", "tvos-simulator"),
    ("tvOS", "tvossimulator-x64", "x86_64-apple-tvos-simulator", "x86_64", "tvos-simulator"),
)


def run(*args: str) -> None:
    result = subprocess.run(args, capture_output=True, text=True)
    if result.returncode:
        raise RuntimeError(f"{' '.join(args[:3])}: {result.stdout}{result.stderr}")


def dynamic_linux(scratch: Path) -> None:
    scratch.mkdir()
    for package in ("SDL3-CS.Linux.Mixer", "SDL3-CS.Linux.Shadercross"):
        for source in (ROOT / package / "lib/linux-x64").iterdir():
            if source.is_file() and any(name in source.name for name in ("gme", "mpg123", "vkd3d")):
                shutil.copyfile(source, scratch / source.name)
    targets = (
        ("libgme.so", "gme_open_data"),
        ("libmpg123.so", "mpg123_init"),
        ("libvkd3d.so", "vkd3d_create_instance"),
    )
    for file, _ in targets:
        with (scratch / file).open("ab") as stream:
            stream.write(b"\0")
    environment = dict(os.environ, LD_LIBRARY_PATH=str(scratch))
    code = (
        "import ctypes,sys;"
        "[(getattr(ctypes.CDLL(sys.argv[1]+'/'+file),symbol)) "
        "for file,symbol in [('libgme.so','gme_open_data'),"
        "('libmpg123.so','mpg123_init'),('libvkd3d.so','vkd3d_create_instance')]]"
    )
    subprocess.run((sys.executable, "-c", code, str(scratch)), env=environment, check=True)
    print("3 modified Linux shared libraries loaded from a replacement directory")


def static_apple(scratch: Path) -> None:
    scratch.mkdir()
    linker = next(
        (path for name in ("ld64.lld", *(f"ld64.lld-{version}" for version in range(23, 13, -1)))
         if (path := shutil.which(name))),
        None,
    )
    if linker is None:
        raise RuntimeError("ld64.lld (or a versioned ld64.lld) is required")
    for platform, rid, target, arch, linker_platform in APPLE:
        for library, symbol in (("gme", "gme_open_data"), ("mpg123", "mpg123_init")):
            work = scratch / f"{rid}-{library}"
            work.mkdir()
            original = ROOT / f"SDL3-CS.{platform}.Mixer" / "lib" / rid / f"lib{library}.a"
            replacement = work / original.name
            shutil.copyfile(original, replacement)
            (work / "probe.c").write_text(f"extern void {symbol}(void); void probe(void) {{ {symbol}(); }}\n")
            (work / "marker.c").write_text("void modified_library_marker(void) {}\n")
            run("clang", "-target", target, "-c", str(work / "probe.c"), "-o", str(work / "probe.o"))
            run("clang", "-target", target, "-c", str(work / "marker.c"), "-o", str(work / "marker.o"))
            run("llvm-ar", "rcs", str(replacement), str(work / "marker.o"))
            if hashlib.sha256(original.read_bytes()).digest() == hashlib.sha256(replacement.read_bytes()).digest():
                raise AssertionError(f"{rid}: replacement archive is unchanged")
            run(
                linker, "-dylib", "-arch", arch, "-platform_version", linker_platform,
                "13.0", "13.0", "-undefined", "dynamic_lookup", "-o", str(work / "relinked.dylib"),
                str(work / "probe.o"), str(replacement),
            )
    print("12 modified iOS/tvOS static archives relinked across 6 RIDs")


if __name__ == "__main__":
    if sys.platform != "linux" or os.uname().machine not in ("x86_64", "amd64"):
        raise SystemExit("This cross-target probe requires a Linux x64 host.")
    with tempfile.TemporaryDirectory(prefix="sdl3cs-lgpl-replace-") as directory:
        root = Path(directory)
        dynamic_linux(root / "shared")
        static_apple(root / "static")
