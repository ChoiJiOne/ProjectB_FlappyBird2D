using System.Collections.Generic;

/**
 * @brief 폰트 텍스처 아틀라스 내 문자 정보입니다.
 */
struct Glyph
{
    public int codePoint;
    public int x0;
    public int y0;
    public int x1;
    public int y1;
    public float xoffset;
    public float yoffset;
    public float xoffset2;
    public float yoffset2;
    public float xadvance;
}