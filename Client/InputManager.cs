using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SDL2;


/**
 * 입력 상태를 나타냅니다.
 * --------------------------------------
 * | 이전 프레임 | 현재 프레임 | 입력 상태 |
 * --------------------------------------
 * |     0      |     0      | NONE     |
 * |     0      |     1      | PRESSED  |
 * |     1      |     0      | RELEASED |
 * |     1      |     1      | HELD     |
 * --------------------------------------
 */
enum EPressState
{
    NONE     = 0x00,
	PRESSED  = 0x01,
	RELEASED = 0x02,
	HELD     = 0x03
};


/**
 * 마우스의 버튼의 종류입니다.
 * 
 * LEFT : 마우스의 왼쪽 버튼입니다.
 * RIGHT : 마우스의 오른쪽 버튼입니다.
 */
enum EMouseButton
{
    LEFT  = 0x0,
	RIGHT = 0x1,
};


/**
 * 가상 키 코드입니다.
 * @see https://wiki.libsdl.org/SDL_Scancode
 */
enum EVirtualKey
{
    CODE_UNKNOWN = 0,
    CODE_A = 4,
    CODE_B = 5,
    CODE_C = 6,
    CODE_D = 7,
    CODE_E = 8,
    CODE_F = 9,
    CODE_G = 10,
    CODE_H = 11,
    CODE_I = 12,
    CODE_J = 13,
    CODE_K = 14,
    CODE_L = 15,
    CODE_M = 16,
    CODE_N = 17,
    CODE_O = 18,
    CODE_P = 19,
    CODE_Q = 20,
    CODE_R = 21,
    CODE_S = 22,
    CODE_T = 23,
    CODE_U = 24,
    CODE_V = 25,
    CODE_W = 26,
    CODE_X = 27,
    CODE_Y = 28,
    CODE_Z = 29,
    CODE_1 = 30,
    CODE_2 = 31,
    CODE_3 = 32,
    CODE_4 = 33,
    CODE_5 = 34,
    CODE_6 = 35,
    CODE_7 = 36,
    CODE_8 = 37,
    CODE_9 = 38,
    CODE_0 = 39,
    CODE_RETURN = 40,
    CODE_ESCAPE = 41,
    CODE_BACKSPACE = 42,
    CODE_TAB = 43,
    CODE_SPACE = 44,
    CODE_MINUS = 45,
    CODE_EQUALS = 46,
    CODE_LEFTBRACKET = 47,
    CODE_RIGHTBRACKET = 48,
    CODE_BACKSLASH = 49,
    CODE_NONUSHASH = 50,
    CODE_SEMICOLON = 51,
    CODE_APOSTROPHE = 52,
    CODE_GRAVE = 53,
    CODE_COMMA = 54,
    CODE_PERIOD = 55,
    CODE_SLASH = 56,
    CODE_CAPSLOCK = 57,
	CODE_F1 = 58,
	CODE_F2 = 59,
	CODE_F3 = 60,
	CODE_F4 = 61,
	CODE_F5 = 62,
	CODE_F6 = 63,
	CODE_F7 = 64,
	CODE_F8 = 65,
	CODE_F9 = 66,
	CODE_F10 = 67,
	CODE_F11 = 68,
	CODE_F12 = 69,
	CODE_PRINTSCREEN = 70,
	CODE_SCROLLLOCK = 71,
	CODE_PAUSE = 72,
	CODE_INSERT = 73,
	CODE_HOME = 74,
	CODE_PAGEUP = 75,
	CODE_DELETE = 76,
	CODE_END = 77,
	CODE_PAGEDOWN = 78,
	CODE_RIGHT = 79,
	CODE_LEFT = 80,
	CODE_DOWN = 81,
	CODE_UP = 82,
	CODE_NUMLOCKCLEAR = 83,
	CODE_KP_DIVIDE = 84,
	CODE_KP_MULTIPLY = 85,
	CODE_KP_MINUS = 86,
	CODE_KP_PLUS = 87,
	CODE_KP_ENTER = 88,
	CODE_KP_1 = 89,
	CODE_KP_2 = 90,
	CODE_KP_3 = 91,
	CODE_KP_4 = 92,
	CODE_KP_5 = 93,
	CODE_KP_6 = 94,
	CODE_KP_7 = 95,
	CODE_KP_8 = 96,
	CODE_KP_9 = 97,
	CODE_KP_0 = 98,
	CODE_KP_PERIOD = 99,
	CODE_NONUSBACKSLASH = 100,
	CODE_APPLICATION = 101,
	CODE_POWER = 102,
    CODE_KP_EQUALS = 103,
    CODE_F13 = 104,
    CODE_F14 = 105,
    CODE_F15 = 106,
    CODE_F16 = 107,
    CODE_F17 = 108,
    CODE_F18 = 109,
    CODE_F19 = 110,
    CODE_F20 = 111,
    CODE_F21 = 112,
    CODE_F22 = 113,
    CODE_F23 = 114,
    CODE_F24 = 115,
    CODE_EXECUTE = 116,
    CODE_HELP = 117,
    CODE_MENU = 118,
    CODE_SELECT = 119,
    CODE_STOP = 120,
    CODE_AGAIN = 121,
    CODE_UNDO = 122,
    CODE_CUT = 123,
    CODE_COPY = 124,
    CODE_PASTE = 125,
    CODE_FIND = 126,
    CODE_MUTE = 127,
    CODE_VOLUMEUP = 128,
    CODE_VOLUMEDOWN = 129,
    CODE_KP_COMMA = 133,
    CODE_KP_EQUALSAS400 = 134,
    CODE_INTERNATIONAL1 = 135,
    CODE_INTERNATIONAL2 = 136,
    CODE_INTERNATIONAL3 = 137,
    CODE_INTERNATIONAL4 = 138,
    CODE_INTERNATIONAL5 = 139,
    CODE_INTERNATIONAL6 = 140,
    CODE_INTERNATIONAL7 = 141,
    CODE_INTERNATIONAL8 = 142,
    CODE_INTERNATIONAL9 = 143,
    CODE_LANG1 = 144, 
    CODE_LANG2 = 145, 
    CODE_LANG3 = 146, 
    CODE_LANG4 = 147,
    CODE_LANG5 = 148,
    CODE_LANG6 = 149, 
    CODE_LANG7 = 150,
    CODE_LANG8 = 151,
    CODE_LANG9 = 152, 
    CODE_ALTERASE = 153, 
    CODE_SYSREQ = 154,
    CODE_CANCEL = 155,  
    CODE_CLEAR = 156,
    CODE_PRIOR = 157,
    CODE_RETURN2 = 158,
    CODE_SEPARATOR = 159,
    CODE_OUT = 160,
    CODE_OPER = 161,
    CODE_CLEARAGAIN = 162,
    CODE_CRSEL = 163,
    CODE_EXSEL = 164,
    CODE_KP_00 = 176,
    CODE_KP_000 = 177,
    CODE_THOUSANDSSEPARATOR = 178,
    CODE_DECIMALSEPARATOR = 179,
    CODE_CURRENCYUNIT = 180,
    CODE_CURRENCYSUBUNIT = 181,
    CODE_KP_LEFTPAREN = 182,
    CODE_KP_RIGHTPAREN = 183,
    CODE_KP_LEFTBRACE = 184,
    CODE_KP_RIGHTBRACE = 185,
    CODE_KP_TAB = 186,
    CODE_KP_BACKSPACE = 187,
    CODE_KP_A = 188,
    CODE_KP_B = 189,
    CODE_KP_C = 190,
    CODE_KP_D = 191,
    CODE_KP_E = 192,
    CODE_KP_F = 193,
    CODE_KP_XOR = 194,
    CODE_KP_POWER = 195,
    CODE_KP_PERCENT = 196,
    CODE_KP_LESS = 197,
    CODE_KP_GREATER = 198,
    CODE_KP_AMPERSAND = 199,
    CODE_KP_DBLAMPERSAND = 200,
    CODE_KP_VERTICALBAR = 201,
    CODE_KP_DBLVERTICALBAR = 202,
    CODE_KP_COLON = 203,
    CODE_KP_HASH = 204,
    CODE_KP_SPACE = 205,
    CODE_KP_AT = 206,
    CODE_KP_EXCLAM = 207,
    CODE_KP_MEMSTORE = 208,
    CODE_KP_MEMRECALL = 209,
    CODE_KP_MEMCLEAR = 210,
    CODE_KP_MEMADD = 211,
    CODE_KP_MEMSUBTRACT = 212,
    CODE_KP_MEMMULTIPLY = 213,
    CODE_KP_MEMDIVIDE = 214,
    CODE_KP_PLUSMINUS = 215,
    CODE_KP_CLEAR = 216,
    CODE_KP_CLEARENTRY = 217,
    CODE_KP_BINARY = 218,
    CODE_KP_OCTAL = 219,
    CODE_KP_DECIMAL = 220,
    CODE_KP_HEXADECIMAL = 221,
    CODE_LCTRL = 224,
    CODE_LSHIFT = 225,
    CODE_LALT = 226,
    CODE_LGUI = 227,
    CODE_RCTRL = 228,
    CODE_RSHIFT = 229,
    CODE_RALT = 230,
    CODE_RGUI = 231,
    CODE_MODE = 257, 
    CODE_AUDIONEXT = 258,
    CODE_AUDIOPREV = 259,
    CODE_AUDIOSTOP = 260,
    CODE_AUDIOPLAY = 261,
    CODE_AUDIOMUTE = 262,
    CODE_MEDIASELECT = 263,
    CODE_WWW = 264,  
    CODE_MAIL = 265,
    CODE_CALCULATOR = 266, 
    CODE_COMPUTER = 267,
    CODE_AC_SEARCH = 268,  
    CODE_AC_HOME = 269,   
    CODE_AC_BACK = 270,     
    CODE_AC_FORWARD = 271, 
    CODE_AC_STOP = 272,     
    CODE_AC_REFRESH = 273,    
    CODE_AC_BOOKMARKS = 274,
    CODE_BRIGHTNESSDOWN = 275,
    CODE_BRIGHTNESSUP = 276,
    CODE_DISPLAYSWITCH = 277, 
    CODE_KBDILLUMTOGGLE = 278,
    CODE_KBDILLUMDOWN = 279,
    CODE_KBDILLUMUP = 280,
    CODE_EJECT = 281,
    CODE_SLEEP = 282,
    CODE_APP1 = 283,
    CODE_APP2 = 284,
    CODE_AUDIOREWIND = 285,
    CODE_AUDIOFASTFORWARD = 286,
    CODE_SOFTLEFT = 287,
    CODE_SOFTRIGHT = 288, 
    CODE_CALL = 289,
    CODE_ENDCALL = 290, 
    CODE_NUM_SCANCODES = 512 
};


/**
 * 윈도우 이벤트 키 값입니다.
 * https://wiki.libsdl.org/SDL_WindowEventID
 */
enum EWindowEvent
{
    NONE = 0,
    SHOWN = 1,
    HIDDEN = 2,
    EXPOSED = 3,
    MOVED = 4,
    RESIZED = 5,
    SIZE_CHANGED = 6,
    MINIMIZED = 7,
    MAXIMIZED = 8,
    RESTORED = 9,
    ENTER = 10,
    LEAVE = 11,
    FOCUS_GAINED = 12,
    FOCUS_LOST = 13,
    CLOSE = 14,
    TAKE_FOCUS = 15, 
    HIT_TEST = 16,
    ICCPROF_CHANGED = 17,
    DISPLAY_CHANGED = 18
};


/**
 * @brief 입력 처리를 수행하는 싱글턴 클래스입니다.
 */
class InputManager
{
    /**
     * @brief 입력 처리를 수행하는 매니저의 인스턴스를 얻습니다.
     * 
     * @return 입력 처리 매니저의 인스턴스를 반환합니다. 
     */
    public static InputManager Get()
    {
        if(inputManager_ == null)
        {
            inputManager_ = new InputManager();
        }

        return inputManager_;
    }


    /**
     * @brief 입력 처리 매너저를 초기화합니다.
     * 
     * @note 이전에 초기화 한 적이 있으면 아무 동작도 수행하지 않습니다.
     */
    public void Setup()
    {
        if (bIsSetup_) return;

        keyboardStateSize_ = 512;

        prevKeyboardState_ = new byte[keyboardStateSize_];
        currKeyboardState_ = new byte[keyboardStateSize_];

        bIsSetup_ = true;
    }


    /**
     * @brief 마우스의 위치에 대한 Getter입니다.
     */
    public Vector2<int> PrevCursorPosition
    {
        get => prevCursorPosition_;
    }

    public Vector2<int> CurrCursorPosition
    {
        get => currCursorPosition_;
    }


    /**
     * @brief 입력 상태를 업데이트합니다.
     */
    public void Tick()
    {
        SDL.SDL_Event eventState;

        while (SDL.SDL_PollEvent(out eventState) != 0)
        {
            EWindowEvent windowEvent = (EWindowEvent)eventState.window.windowEvent;

            if(windowEventActions_.ContainsKey(windowEvent))
            {
                windowEventActions_[windowEvent]();
            }
        }

        IntPtr keyboardStateBuffer = SDL.SDL_GetKeyboardState(out _);

        prevKeyboardState_ = (byte[])currKeyboardState_.Clone();
        Marshal.Copy(keyboardStateBuffer, currKeyboardState_, 0, keyboardStateSize_);

        prevCursorPosition_ = currCursorPosition_;
        prevButtonState_ = currButtonState_;

        currButtonState_ = SDL.SDL_GetMouseState(out currCursorPosition_.x, out currCursorPosition_.y);
    }


    /**
     * @brief 윈도우 이벤트가 동작할 액션을 바인딩합니다.
     * 
     * @note 이미 키 값에 대응하는 이벤트 액션이 존재하면 덮어 써집니다.
     * 
     * @param windowEvent 액션에 대응하는 윈도우 이벤트 키 값입니다.
     * @param eventAction 윈도우 이벤트가 감지될 경우 실행할 이벤트 액션입니다.
     */
    public void BindWindowEventAction(EWindowEvent windowEvent, Action eventAction)
    {
        if (windowEventActions_.ContainsKey(windowEvent))
        {
            windowEventActions_[windowEvent] = eventAction;
        }
        else
        {
            windowEventActions_.Add(windowEvent, eventAction);
        }
    }


    /**
     * @brief 윈도우 이벤트가 동작할 액션의 바인딩을 해제합니다.
     * 
     * @note 키 값에 대응하는 이벤트 액션이 존재하지 않으면 아무 동작도 수행하지 않습니다.
     * 
     * @param windowEvent 바인딩 해제할 윈도우 이벤트입니다.
     */
    public void UnbindWindowEventAction(EWindowEvent windowEvent)
    {
        if (windowEventActions_.ContainsKey(windowEvent))
        {
            windowEventActions_.Remove(windowEvent);
        }
    }


    /**
     * @brief 가상 키 코드의 입력 상태를 얻습니다.
     * 
     * @param virtualKey 입력 상태를 얻을 가상 키 코드 값입니다.
     * 
     * @return 가상 키 코드값의 입력 상태를 반환합니다.
     */
    public EPressState GetKeyPressState(EVirtualKey virtualKey)
    {
        EPressState pressState = EPressState.NONE;

        if (IsPressKey(ref prevKeyboardState_, virtualKey))
        {
            if (IsPressKey(ref currKeyboardState_, virtualKey))
            {
                pressState = EPressState.HELD;
            }
            else
            {
                pressState = EPressState.RELEASED;
            }
        }
        else
        {
            if (IsPressKey(ref currKeyboardState_, virtualKey))
            {
                pressState = EPressState.PRESSED;
            }
            else
            {
                pressState = EPressState.NONE;
            }
        }

        return pressState;
    }


    /**
     * @brief 마우스 버튼의 입력 상태를 얻습니다.
     * 
     * @param mouseButton 마우스 버튼의 타입입니다.
     * 
     * @return 마우스 버튼의 입력 상태를 반환합니다.
     */
    public EPressState GetMousePressState(EMouseButton mouseButton)
    {
        EPressState pressState = EPressState.NONE;

        if (IsPressMouseButton(prevButtonState_, mouseButton))
        {
            if (IsPressMouseButton(currButtonState_, mouseButton))
            {
                pressState = EPressState.HELD;
            }
            else
            {
                pressState = EPressState.RELEASED;
            }
        }
        else
        {
            if (IsPressMouseButton(currButtonState_, mouseButton))
            {
                pressState = EPressState.PRESSED;
            }
            else
            {
                pressState = EPressState.NONE;
            }
        }

        return pressState;
    }


    /**
     * @brief 가상 키가 눌렸는지 확인합니다.
     * 
     * @param keyboardState 가상 키들의 입력 상태입니다.
     * @param virtualKey 눌렸는지 확인할 가상 키입니다.
     * 
     * @return 특정 키가 눌렸다면 true, 그렇지 않으면 false를 반환합니다.
     */
    private bool IsPressKey(ref byte[] keyboardState, EVirtualKey virtualKey)
    {
        return keyboardState[(int)virtualKey] == 0 ? false : true;
    }


    /**
     * @brief 마우스 버튼이 눌려는지 확인합니다.
     * 
     * @param buttonState 마우스 버튼의 입력 상태입니다.
     * @param mouseButton 마우스 버튼의 타입입니다.
     * 
     * @return 특정 마우스 버튼이 눌렸다면 true, 그렇지 않으면 false를 반환합니다.
     */
    private bool IsPressMouseButton(uint buttonState, EMouseButton mouseButton)
    {
        uint mask = 0;

        switch(mouseButton)
        {
            case EMouseButton.LEFT:
                mask = SDL.SDL_BUTTON_LMASK;
                break;

            case EMouseButton.RIGHT:
                mask = SDL.SDL_BUTTON_RMASK;
                break;
        }

        return (buttonState & mask) == 0 ? false : true;
    }


    /**
     * @brief 생성자는 외부에서 호출할 수 없도록 감춥니다.
     */
    private InputManager() { }


    /**
     * @brief 입력 처리 매니저가 초기화된 적이 있는지 확입하니다.
     */
    private bool bIsSetup_ = false;


    /**
     * @brief Tick 호출 이전의 가상 키들의 상태입니다.
     */
    private byte[] prevKeyboardState_;


    /**
     * @brief Tick 호출 이후의 가상 키들의 상태입니다.
     */
    private byte[] currKeyboardState_;


    /**
     * @brief 가상 키들의 상태를 저장하는 배열의 크기입니다.
     */
    private int keyboardStateSize_;


    /**
     * @brief Tick 호출 이전의 마우스 위치입니다.
     */
    private Vector2<int> prevCursorPosition_;


    /**
     * @brief Tick 호출 이전의 마우스 버튼 상태입니다.
     */
    private uint prevButtonState_;


    /**
     * @brief Tick 호출 이후의 마우스 위치입니다.
     */
    private Vector2<int> currCursorPosition_;


    /**
     * @brief Tick 호출 이후의 마우스 버튼 상태입니다.
     */
    private uint currButtonState_;


    /**
     * @brief 윈도우 이벤트 키 값에 대응하는 실행 루틴입니다.
     */
    private Dictionary<EWindowEvent, Action> windowEventActions_ = new Dictionary<EWindowEvent, Action>();


    /**
     * @brief 입력 처리를 수행하는 매니저의 인스턴스입니다.
     */
    private static InputManager inputManager_;
}