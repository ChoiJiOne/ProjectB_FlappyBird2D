#pragma once

#include <cstdint>

/**
 * @brief 텍스처 아틀라스에 표시된 문자 정보입니다.
 */
struct Glyph
{
	int32_t codePoint;
	int32_t x0;
	int32_t y0;
	int32_t x1;
	int32_t y1;
	float xoffset;
	float yoffset;
	float xoffset2;
	float yoffset2;
	float xadvance;
};