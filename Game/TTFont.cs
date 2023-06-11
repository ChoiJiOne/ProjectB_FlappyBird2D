using System;
using SDL2;
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


/**
 * @brief 트루 타입 폰트의 문자 정보 및 텍스처 아틀라스를 관리합니다.
 */
class TTFont : IContent
{
    /**
     * @brief 트루 타입 폰트의 글리프 정보와 텍스처 아틀라스를 로딩합니다.
     * 
     * @param glyphPath 글리프 정보를 포함한 ini 파일 경로입니다.
     * @param textureAtlasPath 트루 타입 폰트의 텍스처 아틀라스 경로입니다.
     * 
     * @thorws
     * - 글리프 정보 파일이 ini 파일이 아니라면 예외를 던집니다.
     * - 글리프 정보 파일이 유효하지 않으면 예외를 던집니다.
     * - 트루 타입 폰트의 텍스처 아틀라스 리소스 로딩에 실패하면 예외를 던집니다.
     * - 트루 타입 폰트의 텍스처 아틀라스 리소스 생성에 실패하면 예외를 던집니다.
     */
    public TTFont(string glyphPath, string textureAtlasPath)
    {

    }

    
    /**
     * @brief 트루 타입 폰트의 데이터를 명시적으로 정리합니다.
     */
    public void Release()
    {
        if (textureAtlas_ != IntPtr.Zero)
        {
            SDL.SDL_DestroyTexture(textureAtlas_);
        }
    }


    /**
     * @brief 코드 포인트의 시작점입니다.
     */
    private int beginCodePoint_;


    /**
     * @brief 코드 포인트의 끝점입니다.
     */
    private int endCodePoint_;


    /**
     * @brief 텍스처 아틀라스의 크기입니다.
     * 
     * @note 텍스처 아틀라스의 가로 세로 크기는 동일합니다.
     */
    private int textureAtlasSize_;


    /**
     * @brief 텍스처 아틀라스의 리소스입니다.
     */
    private IntPtr textureAtlas_;


    /**
     * @brief 폰트의 크기입니다.
     */
    private float fontSize_;
}