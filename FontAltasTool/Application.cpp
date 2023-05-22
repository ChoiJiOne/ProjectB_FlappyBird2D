// @third party code - BEGIN
#include <stb_rect_pack.h>
#include <stb_truetype.h>
// @third party code - END

#include <iostream>
#include <vector>

#include "FileHelper.hpp"
#include "Glyph.h"
#include "INIFormat.h"
#include "INISection.h"


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
	INIFormat iniFormat;

	INISection section0;
	section0.AddData("A", "1");
	section0.AddData("B", "2");
	section0.AddData("C", "3");
	section0.AddData("D", "4");
	iniFormat.AddSection("x", section0);
	
	INISection section1;
	section1.AddData("E", "1");
	section1.AddData("F", "2");
	section1.AddData("G", "3");
	section1.AddData("H", "4");
	iniFormat.AddSection("y", section1);
	
	INIFormat::ExportINIFile("D:\\Work\\FlappyBird2D\\Content\\test.ini", iniFormat);
	return 0;
}