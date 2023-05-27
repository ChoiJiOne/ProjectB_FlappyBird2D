#pragma once

#include <cstdint>
#include <iostream>
#include <windows.h>

#include "Macro.h"


/**
 * @brief 로그 처리에 대한 헬퍼 기능을 수행합니다.
 * 
 * @note 이 클래스는 멤버 함수 모두가 정적인 정적(static) 클래스입니다.
 */
class Logger
{
public:
	/**
	 * @brief 로그의 레벨입니다.
	 * 
	 * @note
	 * 일반의 경우 흰색으로 표시됩니다.
	 * 경고의 경우 노란색으로 표시됩니다.
	 * 에러의 경우 빨간색으로 표시됩니다.
	 */
	enum class ELevel
	{
		NORMAL  = 0x00,
		WARNING = 0x01,
		ERR     = 0x02,
		SUCCESS = 0x03,
	};


public:
	/**
	 * @brief 로그를 화면에 출력합니다.
	 * 
	 * @param level 화면에 출력할 로그의 레벨입니다.
	 * @param message 화면에 출력할 로그 메시지입니다.
	 */
	static inline void Display(const ELevel& level, const std::string& message)
	{
		std::string logMessage;

		switch (level)
		{
		case ELevel::NORMAL:
			logMessage = "[NORMAL]";
			SetConsoleColor(normalMessageColor_);
			break;

		case ELevel::WARNING:
			logMessage = "[WARNING]";
			SetConsoleColor(warningMessageColor_);
			break;

		case ELevel::ERR:
			logMessage = "[ERR]";
			SetConsoleColor(errorMessageColor_);
			break;

		case ELevel::SUCCESS:
			logMessage = "[SUCCESS]";
			SetConsoleColor(successMessageColor_);
			break;

		default:
			ENFORCE_THROW_EXCEPTION("undedefined logging level...");
		}

		std::cout << logMessage << message << '\n';
		SetConsoleColor(EConsoleColor::WHITE);
	}


	/**
	 * @brief 로그를 화면에 출력합니다.
	 *
	 * @param level 화면에 출력할 로그의 레벨입니다.
	 * @param message 화면에 출력할 로그 메시지입니다.
	 */
	static inline void Display(const ELevel& level, const std::wstring& message)
	{
		std::wstring logMessage;

		switch (level)
		{
		case ELevel::NORMAL:
			logMessage = L"[NORMAL]";
			SetConsoleColor(normalMessageColor_);
			break;

		case ELevel::WARNING:
			logMessage = L"[WARNING]";
			SetConsoleColor(warningMessageColor_);
			break;

		case ELevel::ERR:
			logMessage = L"[ERR]";
			SetConsoleColor(errorMessageColor_);
			break;

		case ELevel::SUCCESS:
			logMessage = L"[SUCCESS]";
			SetConsoleColor(successMessageColor_);
			break;

		default:
			ENFORCE_THROW_EXCEPTION("undedefined logging level...");
		}

		std::wcout << logMessage << message << '\n';
		SetConsoleColor(EConsoleColor::WHITE);
	}


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


private:
	/**
	 * @brief 일반 로그 메시지의 텍스트 색상입니다.
	 */
	static const EConsoleColor normalMessageColor_ = EConsoleColor::WHITE;


	/**
	 * @brief 경고 메시지의 텍스트 색상입니다.
	 */
	static const EConsoleColor warningMessageColor_ = EConsoleColor::YELLOW;


	/**
	 * @brief 에러 메시지의 텍스트 색상입니다.
	 */
	static const EConsoleColor errorMessageColor_ = EConsoleColor::RED;


	/**
	 * @brief 성공 메시지의 텍스트 색상입니다.
	 */
	static const EConsoleColor successMessageColor_ = EConsoleColor::BLUE;
};