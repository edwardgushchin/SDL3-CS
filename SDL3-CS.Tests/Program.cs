using SDL3;

CreateAudioStreamWithNullSpecs nullSpecOverload = SDL.CreateAudioStream;
CreateAudioStreamWithTypedSpecs typedSpecOverload = SDL.CreateAudioStream;
SetAudioStreamFormatWithNullSpecs setAudioStreamFormatNull = SDL.SetAudioStreamFormat;
SetAudioStreamFormatWithTypedSpecs setAudioStreamFormatTyped = SDL.SetAudioStreamFormat;
AlignedAllocZeroDelegate alignedAllocZero = SDL.AlignedAllocZero;
WcstoulDelegate wcstoul = SDL.Wcstoul;
WcstollDelegate wcstoll = SDL.Wcstoll;
WcstoullDelegate wcstoull = SDL.Wcstoull;
RequestNotificationPermissionDelegate requestNotificationPermission = SDL.RequestNotificationPermission;
ShowNotificationWithPropertiesDelegate showNotificationWithProperties = SDL.ShowNotificationWithProperties;
ShowNotificationDelegate showNotification = SDL.ShowNotification;
RemoveNotificationDelegate removeNotification = SDL.RemoveNotification;
IsPhoneDelegate isPhone = SDL.IsPhone;
HasSVE2Delegate hasSVE2 = SDL.HasSVE2;
LoadJPGIODelegate loadJPGIO = SDL.LoadJPGIO;
LoadJPGDelegate loadJPG = SDL.LoadJPG;
GamepadHasCapSenseDelegate gamepadHasCapSense = SDL.GamepadHasCapSense;
GetGamepadCapSenseDelegate getGamepadCapSense = SDL.GetGamepadCapSense;

GC.KeepAlive(nullSpecOverload);
GC.KeepAlive(typedSpecOverload);
GC.KeepAlive(setAudioStreamFormatNull);
GC.KeepAlive(setAudioStreamFormatTyped);
GC.KeepAlive(alignedAllocZero);
GC.KeepAlive(wcstoul);
GC.KeepAlive(wcstoll);
GC.KeepAlive(wcstoull);
GC.KeepAlive(requestNotificationPermission);
GC.KeepAlive(showNotificationWithProperties);
GC.KeepAlive(showNotification);
GC.KeepAlive(removeNotification);
GC.KeepAlive(isPhone);
GC.KeepAlive(hasSVE2);
GC.KeepAlive(loadJPGIO);
GC.KeepAlive(loadJPG);
GC.KeepAlive(gamepadHasCapSense);
GC.KeepAlive(getGamepadCapSense);

if (SDL.Hints.JoystickDrumDevices != "SDL_JOYSTICK_DRUM_DEVICES")
{
    throw new InvalidOperationException("Unexpected JoystickDrumDevices hint value.");
}

if (SDL.Hints.JoystickGuitarDevices != "SDL_JOYSTICK_GUITAR_DEVICES")
{
    throw new InvalidOperationException("Unexpected JoystickGuitarDevices hint value.");
}

if (SDL.Hints.JoystickGameInputRaw != "SDL_JOYSTICK_GAMEINPUT_RAW")
{
    throw new InvalidOperationException("Unexpected JoystickGameInputRaw hint value.");
}

if (SDL.Hints.DosAllowDirectFramebuffer != "SDL_DOS_ALLOW_DIRECT_FRAMEBUFFER")
{
    throw new InvalidOperationException("Unexpected DosAllowDirectFramebuffer hint value.");
}

if (SDL.Hints.VideoX11EnableXsyncExt != "SDL_VIDEO_X11_ENABLE_XSYNC_EXT")
{
    throw new InvalidOperationException("Unexpected VideoX11EnableXsyncExt hint value.");
}

if (SDL.Hints.AndroidAllowPersistentFolderAccess != "SDL_ANDROID_ALLOW_PERSISTENT_FOLDER_ACCESS")
{
    throw new InvalidOperationException("Unexpected AndroidAllowPersistentFolderAccess hint value.");
}

if (SDL.Hints.AudioDuckOthers != "SDL_AUDIO_DUCK_OTHERS")
{
    throw new InvalidOperationException("Unexpected AudioDuckOthers hint value.");
}

if ((uint)SDL.EventType.WindowSettingsChanged != (uint)SDL.EventType.WindowHDRStateChanged + 1)
{
    throw new InvalidOperationException("Unexpected WindowSettingsChanged event value.");
}

if (SDL.EventType.WindowLast != SDL.EventType.WindowSettingsChanged)
{
    throw new InvalidOperationException("Unexpected WindowLast event value after visionOS settings sync.");
}

if (SDL.Props.WindowCreateVisionOSSettingsString != "SDL.window.create.visionos.settings")
{
    throw new InvalidOperationException("Unexpected WindowCreateVisionOSSettingsString property value.");
}

if (SDL.Props.WindowVisionOSSettingsString != "SDL.window.visionos.settings")
{
    throw new InvalidOperationException("Unexpected WindowVisionOSSettingsString property value.");
}

if (SDL.Props.TextInputTitleString != "SDL.textinput.title")
{
    throw new InvalidOperationException("Unexpected TextInputTitleString property value.");
}

if (SDL.Props.TextInputPlaceholderString != "SDL.textinput.placeholder")
{
    throw new InvalidOperationException("Unexpected TextInputPlaceholderString property value.");
}

if (SDL.Props.TextInputDefaultTextString != "SDL.textinput.default_text")
{
    throw new InvalidOperationException("Unexpected TextInputDefaultTextString property value.");
}

if (SDL.Props.TextInputMaxLengthNumber != "SDL.textinput.max_length")
{
    throw new InvalidOperationException("Unexpected TextInputMaxLengthNumber property value.");
}

if ((int)SDL.NotificationPriority.Low != -1)
{
    throw new InvalidOperationException("Unexpected Low notification priority value.");
}

if ((int)SDL.NotificationPriority.Critical != 2)
{
    throw new InvalidOperationException("Unexpected Critical notification priority value.");
}

if ((int)SDL.NotificationActionType.Button != 1)
{
    throw new InvalidOperationException("Unexpected Button notification action type value.");
}

if (System.Runtime.InteropServices.Marshal.SizeOf<SDL.NotificationAction>() != 128)
{
    throw new InvalidOperationException("Unexpected NotificationAction ABI size.");
}

SDL.NotificationAction notificationAction = default;
notificationAction.Type = SDL.NotificationActionType.Button;
notificationAction.Button.Type = SDL.NotificationActionType.Button;
if (notificationAction.Button.Type != SDL.NotificationActionType.Button)
{
    throw new InvalidOperationException("Unexpected NotificationAction union field behavior.");
}

if (SDL.Props.GlobalNotificationHeaderIconString != "SDL.notification.header_icon")
{
    throw new InvalidOperationException("Unexpected GlobalNotificationHeaderIconString property value.");
}

if (SDL.Props.NotificationActionsPointer != "SDL.notification.actions")
{
    throw new InvalidOperationException("Unexpected NotificationActionsPointer property value.");
}

if (SDL.Props.NotificationActionCountNumber != "SDL.notification.action_count")
{
    throw new InvalidOperationException("Unexpected NotificationActionCountNumber property value.");
}

if (SDL.Props.NotificationImagePointer != "SDL.notification.image")
{
    throw new InvalidOperationException("Unexpected NotificationImagePointer property value.");
}

if (SDL.Props.NotificationMessageString != "SDL.notification.message")
{
    throw new InvalidOperationException("Unexpected NotificationMessageString property value.");
}

if (SDL.Props.NotificationPriorityNumber != "SDL.notification.priority")
{
    throw new InvalidOperationException("Unexpected NotificationPriorityNumber property value.");
}

if (SDL.Props.NotificationReplacesNumber != "SDL.notification.replaces")
{
    throw new InvalidOperationException("Unexpected NotificationReplacesNumber property value.");
}

if (SDL.Props.NotificationSoundString != "SDL.notification.sound")
{
    throw new InvalidOperationException("Unexpected NotificationSoundString property value.");
}

if (SDL.Props.NotificationTransientBoolean != "SDL.notification.transient")
{
    throw new InvalidOperationException("Unexpected NotificationTransientBoolean property value.");
}

if (SDL.Props.NotificationTitleString != "SDL.notification.title")
{
    throw new InvalidOperationException("Unexpected NotificationTitleString property value.");
}

if ((int)SDL.SystemCursor.ContextMenu != 20)
{
    throw new InvalidOperationException("Unexpected ContextMenu cursor value.");
}

if ((int)SDL.SystemCursor.ZoomOut != 33)
{
    throw new InvalidOperationException("Unexpected ZoomOut cursor value.");
}

if ((int)SDL.SystemCursor.SDLNumSystemCursors != 34)
{
    throw new InvalidOperationException("Unexpected SDLNumSystemCursors value after CSS cursor sync.");
}

if ((int)SDL.GamepadCapSenseType.Invalid != -1)
{
    throw new InvalidOperationException("Unexpected Invalid gamepad cap sense value.");
}

if ((int)SDL.GamepadCapSenseType.LeftStick != 0)
{
    throw new InvalidOperationException("Unexpected LeftStick gamepad cap sense value.");
}

if ((int)SDL.GamepadCapSenseType.Count != 4)
{
    throw new InvalidOperationException("Unexpected Count gamepad cap sense value.");
}

if ((uint)SDL.EventType.GamepadCapSenseTouch != (uint)SDL.EventType.GamepadSteamHandleUpdated + 1)
{
    throw new InvalidOperationException("Unexpected GamepadCapSenseTouch event value.");
}

if ((uint)SDL.EventType.GamepadCapSenseRelease != (uint)SDL.EventType.GamepadCapSenseTouch + 1)
{
    throw new InvalidOperationException("Unexpected GamepadCapSenseRelease event value.");
}

if ((uint)SDL.EventType.NotificationActionInvoked != 0x1500)
{
    throw new InvalidOperationException("Unexpected NotificationActionInvoked event value.");
}

if (System.Runtime.InteropServices.Marshal.SizeOf<SDL.NotificationEvent>() != (IntPtr.Size == 8 ? 32 : 24))
{
    throw new InvalidOperationException("Unexpected NotificationEvent ABI size.");
}

if (System.Runtime.InteropServices.Marshal.OffsetOf<SDL.NotificationEvent>(nameof(SDL.NotificationEvent.Timestamp)).ToInt32() != 8)
{
    throw new InvalidOperationException("Unexpected NotificationEvent.Timestamp offset.");
}

if (System.Runtime.InteropServices.Marshal.OffsetOf<SDL.NotificationEvent>(nameof(SDL.NotificationEvent.Which)).ToInt32() != 16)
{
    throw new InvalidOperationException("Unexpected NotificationEvent.Which offset.");
}

if (System.Runtime.InteropServices.Marshal.OffsetOf<SDL.NotificationEvent>(nameof(SDL.NotificationEvent.ActionID)).ToInt32() != (IntPtr.Size == 8 ? 24 : 20))
{
    throw new InvalidOperationException("Unexpected NotificationEvent.ActionID offset.");
}

SDL.Event capSenseUnion = default;
capSenseUnion.GCapSense.Type = SDL.EventType.GamepadCapSenseTouch;
if (capSenseUnion.GCapSense.Type != SDL.EventType.GamepadCapSenseTouch)
{
    throw new InvalidOperationException("Unexpected gamepad cap sense union field behavior.");
}

SDL.Event notificationUnion = default;
notificationUnion.Notification.Type = SDL.EventType.NotificationActionInvoked;
notificationUnion.Notification.Which = 42;
if (notificationUnion.Type != (uint)SDL.EventType.NotificationActionInvoked)
{
    throw new InvalidOperationException("Unexpected notification union type field behavior.");
}

if (notificationUnion.Notification.Which != 42)
{
    throw new InvalidOperationException("Unexpected notification union data field behavior.");
}

if ((int)SDL.GamepadType.Steam != (int)SDL.GamepadType.GameCube + 1)
{
    throw new InvalidOperationException("Unexpected Steam gamepad type value.");
}

if ((int)SDL.GamepadType.Count != (int)SDL.GamepadType.Steam + 1)
{
    throw new InvalidOperationException("Unexpected gamepad type count after Steam insertion.");
}

Console.WriteLine("CreateAudioStream overload smoke test passed.");
Console.WriteLine("SetAudioStreamFormat overload smoke test passed.");
Console.WriteLine("AlignedAllocZero binding smoke test passed.");
Console.WriteLine("Wide-char conversion binding smoke test passed.");
Console.WriteLine("Joystick device hint constants smoke test passed.");
Console.WriteLine("Joystick GameInput raw hint constant smoke test passed.");
Console.WriteLine("DOS framebuffer hint constant smoke test passed.");
Console.WriteLine("X11 XSync hint constant smoke test passed.");
Console.WriteLine("Android persistent folder access hint smoke test passed.");
Console.WriteLine("Audio duck others hint smoke test passed.");
Console.WriteLine("visionOS settings window constants smoke test passed.");
Console.WriteLine("Text input property constants smoke test passed.");
Console.WriteLine("IsPhone binding smoke test passed.");
Console.WriteLine("SVE2 CPU binding smoke test passed.");
Console.WriteLine("JPEG surface loader binding smoke test passed.");
Console.WriteLine("Notification API binding smoke test passed.");
Console.WriteLine("CSS system cursor enum smoke test passed.");
Console.WriteLine("Gamepad cap sense API smoke test passed.");
Console.WriteLine("Notification event smoke test passed.");
Console.WriteLine("Steam gamepad type smoke test passed.");

delegate IntPtr CreateAudioStreamWithNullSpecs(IntPtr srcSpec, IntPtr dstSpec);

delegate IntPtr CreateAudioStreamWithTypedSpecs(in SDL.AudioSpec srcSpec, in SDL.AudioSpec dstSpec);

delegate bool SetAudioStreamFormatWithNullSpecs(IntPtr stream, IntPtr srcSpec, IntPtr dstSpec);

delegate bool SetAudioStreamFormatWithTypedSpecs(IntPtr stream, in SDL.AudioSpec srcSpec, in SDL.AudioSpec dstSpec);

delegate IntPtr AlignedAllocZeroDelegate(UIntPtr alignment, UIntPtr size);

delegate System.Runtime.InteropServices.CULong WcstoulDelegate(string str, IntPtr endp, int @base);

delegate long WcstollDelegate(string str, IntPtr endp, int @base);

delegate ulong WcstoullDelegate(string str, IntPtr endp, int @base);

delegate bool RequestNotificationPermissionDelegate();

delegate uint ShowNotificationWithPropertiesDelegate(uint props);

delegate uint ShowNotificationDelegate(string title, string? message, IntPtr image, IntPtr actions, int numActions);

delegate bool RemoveNotificationDelegate(uint notification);

delegate bool IsPhoneDelegate();

delegate bool HasSVE2Delegate();

delegate IntPtr LoadJPGIODelegate(IntPtr src, bool closeio);

delegate IntPtr LoadJPGDelegate(string file);

delegate bool GamepadHasCapSenseDelegate(IntPtr gamepad, SDL.GamepadCapSenseType type);

delegate bool GetGamepadCapSenseDelegate(IntPtr gamepad, SDL.GamepadCapSenseType type);
