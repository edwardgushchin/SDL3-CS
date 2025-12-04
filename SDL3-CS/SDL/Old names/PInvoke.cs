#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 */
#endregion

namespace SDL3;

public partial class SDL
{
    #region SDL_atomic.h
    public static int AtomicAdd(ref AtomicInt a, int v) => AddAtomicInt(ref a, v);
    public static bool AtomicCAS(ref AtomicInt a, int oldval, int newval) => CompareAndSwapAtomicInt(ref a, oldval, newval);
    public static bool AtomicCASPtr(ref IntPtr a, IntPtr oldval, IntPtr newval) => CompareAndSwapAtomicPointer(ref a, oldval, newval);
    public static IntPtr AtomicGetPtr(ref IntPtr a) => GetAtomicPointer(ref a);
    public static void AtomicLock(ref int @lock) => LockSpinlock(ref @lock);
    public static int AtomicSet(ref AtomicInt a, int v) => SetAtomicInt(ref a, v);
    public static IntPtr AtomicSetPtr(ref IntPtr a, IntPtr v) => SetAtomicPointer(ref a, v);
    public static bool AtomicTryLock(ref int @lock) => TryLockSpinlock(ref @lock);
    public static void AtomicUnlock(ref int @lock) => UnlockSpinlock(ref @lock);
    #endregion
    
    #region SDL_audio.h
    public static int AudioStreamAvailable(IntPtr stream) => GetAudioStreamAvailable(stream);
    public static bool AudioStreamClear(IntPtr stream) => ClearAudioStream(stream);
    public static bool AudioStreamFlush(IntPtr stream) => FlushAudioStream(stream);
    public static int AudioStreamGet(IntPtr stream, byte[] buf, int len) => GetAudioStreamData(stream, buf, len);
    public static bool AudioStreamPut(IntPtr stream, byte[] buf, int len) => PutAudioStreamData(stream, buf, len);
    public static void FreeAudioStream(IntPtr stream) => DestroyAudioStream(stream);
    public static void FreeWAV(IntPtr mem) => Free(mem);
    public static bool LoadWAVRW(IntPtr src, bool closeio, out AudioSpec spec, out IntPtr audioBuf, out uint audioLen) => LoadWAVIO(src, closeio, out spec, out audioBuf, out audioLen);
    public static bool MixAudioFormat(IntPtr dst, IntPtr src, AudioFormat format, uint len, float volume) => MixAudio(dst, src, format, len, volume);
    public static IntPtr NewAudioStream(in AudioSpec srcSpec, in AudioSpec dstSpec) => CreateAudioStream(srcSpec, dstSpec);
    #endregion
    
    #region SDL_cpuinfo.h
    public static int GetCPUCount() => GetNumLogicalCPUCores();
    public static UIntPtr SIMDGetAlignment() => GetSIMDAlignment();
    #endregion
    
    #region SDL_events.h
    public static void DelEventWatch(EventFilter filter, IntPtr userdata) => RemoveEventWatch(filter, userdata);
    #endregion
    
    #region SDL_gamecontroller.h
    public static int GameControllerAddMapping(string mapping) => AddGamepadMapping(mapping);
    public static int GameControllerAddMappingsFromFile(string file) => AddGamepadMappingsFromFile(file);
    public static int GameControllerAddMappingsFromRW(IntPtr src, bool closeio) => AddGamepadMappingsFromIO(src, closeio);
    public static void GameControllerClose(IntPtr gamepad) => CloseGamepad(gamepad);
    public static IntPtr GameControllerFromInstanceID(uint instanceID) => GetGamepadFromID(instanceID);
    public static IntPtr GameControllerFromPlayerIndex(int playerIndex) => GetGamepadFromPlayerIndex(playerIndex);
    public static string? GameControllerGetAppleSFSymbolsNameForAxis(IntPtr gamepad, GamepadAxis axis) => GetGamepadAppleSFSymbolsNameForAxis(gamepad, axis);
    public static string? GameControllerGetAppleSFSymbolsNameForButton(IntPtr gamepad, GamepadButton button) => GetGamepadAppleSFSymbolsNameForButton(gamepad, button);
    public static bool GameControllerGetAttached(IntPtr gamepad) => GamepadConnected(gamepad);
    public static short GameControllerGetAxis(IntPtr gamepad, GamepadAxis axis) => GetGamepadAxis(gamepad, axis);
    public static GamepadAxis GameControllerGetAxisFromString(string str) => GetGamepadAxisFromString(str);
    
    /*public static SDL_GameControllerGetButton SDL_GetGamepadButton
    public static SDL_GameControllerGetButtonFromString SDL_GetGamepadButtonFromString
    public static SDL_GameControllerGetFirmwareVersion SDL_GetGamepadFirmwareVersion
    public static SDL_GameControllerGetJoystick SDL_GetGamepadJoystick
    public static SDL_GameControllerGetNumTouchpadFingers SDL_GetNumGamepadTouchpadFingers
    public static SDL_GameControllerGetNumTouchpads SDL_GetNumGamepadTouchpads
    public static SDL_GameControllerGetPlayerIndex SDL_GetGamepadPlayerIndex
    public static SDL_GameControllerGetProduct SDL_GetGamepadProduct
    public static SDL_GameControllerGetProductVersion SDL_GetGamepadProductVersion
    public static SDL_GameControllerGetSensorData SDL_GetGamepadSensorData
    public static SDL_GameControllerGetSensorDataRate SDL_GetGamepadSensorDataRate
    public static SDL_GameControllerGetSerial SDL_GetGamepadSerial
    public static SDL_GameControllerGetSteamHandle SDL_GetGamepadSteamHandle
    public static SDL_GameControllerGetStringForAxis SDL_GetGamepadStringForAxis
    public static SDL_GameControllerGetStringForButton SDL_GetGamepadStringForButton
    public static SDL_GameControllerGetTouchpadFinger SDL_GetGamepadTouchpadFinger
    public static SDL_GameControllerGetType SDL_GetGamepadType
    public static SDL_GameControllerGetVendor SDL_GetGamepadVendor
    public static SDL_GameControllerHasAxis SDL_GamepadHasAxis
    public static SDL_GameControllerHasButton SDL_GamepadHasButton
    public static SDL_GameControllerHasSensor SDL_GamepadHasSensor
    public static SDL_GameControllerIsSensorEnabled SDL_GamepadSensorEnabled
    public static SDL_GameControllerMapping SDL_GetGamepadMapping
    public static SDL_GameControllerMappingForGUID SDL_GetGamepadMappingForGUID
    public static SDL_GameControllerName SDL_GetGamepadName
    public static SDL_GameControllerOpen SDL_OpenGamepad
    public static SDL_GameControllerPath SDL_GetGamepadPath
    public static SDL_GameControllerRumble SDL_RumbleGamepad
    public static SDL_GameControllerRumbleTriggers SDL_RumbleGamepadTriggers
    public static SDL_GameControllerSendEffect SDL_SendGamepadEffect
    public static SDL_GameControllerSetLED SDL_SetGamepadLED
    public static SDL_GameControllerSetPlayerIndex SDL_SetGamepadPlayerIndex
    public static SDL_GameControllerSetSensorEnabled SDL_SetGamepadSensorEnabled
    public static SDL_GameControllerType SDL_GamepadType
    public static SDL_GameControllerUpdate SDL_UpdateGamepads
    public static SDL_INIT_GAMECONTROLLER SDL_INIT_GAMEPAD
    public static SDL_IsGameController SDL_IsGamep*/
    #endregion
}
