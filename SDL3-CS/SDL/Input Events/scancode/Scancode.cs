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

public static partial class SDL
{
    /// <summary>
    /// <para>The SDL keyboard scancode representation.</para>
    /// <para>An SDL scancode is the physical representation of a key on the keyboard,
    /// independent of language and keyboard mapping.</para>
    /// <para>VValues of this type are used to represent keyboard keys, among other places
    /// in the <c>scancode</c> field of the <see cref="KeyboardEvent"/> structure.</para>
    /// <para>The values in this enumeration are based on the USB usage page standard:
    /// https://usb.org/sites/default/files/hut1_5.pdf</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    public enum Scancode
    {
        Unknown = 0,

        A = 4,
        B = 5,
        C = 6,
        D = 7,
        E = 8,
        F = 9,
        G = 10,
        H = 11,
        I = 12,
        J = 13,
        K = 14,
        L = 15,
        M = 16,
        N = 17,
        O = 18,
        P = 19,
        Q = 20,
        R = 21,
        S = 22,
        T = 23,
        U = 24,
        V = 25,
        W = 26,
        X = 27,
        Y = 28,
        Z = 29,

        Alpha1 = 30,
        Alpha2 = 31,
        Alpha3 = 32,
        Alpha4 = 33,
        Alpha5 = 34,
        Alpha6 = 35,
        Alpha7 = 36,
        Alpha8 = 37,
        Alpha9 = 38,
        Alpha0 = 39,

        Return = 40,
        Escape = 41,
        Backspace = 42,
        Tab = 43,
        Space = 44,

        Minus = 45,
        Equals = 46,
        Leftbracket = 47,
        Rightbracket = 48,
        
        /// <summary>
        /// Located at the lower left of the return
        /// key on ISO keyboards and at the right end
        /// of the QWERTY row on ANSI keyboards.
        /// Produces REVERSE SOLIDUS (backslash) and
        /// VERTICAL LINE in a US layout, REVERSE
        /// SOLIDUS and VERTICAL LINE in a UK Mac
        /// layout, NUMBER SIGN and TILDE in a UK
        /// Windows layout, DOLLAR SIGN and POUND SIGN
        /// in a Swiss German layout, NUMBER SIGN and
        /// APOSTROPHE in a German layout, GRAVE
        /// ACCENT and POUND SIGN in a French Mac
        /// layout, and ASTERISK and MICRO SIGN in a
        /// French Windows layout.
        /// </summary>
        Backslash = 49,
        
        /// <summary>
        /// ISO USB keyboards actually use this code
        /// instead of 49 for the same key, but all
        /// OSes I've seen treat the two codes
        /// identically. So, as an implementor, unless
        /// your keyboard generates both of those
        /// codes and your OS treats them differently,
        /// you should generate BACKSLASH
        /// instead of this code. As a user, you
        /// should not rely on this code because SDL
        /// will never generate it with most (all?)
        /// keyboards.
        /// </summary>
        NonUshash = 50,
        Semicolon = 51,
        Apostrophe = 52,
        
        /// <summary>
        /// Located in the top left corner (on both ANSI
        /// and ISO keyboards). Produces GRAVE ACCENT and
        /// TILDE in a US Windows layout and in US and UK
        /// Mac layouts on ANSI keyboards, GRAVE ACCENT
        /// and NOT SIGN in a UK Windows layout, SECTION
        /// SIGN and PLUS-MINUS SIGN in US and UK Mac
        /// layouts on ISO keyboards, SECTION SIGN and
        /// DEGREE SIGN in a Swiss German layout (Mac:
        /// only on ISO keyboards), CIRCUMFLEX ACCENT and
        /// DEGREE SIGN in a German layout (Mac: only on
        /// ISO keyboards), SUPERSCRIPT TWO and TILDE in a
        /// French Windows layout, COMMERCIAL AT and
        /// NUMBER SIGN in a French Mac layout on ISO
        /// keyboards, and LESS-THAN SIGN and GREATER-THAN
        /// SIGN in a Swiss German, German, or French Mac
        /// layout on ANSI keyboards.
        /// </summary>
        Grave = 53,
        Comma = 54,
        Period = 55,
        Slash = 56,

        Capslock = 57,

        F1 = 58,
        F2 = 59,
        F3 = 60,
        F4 = 61,
        F5 = 62,
        F6 = 63,
        F7 = 64,
        F8 = 65,
        F9 = 66,
        F10 = 67,
        F11 = 68,
        F12 = 69,

        Printscreen = 70,
        Scrolllock = 71,
        Pause = 72,
        
        /// <summary>
        /// insert on PC, help on some Mac keyboards (but
        /// does send code 73, not 117)
        /// </summary>
        Insert = 73,
        Home = 74,
        Pageup = 75,
        Delete = 76,
        End = 77,
        Pagedown = 78,
        Right = 79,
        Left = 80,
        Down = 81,
        Up = 82,

        /// <summary>
        /// num lock on PC, clear on Mac keyboards
        /// </summary>
        NumLockClear = 83,
        KpDivide = 84,
        KpMultiply = 85,
        KpMinus = 86,
        KpPlus = 87,
        KpEnter = 88,
        Kp1 = 89,
        Kp2 = 90,
        Kp3 = 91,
        Kp4 = 92,
        Kp5 = 93,
        Kp6 = 94,
        Kp7 = 95,
        Kp8 = 96,
        Kp9 = 97,
        Kp0 = 98,
        KpPeriod = 99,

        /// <summary>
        /// This is the additional key that ISO
        /// keyboards have over ANSI ones,
        /// located between left shift and Z.
        /// Produces GRAVE ACCENT and TILDE in a
        /// US or UK Mac layout, REVERSE SOLIDUS
        /// (backslash) and VERTICAL LINE in a
        /// US or UK Windows layout, and
        /// LESS-THAN SIGN and GREATER-THAN SIGN
        /// in a Swiss German, German, or French
        /// layout.
        /// </summary>
        NonUsBackSlash = 100,
        
        /// <summary>
        /// windows contextual menu, compose
        /// </summary>
        Application = 101,
        
        /// <summary>
        /// The USB document says this is a status flag,
        /// not a physical key - but some Mac keyboards
        /// do have a power key.
        /// </summary>
        Power = 102,
        KpEquals = 103,
        F13 = 104,
        F14 = 105,
        F15 = 106,
        F16 = 107,
        F17 = 108,
        F18 = 109,
        F19 = 110,
        F20 = 111,
        F21 = 112,
        F22 = 113,
        F23 = 114,
        F24 = 115,
        Execute = 116,
        
        /// <summary>
        /// AL Integrated Help Center
        /// </summary>
        Help = 117,
        
        /// <summary>
        /// Menu (show menu)
        /// </summary>
        Menu = 118,
        Select = 119,
        
        /// <summary>
        /// AC Stop
        /// </summary>
        Stop = 120,
        
        /// <summary>
        /// AC Redo/Repeat
        /// </summary>
        Again = 121,
        
        /// <summary>
        /// AC Undo
        /// </summary>
        Undo = 122,
        
        /// <summary>
        /// AC Cut
        /// </summary>
        Cut = 123,
        
        /// <summary>
        /// AC Copy
        /// </summary>
        Copy = 124,
        
        /// <summary>
        /// AC Paste
        /// </summary>
        Paste = 125,
        
        /// <summary>
        /// AC Find
        /// </summary>
        Find = 126,
        Mute = 127,
        VolumeUp = 128,
        VolumeDown = 129,
        
        
         /*
          not sure whether there's a reason to enable these
          LOCKINGCAPSLOCK = 130,
          LOCKINGNUMLOCK = 131,
          LOCKINGSCROLLLOCK = 132, 
         */
         
        KpComma = 133,
        KpEqualsAs400 = 134,

        /// <summary>
        /// used on Asian keyboards, see
        /// footnotes in USB doc
        /// </summary>
        International1 = 135,
        International2 = 136,
        
        /// <summary>
        /// Yen
        /// </summary>
        International3 = 137,
        International4 = 138,
        International5 = 139,
        International6 = 140,
        International7 = 141,
        International8 = 142,
        International9 = 143,
        
        /// <summary>
        /// Hangul/English toggle
        /// </summary>
        Lang1 = 144,
        
        /// <summary>
        /// Hanja conversion
        /// </summary>
        Lang2 = 145,
        
        /// <summary>
        /// Katakana
        /// </summary>
        Lang3 = 146,
        
        /// <summary>
        /// Hiragana
        /// </summary>
        Lang4 = 147,
        
        /// <summary>
        /// Zenkaku/Hankaku
        /// </summary>
        Lang5 = 148,
        
        /// <summary>
        /// reserved
        /// </summary>
        Lang6 = 149,
        
        /// <summary>
        /// reserved
        /// </summary>
        Lang7 = 150,
        
        /// <summary>
        /// reserved
        /// </summary>
        Lang8 = 151,
        
        /// <summary>
        /// reserved
        /// </summary>
        Lang9 = 152,

        /// <summary>
        /// Erase-Eaze
        /// </summary>
        AltErase = 153,
        SysReq = 154,
        
        /// <summary>
        /// AC Cancel
        /// </summary>
        Cancel = 155,
        Clear = 156,
        Prior = 157,
        Return2 = 158,
        Separator = 159,
        Out = 160,
        Oper = 161,
        ClearAgain = 162,
        CrSel = 163,
        ExSel = 164,

        Kp00 = 176,
        Kp000 = 177,
        ThousandsSeparator = 178,
        DecimalSeparator = 179,
        CurrencyUnit = 180,
        CurrencySubunit = 181,
        KpLeftParen = 182,
        KpRightParen = 183,
        KpLeftBrace = 184,
        KpRightBrace = 185,
        KpTab = 186,
        KpBackspace = 187,
        KpA = 188,
        KpB = 189,
        KpC = 190,
        KpD = 191,
        KpE = 192,
        KpF = 193,
        KpXor = 194,
        KpPower = 195,
        KpPercent = 196,
        KpLess = 197,
        KpGreater = 198,
        KpAmpersand = 199,
        KpDblAmpersand = 200,
        KpVerticalBar = 201,
        KpDblVerticalBar = 202,
        KpColon = 203,
        KpHash = 204,
        KpSpace = 205,
        KpAt = 206,
        KpExClam = 207,
        KpMemStore = 208,
        KpMemRecall = 209,
        KpMemClear = 210,
        KpMemAdd = 211,
        KpMemSubtract = 212,
        KpMemMultiply = 213,
        KpMemDivide = 214,
        KpPlusMinus = 215,
        KpClear = 216,
        KpClearEntry = 217,
        KpBinary = 218,
        KpOctal = 219,
        KpDecimal = 220,
        KpHexadecimal = 221,

        LCtrl = 224,
        LShift = 225,
        
        /// <summary>
        /// alt, option
        /// </summary>
        LAlt = 226,
        
        /// <summary>
        /// windows, command (apple), meta
        /// </summary>
        LGUI = 227,
        RCtrl = 228,
        RShift = 229,
        
        /// <summary>
        /// alt gr, option
        /// </summary>
        RAlt = 230,
        
        /// <summary>
        /// windows, command (apple), meta
        /// </summary>
        RGUI = 231,

        /// <summary>
        /// I'm not sure if this is really not covered
        /// by any of the above, but since there's a
        /// special SDL_KMOD_MODE for it I'm adding it here
        /// </summary>
        Mode = 257,

        /// <summary>
        /// Sleep
        /// </summary>
        Sleep = 258,
        
        /// <summary>
        /// Wake
        /// </summary>
        Wake = 259,

        /// <summary>
        /// Channel Increment
        /// </summary>
        ChannelIncrement = 260,
        
        /// <summary>
        /// Channel Decrement
        /// </summary>
        ChannelDecrement = 261,

        /// <summary>
        /// Play
        /// </summary>
        MediaPlay = 262,
        
        /// <summary>
        /// Pause
        /// </summary>
        MediaPause = 263,
        
        /// <summary>
        /// Record
        /// </summary>
        MediaRecord = 264,
        
        /// <summary>
        /// Fast Forward
        /// </summary>
        MediaFastForward = 265,
        
        /// <summary>
        /// Rewind
        /// </summary>
        MediaRewind = 266,
        
        /// <summary>
        /// Next Track
        /// </summary>
        MediaNextTrack = 267,
        
        /// <summary>
        /// Previous Track
        /// </summary>
        MediaPreviousTrack = 268,
        
        /// <summary>
        /// Stop
        /// </summary>
        MediaStop = 269,
        
        /// <summary>
        /// Eject
        /// </summary>
        MediaEject = 270,
        
        /// <summary>
        /// Play / Pause
        /// </summary>
        MediaPlayPause = 271,
        
        /// <summary>
        /// Media Select
        /// </summary>
        MediaSelect = 272,
        
        /// <summary>
        /// AC New
        /// </summary>
        ACNew = 273,
        
        /// <summary>
        /// AC Open
        /// </summary>
        ACOpen = 274,
        
        /// <summary>
        /// AC Close
        /// </summary>
        ACClose = 275,
        
        /// <summary>
        /// AC Exit
        /// </summary>
        ACExit = 276,
        
        /// <summary>
        /// AC Save
        /// </summary>
        ACSave = 277,
        
        /// <summary>
        /// AC Print
        /// </summary>
        ACPrint = 278,
        
        /// <summary>
        /// AC Properties
        /// </summary>
        ACProperties = 279,

        /// <summary>
        /// AC Search
        /// </summary>
        ACSearch = 280,
        
        /// <summary>
        /// AC Home
        /// </summary>
        ACHome = 281,
        
        /// <summary>
        /// AC Back
        /// </summary>
        ACBack = 282,
        
        /// <summary>
        /// AC Forward
        /// </summary>
        ACForward = 283,
        
        /// <summary>
        /// AC Stop
        /// </summary>
        ACStop = 284,
        
        /// <summary>
        /// AC Refresh
        /// </summary>
        ACRefresh = 285,
        
        /// <summary>
        /// AC Bookmarks
        /// </summary>
        ACBookmarks = 286,
        

        /// <summary>
        /// Usually situated below the display on phones and
        /// used as a multi-function feature key for selecting
        /// a software defined function shown on the bottom left
        /// of the display.
        /// </summary>
        SoftLeft = 287,
        
        /// <summary>
        /// Usually situated below the display on phones and
        /// used as a multi-function feature key for selecting
        /// a software defined function shown on the bottom right
        /// of the display.
        /// </summary>
        SoftRight = 288,
        
        /// <summary>
        /// Used for accepting phone calls.
        /// </summary>
        Call = 289,
        
        /// <summary>
        /// Used for rejecting phone calls.
        /// </summary>
        EndCall = 290,

        /// <summary>
        /// 400-500 reserved for dynamic keycodes
        /// </summary>
        Reserved = 400,

        /// <summary>
        /// not a key, just marks the number of scancodes for array bounds
        /// </summary>
        Count = 512
    }
}