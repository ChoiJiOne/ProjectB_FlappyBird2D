#pragma once

#include <cstdint>
#include <windows.h>

#include "Macro.h"


/**
 * @brief 로그 처리에 대한 헬퍼 기능을 수행합니다.
 * 
 * @note 이 클래스는 멤버 함수 모두가 정적인 정적(static) 클래스입니다.
 */
class Logger
{
private:
	/**
	 * @brief 콘솔의 텍스트 컬러입니다.
	 */
	enum class EConsoleColor : int32_t
	{
		BLACK       = 0,
		BLUE        = 1,
		GREEN       = 2,
		AQUA        = 3,
		RED         = 4,
		PURPLE      = 5,
		YELLOW      = 6,
		WHITE       = 7,
		GRAY        = 8,
		LIGHTBLUE   = 9,
		LIGHTGREEN  = 10,
		LIGHTAQUA   = 11,
		LIGHTRED    = 12,
		LIGHTPURPLE = 13,
		LIGHTYELLOW = 14,
		BRIGHTWHITE = 15
	};


private:
	/**
	 * @brief 콘솔 창의 텍스트 색상을 설정합니다.
	 *
	 * @param consoleColor 설정할 색상입니다.
	 *
	 * @throws 설정에 실패하면 C++ 표준 예외를 던집니다.
	 */
	static inline void SetConsoleColor(const EConsoleColor& consoleColor)
	{
		CHECK(SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), static_cast<WORD>(consoleColor)), "failed to set text color...");
	}
};