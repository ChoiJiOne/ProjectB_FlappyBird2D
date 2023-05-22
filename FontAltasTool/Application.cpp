// @third party code - BEGIN
#include <stb_rect_pack.h>
#include <stb_truetype.h>
#include <stb_image_write.h>
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
	std::string path = "D:\\Work\\FlappyBird2D\\Content\\Font\\SeoulNamsanEB.ttf";
	int32_t beginCodePoint = 32;
	int32_t endCodePoint = 127;
	float fontSize = 16.0f;
	std::vector<Glyph> glyphs(endCodePoint - beginCodePoint + 1);
	std::vector<stbtt_packedchar> packedchars(endCodePoint - beginCodePoint + 1);
	int32_t atlasSize = 0;

	std::vector<uint8_t> buffer;
	FileHelper::ReadBufferFromFile(path, buffer);
	
	stbtt_fontinfo fontInfo;
	CHECK((stbtt_InitFont(
		&fontInfo,
		reinterpret_cast<const unsigned char*>(&buffer[0]),
		stbtt_GetFontOffsetForIndex(reinterpret_cast<const unsigned char*>(&buffer[0]), 0)
	) != 0), "failed to initialize stb_truetype...");

	int32_t success = 0;
	stbtt_pack_context packContext;
	std::shared_ptr<uint8_t[]> bitmap = nullptr;

	for (int32_t size = 16; size < 8192; size *= 2)
	{
		bitmap = std::make_unique<uint8_t[]>(size * size);
		success = stbtt_PackBegin(&packContext, bitmap.get(), size, size, 0, 1, nullptr);
		stbtt_PackSetOversampling(&packContext, 1, 1);

		success = stbtt_PackFontRange(
			&packContext,
			reinterpret_cast<const unsigned char*>(&buffer[0]),
			0,
			fontSize,
			beginCodePoint,
			static_cast<int>(packedchars.size()),
			&packedchars[0]
		);

		if (success)
		{
			stbtt_PackEnd(&packContext);
			atlasSize = size;
			break;
		}
		else
		{
			stbtt_PackEnd(&packContext);
			bitmap.reset();
		}
	}

	for (std::size_t index = 0; index < packedchars.size(); ++index)
	{
		glyphs[index].codePoint = static_cast<int32_t>(index + beginCodePoint);

		glyphs[index].x0 = packedchars[index].x0;
		glyphs[index].y0 = packedchars[index].y0;
		glyphs[index].x1 = packedchars[index].x1;
		glyphs[index].y1 = packedchars[index].y1;

		glyphs[index].xoffset = packedchars[index].xoff;
		glyphs[index].yoffset = packedchars[index].yoff;

		glyphs[index].xoffset2 = packedchars[index].xoff2;
		glyphs[index].yoffset2 = packedchars[index].yoff2;

		glyphs[index].xadvance = packedchars[index].xadvance;
	}

	stbi_write_png("D:\\Work\\FlappyBird2D\\Content\\test.png", atlasSize, atlasSize, 1, bitmap.get(), atlasSize);

	return 0;
}