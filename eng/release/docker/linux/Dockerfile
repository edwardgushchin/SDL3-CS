FROM archlinux:base-devel

RUN pacman -Syu --needed --noconfirm \
    alsa-lib \
    bison \
    cmake \
    dotnet-sdk \
    fribidi \
    git \
    hidapi \
    ibus \
    jack2 \
    libdecor \
    libdrm \
    libglvnd \
    libpulse \
    libthai \
    liburing \
    libusb \
    libx11 \
    libxcursor \
    libxext \
    libxfixes \
    libxi \
    libxinerama \
    libxkbcommon \
    libxrandr \
    libxrender \
    libxss \
    libxtst \
    make \
    mesa \
    nasm \
    ninja \
    patchelf \
    pipewire \
    pkgconf \
    sndio \
    vulkan-headers \
    vulkan-icd-loader \
    wayland \
    wayland-protocols \
    && pacman -Scc --noconfirm

RUN dotnet tool install --global PowerShell

ENV PATH="/root/.dotnet/tools:${PATH}"
ENV SDL3CS_NATIVE_BUILD_PARALLEL_LEVEL=2

WORKDIR /work

CMD ["pwsh", "-NoProfile"]
