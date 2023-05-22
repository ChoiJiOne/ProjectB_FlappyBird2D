// @third party code - BEGIN
#include <stb_rect_pack.h>
#include <stb_truetype.h>
// @third party code - END

#include <iostream>
#include <vector>

#include "FileHelper.hpp"
#include "Glyph.h"
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
	INISection section;
	section.AddData("A", "1");
	section.AddData("B", "2");
	section.AddData("C", "3");
	section.AddData("D", "4");

	auto sectionData0 = section.GetSectionData();
	for (auto data : sectionData0)
	{
		std::cout << data.first << ", " << data.second << std::endl;
	}

	auto data1 = section.GetData("A");
	std::cout << data1.first << ", " << data1.second << std::endl;

	std::string data2 = section.GetValue("B");
	std::cout << data2 << std::endl;

	section.RemoveData("C");
	auto sectionData1 = section.GetSectionData();
	for (auto data : sectionData1)
	{
		std::cout << data.first << ", " << data.second << std::endl;
	}

	return 0;
}