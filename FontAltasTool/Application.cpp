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
		std::cout << "failed to generate font altas...\n";
		return -1;
	}

	
	
	return 0;
}