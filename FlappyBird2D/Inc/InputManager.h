#pragma once

#include "IManager.h"


/**
 * @brief 입력 상태를 나타냅니다.
 *
 * @note 프레임에 따른 입력 상태입니다.
 * --------------------------------------
 * | 이전 프레임 | 현재 프레임 | 입력 상태 |
 * --------------------------------------
 * |     0      |     0      | NONE     |
 * |     0      |     1      | PRESSED  |
 * |     1      |     0      | RELEASED |
 * |     1      |     1      | HELD     |
 * --------------------------------------
 */
enum class EPressState : int32_t
{
	None     = 0x00,
	Pressed  = 0x01,
	Released = 0x02,
	Held     = 0x03
};


/**
 * @brief 마우스의 버튼의 종류입니다.
 */
enum class EMouseButton : int32_t
{
	Left  = 0x0,
	Right = 0x1,
};


/**
 * @brief 가상 키 코드입니다.
 *
 * @see https://wiki.libsdl.org/SDL_Scancode
 */
enum class EVirtualKey : int32_t
{
	Vk_UNKNOWN = 0,
	Vk_A = 4,
	Vk_B = 5,
	Vk_C = 6,
	Vk_D = 7,
	Vk_E = 8,
	Vk_F = 9,
	Vk_G = 10,
	Vk_H = 11,
	Vk_I = 12,
	Vk_J = 13,
	Vk_K = 14,
	Vk_L = 15,
	Vk_M = 16,
	Vk_N = 17,
	Vk_O = 18,
	Vk_P = 19,
	Vk_Q = 20,
	Vk_R = 21,
	Vk_S = 22,
	Vk_T = 23,
	Vk_U = 24,
	Vk_V = 25,
	Vk_W = 26,
	Vk_X = 27,
	Vk_Y = 28,
	Vk_Z = 29,
	Vk_1 = 30,
	Vk_2 = 31,
	Vk_3 = 32,
	Vk_4 = 33,
	Vk_5 = 34,
	Vk_6 = 35,
	Vk_7 = 36,
	Vk_8 = 37,
	Vk_9 = 38,
	Vk_0 = 39,
	Vk_RETURN = 40,
	Vk_ESCAPE = 41,
	Vk_BACKSPACE = 42,
	Vk_TAB = 43,
	Vk_SPACE = 44,
	Vk_MINUS = 45,
	Vk_EQUALS = 46,
	Vk_LEFTBRACKET = 47,
	Vk_RIGHTBRACKET = 48,
	Vk_BACKSLASH = 49,
	Vk_NONUSHASH = 50,
	Vk_SEMICOLON = 51,
	Vk_APOSTROPHE = 52,
	Vk_GRAVE = 53,
	Vk_COMMA = 54,
	Vk_PERIOD = 55,
	Vk_SLASH = 56,
	Vk_CAPSLOCK = 57,
	Vk_F1 = 58,
	Vk_F2 = 59,
	Vk_F3 = 60,
	Vk_F4 = 61,
	Vk_F5 = 62,
	Vk_F6 = 63,
	Vk_F7 = 64,
	Vk_F8 = 65,
	Vk_F9 = 66,
	Vk_F10 = 67,
	Vk_F11 = 68,
	Vk_F12 = 69,
	Vk_PRINTSCREEN = 70,
	Vk_SCROLLLOCK = 71,
	Vk_PAUSE = 72,
	Vk_INSERT = 73,
	Vk_HOME = 74,
	Vk_PAGEUP = 75,
	Vk_DELETE = 76,
	Vk_END = 77,
	Vk_PAGEDOWN = 78,
	Vk_RIGHT = 79,
	Vk_LEFT = 80,
	Vk_DOWN = 81,
	Vk_UP = 82,
	Vk_NUMLOCKCLEAR = 83,
	Vk_KP_DIVIDE = 84,
	Vk_KP_MULTIPLY = 85,
	Vk_KP_MINUS = 86,
	Vk_KP_PLUS = 87,
	Vk_KP_ENTER = 88,
	Vk_KP_1 = 89,
	Vk_KP_2 = 90,
	Vk_KP_3 = 91,
	Vk_KP_4 = 92,
	Vk_KP_5 = 93,
	Vk_KP_6 = 94,
	Vk_KP_7 = 95,
	Vk_KP_8 = 96,
	Vk_KP_9 = 97,
	Vk_KP_0 = 98,
	Vk_KP_PERIOD = 99,
	Vk_NONUSBACKSLASH = 100,
	Vk_APPLICATION = 101,
	Vk_POWER = 102,
	Vk_KP_EQUALS = 103,
	Vk_F13 = 104,
	Vk_F14 = 105,
	Vk_F15 = 106,
	Vk_F16 = 107,
	Vk_F17 = 108,
	Vk_F18 = 109,
	Vk_F19 = 110,
	Vk_F20 = 111,
	Vk_F21 = 112,
	Vk_F22 = 113,
	Vk_F23 = 114,
	Vk_F24 = 115,
	Vk_EXECUTE = 116,
	Vk_HELP = 117,
	Vk_MENU = 118,
	Vk_SELECT = 119,
	Vk_STOP = 120,
	Vk_AGAIN = 121,
	Vk_UNDO = 122,
	Vk_CUT = 123,
	Vk_COPY = 124,
	Vk_PASTE = 125,
	Vk_FIND = 126,
	Vk_MUTE = 127,
	Vk_VOLUMEUP = 128,
	Vk_VOLUMEDOWN = 129,
	Vk_KP_COMMA = 133,
	Vk_KP_EQUALSAS400 = 134,
	Vk_INTERNATIONAL1 = 135,
	Vk_INTERNATIONAL2 = 136,
	Vk_INTERNATIONAL3 = 137,
	Vk_INTERNATIONAL4 = 138,
	Vk_INTERNATIONAL5 = 139,
	Vk_INTERNATIONAL6 = 140,
	Vk_INTERNATIONAL7 = 141,
	Vk_INTERNATIONAL8 = 142,
	Vk_INTERNATIONAL9 = 143,
	Vk_LANG1 = 144,
	Vk_LANG2 = 145,
	Vk_LANG3 = 146,
	Vk_LANG4 = 147,
	Vk_LANG5 = 148,
	Vk_LANG6 = 149,
	Vk_LANG7 = 150,
	Vk_LANG8 = 151,
	Vk_LANG9 = 152,
	Vk_ALTERASE = 153,
	Vk_SYSREQ = 154,
	Vk_CANCEL = 155,
	Vk_CLEAR = 156,
	Vk_PRIOR = 157,
	Vk_RETURN2 = 158,
	Vk_SEPARATOR = 159,
	Vk_OUT = 160,
	Vk_OPER = 161,
	Vk_CLEARAGAIN = 162,
	Vk_CRSEL = 163,
	Vk_EXSEL = 164,
	Vk_KP_00 = 176,
	Vk_KP_000 = 177,
	Vk_THOUSANDSSEPARATOR = 178,
	Vk_DECIMALSEPARATOR = 179,
	Vk_CURRENCYUNIT = 180,
	Vk_CURRENCYSUBUNIT = 181,
	Vk_KP_LEFTPAREN = 182,
	Vk_KP_RIGHTPAREN = 183,
	Vk_KP_LEFTBRACE = 184,
	Vk_KP_RIGHTBRACE = 185,
	Vk_KP_TAB = 186,
	Vk_KP_BACKSPACE = 187,
	Vk_KP_A = 188,
	Vk_KP_B = 189,
	Vk_KP_C = 190,
	Vk_KP_D = 191,
	Vk_KP_E = 192,
	Vk_KP_F = 193,
	Vk_KP_XOR = 194,
	Vk_KP_POWER = 195,
	Vk_KP_PERCENT = 196,
	Vk_KP_LESS = 197,
	Vk_KP_GREATER = 198,
	Vk_KP_AMPERSAND = 199,
	Vk_KP_DBLAMPERSAND = 200,
	Vk_KP_VERTICALBAR = 201,
	Vk_KP_DBLVERTICALBAR = 202,
	Vk_KP_COLON = 203,
	Vk_KP_HASH = 204,
	Vk_KP_SPACE = 205,
	Vk_KP_AT = 206,
	Vk_KP_EXCLAM = 207,
	Vk_KP_MEMSTORE = 208,
	Vk_KP_MEMRECALL = 209,
	Vk_KP_MEMCLEAR = 210,
	Vk_KP_MEMADD = 211,
	Vk_KP_MEMSUBTRACT = 212,
	Vk_KP_MEMMULTIPLY = 213,
	Vk_KP_MEMDIVIDE = 214,
	Vk_KP_PLUSMINUS = 215,
	Vk_KP_CLEAR = 216,
	Vk_KP_CLEARENTRY = 217,
	Vk_KP_BINARY = 218,
	Vk_KP_OCTAL = 219,
	Vk_KP_DECIMAL = 220,
	Vk_KP_HEXADECIMAL = 221,
	Vk_LCTRL = 224,
	Vk_LSHIFT = 225,
	Vk_LALT = 226,
	Vk_LGUI = 227,
	Vk_RCTRL = 228,
	Vk_RSHIFT = 229,
	Vk_RALT = 230,
	Vk_RGUI = 231,
	Vk_MODE = 257,
	Vk_AUDIONEXT = 258,
	Vk_AUDIOPREV = 259,
	Vk_AUDIOSTOP = 260,
	Vk_AUDIOPLAY = 261,
	Vk_AUDIOMUTE = 262,
	Vk_MEDIASELECT = 263,
	Vk_WWW = 264,
	Vk_MAIL = 265,
	Vk_CALCULATOR = 266,
	Vk_COMPUTER = 267,
	Vk_AC_SEARCH = 268,
	Vk_AC_HOME = 269,
	Vk_AC_BACK = 270,
	Vk_AC_FORWARD = 271,
	Vk_AC_STOP = 272,
	Vk_AC_REFRESH = 273,
	Vk_AC_BOOKMARKS = 274,
	Vk_BRIGHTNESSDOWN = 275,
	Vk_BRIGHTNESSUP = 276,
	Vk_DISPLAYSWITCH = 277,
	Vk_KBDILLUMTOGGLE = 278,
	Vk_KBDILLUMDOWN = 279,
	Vk_KBDILLUMUP = 280,
	Vk_EJECT = 281,
	Vk_SLEEP = 282,
	Vk_APP1 = 283,
	Vk_APP2 = 284,
	Vk_AUDIOREWIND = 285,
	Vk_AUDIOFASTFORWARD = 286,
	Vk_SOFTLEFT = 287,
	Vk_SOFTRIGHT = 288,
	Vk_CALL = 289,
	Vk_ENDCALL = 290,
	Vk_NUM_SCANCODES = 512
};


/**
 * @brief 입력 처리를 수행하는 매니저입니다.
 * 
 * @note 이 클래스는 싱글턴입니다.
 */
class InputManager : public IManager
{
public:
	/**
	 * @brief 입력 처리를 수행하는 매니저의 복사 생성자와 대입 연산자를 명시적으로 삭제합니다.
	 */
	DISALLOW_COPY_AND_ASSIGN(InputManager);


	/**
	 * @brief 입력 처리를 수행하는 매니저의 참조자를 얻습니다.
	 * 
	 * @return 입력 처리를 수행하는 매니저의 참조자를 반환합니다.
	 */
	static InputManager& Get();


	/**
	 * @brief 입력 처리를 수행하는 매니저의 사용을 시작합니다.
	 *
	 * @note 이 메서드는 한 번만 호출되어야 합니다.
	 */
	virtual void Startup() override;


	/**
	 * @brief 입력 처리를 수행하는 매니저의 사용을 종료합니다.
	 *
	 * @note
	 * - 애플리케이션 종료 전에 이 메서드를 반드시 호출하여 내부 리소스를 정리해야 합니다.
	 * - 이 메서드는 반드시 한 번만 호출되어야 합니다.
	 */
	virtual void Shutdown() override;


private:
	/**
	 * @brief 입력 처리를 수행하는 매니저에 디폴트 생성자와 빈 가상 소멸자를 삽입합니다.
	 */
	DEFAULT_CONSTRUCTOR_AND_VIRTUAL_DESTRUCTOR(InputManager);


private:

};