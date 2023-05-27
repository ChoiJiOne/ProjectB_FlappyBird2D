// @third party code - BEGIN
#include <stb_rect_pack.h>
#include <stb_truetype.h>
#include <stb_image_write.h>
// @third party code - END

#include <array>
#include <iostream>
#include <sstream>
#include <vector>

#include "CommandLine.h"
#include "FileHelper.hpp"
#include "Glyph.h"
#include "INIFormat.h"
#include "INISection.h"
#include "Logger.hpp"
#include "StringHelper.hpp"

/**
 * @brief 폰트 아틀라스 생성을 위한 명령행 인수가 모두 전달되었는지 검사합니다.
 * 
 * @return 폰트 아틀라스 생성을 위한 명령행 인수가 모두 전달되었다면 true, 그렇지 않으면 false를 반환합니다.
 */
bool IsPassArgumentForFontAtlas()
{
	std::array<std::string, 5> arguments = {
		"FontPath",
		"BeginCodePoint",
		"EndCodePoint",
		"FontSize",
		"OutputPath",
	};

	for (const auto& argument : arguments)
	{
		if (CommandLine::IsValid(argument))
		{
			Logger::Display(Logger::ELevel::NORMAL, StringHelper::Format("confirming the pass of parameter %s... => VALID!", argument.c_str()));
		}
		else
		{
			Logger::Display(Logger::ELevel::ERR, StringHelper::Format("confirming the pass of parameter %s... => INVALID!", argument.c_str()));
			return false;
		}
	}

	return true;
}

/**
 * @brief 폰트 아틀라스 생성을 위한 명령행 인수가 유효한지 검사합니다.
 * 
 * @param outFontPath[out] 트루 타입 폰트의 경로입니다.
 * @param outBeginCodePoint[out] 코드 포인트의 시작점입니다.
 * @param outEndCodePoint[out] 코드 포인트의 끝점입니다.
 * @param outFontSize[out] 폰트의 크기입니다.
 * @param outOutputPath[out] 폰트 아틀라스의 출력 경로입니다.
 * 
 * @return 폰트 아틀라스 생성을 위한 명령행 인수가 유효하면 true, 그렇지 않으면 false를 반환합니다.
 */
bool IsValidArgumentForFontAtlas(
	std::string& outFontPath,
	int32_t& outBeginCodePoint,
	int32_t& outEndCodePoint,
	float& outFontSize,
	std::string& outOutputPath
)
{
	std::string fontPath = CommandLine::GetValue("FontPath");
	if (fontPath.find(".ttf") == std::string::npos)
	{
		Logger::Display(Logger::ELevel::ERR, StringHelper::Format("%s is not true type font file...", fontPath.c_str()));
		return false;
	}
	else
	{
		Logger::Display(Logger::ELevel::NORMAL, StringHelper::Format("%s is true type font file...", fontPath.c_str()));
		outFontPath = fontPath;
	}

	int32_t beginCodePoint = 0;
	std::stringstream beginCodePointStream(CommandLine::GetValue("BeginCodePoint"));
	beginCodePointStream >> beginCodePoint;
	if (beginCodePointStream.fail() || beginCodePoint < 0)
	{
		Logger::Display(Logger::ELevel::ERR, StringHelper::Format("invalid begin code point => %d...", beginCodePoint));
		return false;
	}
	else
	{
		Logger::Display(Logger::ELevel::NORMAL, StringHelper::Format("valid begin code point => %d...", beginCodePoint));
		outBeginCodePoint = beginCodePoint;
	}

	int32_t endCodePoint = 0;
	std::stringstream endCodePointStream(CommandLine::GetValue("EndCodePoint"));
	endCodePointStream >> endCodePoint;
	if (endCodePointStream.fail() || endCodePoint < 0)
	{
		Logger::Display(Logger::ELevel::ERR, StringHelper::Format("invalid end code point => %d...", endCodePoint));
		return false;
	}
	else
	{
		Logger::Display(Logger::ELevel::NORMAL, StringHelper::Format("valid end code point => %d...", endCodePoint));
		outEndCodePoint = endCodePoint;
	}

	if (beginCodePoint > endCodePoint)
	{
		Logger::Display(Logger::ELevel::WARNING, StringHelper::Format("BeginCodePoint is bigger than EndCodePoint..."));
		Logger::Display(Logger::ELevel::WARNING, StringHelper::Format("swap BeginCodePoint and EndCodePoint..."));
		outBeginCodePoint = endCodePoint;
		outEndCodePoint = beginCodePoint;
	}
	
	float fontSize = 0.0f;
	std::stringstream fontSizeStream(CommandLine::GetValue("FontSize"));
	fontSizeStream >> fontSize;
	if (fontSizeStream.fail() || fontSize < 0.0f)
	{
		Logger::Display(Logger::ELevel::ERR, StringHelper::Format("invalid font size => %f...", fontSize));
		return false;
	}
	else
	{
		Logger::Display(Logger::ELevel::NORMAL, StringHelper::Format("valid font size => %f...", fontSize));
		outFontSize = fontSize;
	}

	std::string outputPath = CommandLine::GetValue("OutputPath");
	if (!FileHelper::IsValidDirectory(outputPath))
	{
		Logger::Display(Logger::ELevel::ERR, StringHelper::Format("invalid output path => %s...", outputPath.c_str()));
		return false;
	}
	else
	{
		Logger::Display(Logger::ELevel::NORMAL, StringHelper::Format("valid output path => %s...", outputPath.c_str()));
		outOutputPath = outputPath;
	}
	
	return true;
}

/**
 * @brief 폰트 아틀라스를 생성합니다.
 * 
 * @note 
 * 코드 포인트의 범위는 시작과 끝을 포함합니다.
 * 폰트 아틀라스 비트맵의 가로 세로 크기는 동일합니다. 
 * 
 * @param fontPath 트루 타입 폰트 리소스의 경로입니다.
 * @param beginCodePoint 코드 포인트의 시작입니다.
 * @param endCodePoint 코드 포인트의 끝입니다.
 * @param fontSize 폰트 아틀라스 내 문자의 크기입니다.
 * @param outGlyphs[out] 폰트 아틀라스 내 문자들의 위치 및 크기입니다.
 * @param outAtlasBitmapBuffer[out] 폰트 아틀라스의 비트맵 버퍼입니다.
 * @param outAtlasBitmapSize[out] 폰트 아틀라스 비트맵의 크기입니다.
 * 
 * @return 폰트 아틀라스 생성에 성공하면 true, 그렇지 않으면 false를 반환합니다. 
 */
bool GenerateFontAtlas(
	const std::string& fontPath,
	int32_t beginCodePoint,
	int32_t endCodePoint,
	float fontSize,
	std::vector<Glyph>& outGlyphs,
	std::vector<uint8_t>& outAtlasBitmapBuffer,
	int32_t& outAltasBitmapSize
)
{



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

	if (!IsPassArgumentForFontAtlas())
	{
		Logger::Display(Logger::ELevel::ERR, "font atlas argument is missing...");
		return -1;
	}

	std::string fontPath;
	int32_t beginCodePoint;
	int32_t endCodePoint;
	float fontSize;
	std::string outputPath;

	if (!IsValidArgumentForFontAtlas(fontPath, beginCodePoint, endCodePoint, fontSize, outputPath))
	{
		Logger::Display(Logger::ELevel::ERR, "font atlas argument is invalid...");
		return -1;
	}

	std::vector<Glyph> glyphs;
	std::vector<uint8_t> atlasBitmapBuffer;
	int32_t altasBitmapSize;

	if (!GenerateFontAtlas(fontPath, beginCodePoint, endCodePoint, fontSize, glyphs, atlasBitmapBuffer, altasBitmapSize))
	{
		Logger::Display(Logger::ELevel::ERR, "failed to generate font atlas...");
	}

	return 0;
}