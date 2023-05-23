// @third party code - BEGIN
#include <stb_rect_pack.h>
#include <stb_truetype.h>
#include <stb_image_write.h>
// @third party code - END

#include <array>
#include <iostream>
#include <vector>

#include "CommandLine.h"
#include "FileHelper.hpp"
#include "StringHelper.hpp"
#include "Glyph.h"
#include "INIFormat.h"
#include "INISection.h"


/**
 * @brief 폰트 아틀라스 생성을 위한 명령행 인수가 유효한지 검사합니다.
 * 
 * @return 폰트 아틀라스 생성을 위한 명령행 인수가 유효하면 true, 그렇지 않으면 false를 반환합니다.
 */
bool IsValidArgumentForFontAtlas()
{
	std::array<std::string, 6> arguments = {
		"Crash",
		"FontPath",
		"BeginCodePoint",
		"EndCodePoint",
		"FontSize",
		"OutputPath",
	};

	for (const auto& argument : arguments)
	{
		if (!CommandLine::IsValid(argument))
		{
			std::cout << StringHelper::Format("[LOG] invalid %s argument for font atlas", argument.c_str());
			return false;
		}
	}

	return true;
}


/**
 * @brief 애플리케이션의 진입점입니다.
 *
 * @param argc 명령행 인자의 수입니다.
 * @param argv 명령행 인자입니다.
 *
 * @return 다른 프로그램에 전달할 수 있는 상태 코드를 반환합니다.
 */
int32_t main(int32_t argc, char** argv)
{
	CommandLine::Parse(argc, argv);

	if (!IsValidArgumentForFontAtlas())
	{
		std::cout << "[LOG] failed to generate font altas\n";
		return -1;
	}
	else
	{
		std::cout << "[LOG] successed to generate font altas\n";
	}
	
	return 0;
}