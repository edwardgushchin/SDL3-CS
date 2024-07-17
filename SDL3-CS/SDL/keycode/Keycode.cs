#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you, must not
 * claim that you, wrote the original software. If you, use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

namespace SDL3;

public static partial class SDL
{
    public enum Keycode : uint
    {
        Unknown             = 0x00000000u, /* 0 */
        Return              = 0x0000000du, /* '\r' */
        Escape              = 0x0000001bu, /* '\x1B' */
        Backspace           = 0x00000008u, /* '\b' */
        Tab                 = 0x00000009u, /* '\t' */
        Space               = 0x00000020u, /* ' ' */
        Exclaim             = 0x00000021u, /* '!' */
        DblApostrophe       = 0x00000022u, /* '"' */
        Hash                = 0x00000023u, /* '#' */
        Dollar              = 0x00000024u, /* '$' */
        Percent             = 0x00000025u, /* '%' */
        Ampersand           = 0x00000026u, /* '&' */
        Apostrophe          = 0x00000027u, /* '\'' */
        LeftParen           = 0x00000028u, /* '(' */
        RightParen          = 0x00000029u, /* ')' */
        Asterisk            = 0x0000002au, /* '*' */
        Plus                = 0x0000002bu, /* '+' */
        Comma               = 0x0000002cu, /* ',' */
        Minus               = 0x0000002du, /* '-' */
        Period              = 0x0000002eu, /* '.' */
        Slash               = 0x0000002fu, /* '/' */
        Alpha0              = 0x00000030u, /* '0' */
        Alpha1              = 0x00000031u, /* '1' */
        Alpha2              = 0x00000032u, /* '2' */
        Alpha3              = 0x00000033u, /* '3' */
        Alpha4              = 0x00000034u, /* '4' */
        Alpha5              = 0x00000035u, /* '5' */
        Alpha6              = 0x00000036u, /* '6' */
        Alpha7              = 0x00000037u, /* '7' */
        Alpha8              = 0x00000038u, /* '8' */
        Alpha9              = 0x00000039u, /* '9' */
        Colon               = 0x0000003au, /* ':' */
        Semicolon           = 0x0000003bu, /* ';' */
        Less                = 0x0000003cu, /* '<' */
        Equals              = 0x0000003du, /* '=' */
        Greater             = 0x0000003eu, /* '>' */
        Question            = 0x0000003fu, /* '?' */
        At                  = 0x00000040u, /* '@' */
        LeftBracket         = 0x0000005bu, /* '[' */
        Backslash           = 0x0000005cu, /* '\\' */
        RightBracket        = 0x0000005du, /* ']' */
        Caret               = 0x0000005eu, /* '^' */
        Underscore          = 0x0000005fu, /* '_' */
        Grave               = 0x00000060u, /* '`' */
        A                   = 0x00000061u, /* 'a' */
        B                   = 0x00000062u, /* 'b' */
        C                   = 0x00000063u, /* 'c' */
        D                   = 0x00000064u, /* 'd' */
        E                   = 0x00000065u, /* 'e' */
        F                   = 0x00000066u, /* 'f' */
        G                   = 0x00000067u, /* 'g' */
        H                   = 0x00000068u, /* 'h' */
        I                   = 0x00000069u, /* 'i' */
        J                   = 0x0000006au, /* 'j' */
        K                   = 0x0000006bu, /* 'k' */
        L                   = 0x0000006cu, /* 'l' */
        M                   = 0x0000006du, /* 'm' */
        N                   = 0x0000006eu, /* 'n' */
        O                   = 0x0000006fu, /* 'o' */
        P                   = 0x00000070u, /* 'p' */
        Q                   = 0x00000071u, /* 'q' */
        R                   = 0x00000072u, /* 'r' */
        S                   = 0x00000073u, /* 's' */
        T                   = 0x00000074u, /* 't' */
        U                   = 0x00000075u, /* 'u' */
        V                   = 0x00000076u, /* 'v' */
        W                   = 0x00000077u, /* 'w' */
        X                   = 0x00000078u, /* 'x' */
        Y                   = 0x00000079u, /* 'y' */
        Z                   = 0x0000007au, /* 'z' */
        LeftBrace           = 0x0000007bu, /* '{' */
        Pipe                = 0x0000007cu, /* '|' */
        RightBrace          = 0x0000007du, /* '}' */
        Tilde               = 0x0000007eu, /* '~' */
        Delete              = 0x0000007fu, /* '\x7F' */
        PlusMinus           = 0x000000b1u, /* '±' */
        Capslock            = 0x40000039u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_CAPSLOCK) */
        F1                  = 0x4000003au, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F1) */
        F2                  = 0x4000003bu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F2) */
        F3                  = 0x4000003cu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F3) */
        F4                  = 0x4000003du, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F4) */
        F5                  = 0x4000003eu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F5) */
        F6                  = 0x4000003fu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F6) */
        F7                  = 0x40000040u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F7) */
        F8                  = 0x40000041u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F8) */
        F9                  = 0x40000042u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F9) */
        F10                 = 0x40000043u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F10) */
        F11                 = 0x40000044u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F11) */
        F12                 = 0x40000045u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F12) */
        PrintScreen         = 0x40000046u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_PRINTSCREEN) */
        ScrolLlock          = 0x40000047u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_SCROLLLOCK) */
        Pause               = 0x40000048u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_PAUSE) */
        Insert              = 0x40000049u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_INSERT) */
        Home                = 0x4000004au, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_HOME) */
        Pageup              = 0x4000004bu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_PAGEUP) */
        End                 = 0x4000004du, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_END) */
        Pagedown            = 0x4000004eu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_PAGEDOWN) */
        Right               = 0x4000004fu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_RIGHT) */
        Left                = 0x40000050u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_LEFT) */
        Down                = 0x40000051u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_DOWN) */
        Up                  = 0x40000052u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_UP) */
        NumLockClear        = 0x40000053u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_NUMLOCKCLEAR) */
        KpDivide            = 0x40000054u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_DIVIDE) */
        KpMultiply          = 0x40000055u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_MULTIPLY) */
        KpMinus             = 0x40000056u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_MINUS) */
        KpPlus              = 0x40000057u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_PLUS) */
        KpEnter             = 0x40000058u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_ENTER) */
        Kp1                 = 0x40000059u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_1) */
        Kp2                 = 0x4000005au, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_2) */
        Kp3                 = 0x4000005bu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_3) */
        Kp4                 = 0x4000005cu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_4) */
        Kp5                 = 0x4000005du, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_5) */
        Kp6                 = 0x4000005eu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_6) */
        Kp7                 = 0x4000005fu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_7) */
        Kp8                 = 0x40000060u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_8) */
        Kp9                 = 0x40000061u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_9) */
        Kp0                 = 0x40000062u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_0) */
        KpPeriod            = 0x40000063u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_PERIOD) */
        Application         = 0x40000065u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_APPLICATION) */
        Power               = 0x40000066u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_POWER) */
        KpEquals            = 0x40000067u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_EQUALS) */
        F13                 = 0x40000068u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F13) */
        F14                 = 0x40000069u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F14) */
        F15                 = 0x4000006au, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F15) */
        F16                 = 0x4000006bu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F16) */
        F17                 = 0x4000006cu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F17) */
        F18                 = 0x4000006du, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F18) */
        F19                 = 0x4000006eu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F19) */
        F20                 = 0x4000006fu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F20) */
        F21                 = 0x40000070u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F21) */
        F22                 = 0x40000071u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F22) */
        F23                 = 0x40000072u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F23) */
        F24                 = 0x40000073u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_F24) */
        Execute             = 0x40000074u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_EXECUTE) */
        Help                = 0x40000075u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_HELP) */
        Menu                = 0x40000076u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MENU) */
        Select              = 0x40000077u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_SELECT) */
        Stop                = 0x40000078u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_STOP) */
        Again               = 0x40000079u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AGAIN) */
        Undo                = 0x4000007au, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_UNDO) */
        Cut                 = 0x4000007bu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_CUT) */
        Copy                = 0x4000007cu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_COPY) */
        Paste               = 0x4000007du, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_PASTE) */
        Find                = 0x4000007eu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_FIND) */
        Mute                = 0x4000007fu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MUTE) */
        VolumeUp            = 0x40000080u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_VOLUMEUP) */
        VolumeDown          = 0x40000081u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_VOLUMEDOWN) */
        KpComma             = 0x40000085u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_COMMA) */
        KpEqualAas400       = 0x40000086u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_EQUALSAS400) */
        AltErase            = 0x40000099u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_ALTERASE) */
        SysReq              = 0x4000009au, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_SYSREQ) */
        Cancel              = 0x4000009bu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_CANCEL) */
        Clear               = 0x4000009cu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_CLEAR) */
        Prior               = 0x4000009du, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_PRIOR) */
        Return2             = 0x4000009eu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_RETURN2) */
        Separator           = 0x4000009fu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_SEPARATOR) */
        Out                 = 0x400000a0u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_OUT) */
        Oper                = 0x400000a1u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_OPER) */
        ClearAgain          = 0x400000a2u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_CLEARAGAIN) */
        CrSel               = 0x400000a3u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_CRSEL) */
        ExSel               = 0x400000a4u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_EXSEL) */
        Kp00                = 0x400000b0u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_00) */
        Kp000               = 0x400000b1u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_000) */
        ThousandsSeparator  = 0x400000b2u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_THOUSANDSSEPARATOR) */
        DecimalSeparator    = 0x400000b3u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_DECIMALSEPARATOR) */
        CurrenCyUnit        = 0x400000b4u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_CURRENCYUNIT) */
        CurrenCySubunit     = 0x400000b5u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_CURRENCYSUBUNIT) */
        KpLeftParen         = 0x400000b6u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_LEFTPAREN) */
        KpRightParen        = 0x400000b7u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_RIGHTPAREN) */
        KpLeftBrace         = 0x400000b8u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_LEFTBRACE) */
        KpRightBrace        = 0x400000b9u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_RIGHTBRACE) */
        KpTab               = 0x400000bau, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_TAB) */
        KpBackspace         = 0x400000bbu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_BACKSPACE) */
        KpA                 = 0x400000bcu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_A) */
        KpB                 = 0x400000bdu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_B) */
        KpC                 = 0x400000beu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_C) */
        KpD                 = 0x400000bfu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_D) */
        KpE                 = 0x400000c0u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_E) */
        KpF                 = 0x400000c1u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_F) */
        KpXor               = 0x400000c2u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_XOR) */
        KpPower             = 0x400000c3u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_POWER) */
        KpPercent           = 0x400000c4u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_PERCENT) */
        KpLess              = 0x400000c5u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_LESS) */
        KpGreater           = 0x400000c6u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_GREATER) */
        KpAmpersand         = 0x400000c7u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_AMPERSAND) */
        KpDblAmpersand      = 0x400000c8u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_DBLAMPERSAND) */
        KpVerticalBar       = 0x400000c9u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_VERTICALBAR) */
        KpDblVerticalBar    = 0x400000cau, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_DBLVERTICALBAR) */
        KpColon             = 0x400000cbu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_COLON) */
        KpHash              = 0x400000ccu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_HASH) */
        KpSpace             = 0x400000cdu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_SPACE) */
        KpAt                = 0x400000ceu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_AT) */
        KpExClam            = 0x400000cfu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_EXCLAM) */
        KpMemStore          = 0x400000d0u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_MEMSTORE) */
        KpMemRecall         = 0x400000d1u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_MEMRECALL) */
        KpMemClear          = 0x400000d2u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_MEMCLEAR) */
        KpMemAdd            = 0x400000d3u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_MEMADD) */
        KpMemSubtract       = 0x400000d4u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_MEMSUBTRACT) */
        KpMemMultiply       = 0x400000d5u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_MEMMULTIPLY) */
        KpMemDivide         = 0x400000d6u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_MEMDIVIDE) */
        KpPlusMinus         = 0x400000d7u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_PLUSMINUS) */
        KpClear             = 0x400000d8u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_CLEAR) */
        KpClearEntry        = 0x400000d9u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_CLEARENTRY) */
        KpBinary            = 0x400000dau, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_BINARY) */
        KpOctal             = 0x400000dbu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_OCTAL) */
        KpDecimal           = 0x400000dcu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_DECIMAL) */
        KpHexadecimal       = 0x400000ddu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_KP_HEXADECIMAL) */
        LCtrl               = 0x400000e0u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_LCTRL) */
        LShift              = 0x400000e1u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_LSHIFT) */
        LAlt                = 0x400000e2u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_LALT) */
        LGui                = 0x400000e3u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_LGUI) */
        RCtrl               = 0x400000e4u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_RCTRL) */
        RShift              = 0x400000e5u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_RSHIFT) */
        RAlt                = 0x400000e6u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_RALT) */
        RGUI                = 0x400000e7u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_RGUI) */
        Mode                = 0x40000101u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MODE) */
        Sleep               = 0x40000102u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_SLEEP) */
        Wake                = 0x40000103u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_WAKE) */
        ChannelIncrement    = 0x40000104u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_CHANNEL_INCREMENT) */
        ChannelDecrement    = 0x40000105u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_CHANNEL_DECREMENT) */
        MediaPlay           = 0x40000106u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MEDIA_PLAY) */
        MediaPause          = 0x40000107u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MEDIA_PAUSE) */
        MediaRecord         = 0x40000108u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MEDIA_RECORD) */
        MediaFastForward    = 0x40000109u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MEDIA_FAST_FORWARD) */
        MediaRewind         = 0x4000010au, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MEDIA_REWIND) */
        MediaNextTrack      = 0x4000010bu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MEDIA_NEXT_TRACK) */
        MediaPreviousTrack  = 0x4000010cu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MEDIA_PREVIOUS_TRACK) */
        MediaStop           = 0x4000010du, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MEDIA_STOP) */
        MediaEject          = 0x4000010eu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MEDIA_EJECT) */
        MediaPlayPause      = 0x4000010fu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MEDIA_PLAY_PAUSE) */
        MediaSelect         = 0x40000110u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_MEDIA_SELECT) */
        AcNew               = 0x40000111u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_NEW) */
        AcOpen              = 0x40000112u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_OPEN) */
        AcClose             = 0x40000113u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_CLOSE) */
        AcExit              = 0x40000114u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_EXIT) */
        AcSave              = 0x40000115u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_SAVE) */
        AcPrint             = 0x40000116u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_PRINT) */
        AcProperties        = 0x40000117u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_PROPERTIES) */
        AcSearch            = 0x40000118u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_SEARCH) */
        AcHome              = 0x40000119u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_HOME) */
        AcBack              = 0x4000011au, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_BACK) */
        AcForward           = 0x4000011bu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_FORWARD) */
        AcStop              = 0x4000011cu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_STOP) */
        AcRefresh           = 0x4000011du, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_REFRESH) */
        AcBookmarks         = 0x4000011eu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_AC_BOOKMARKS) */
        SoftLeft            = 0x4000011fu, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_SOFTLEFT) */
        SoftRight           = 0x40000120u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_SOFTRIGHT) */
        Call                = 0x40000121u, /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_CALL) */
        EndCall             = 0x40000122u /* SDL_SCANCODE_TO_KEYCODE(SDL_SCANCODE_ENDCALL) */
    }
}