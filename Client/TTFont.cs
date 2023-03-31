using System;
using System.Collections.Generic;
using SDL2;


/**
 * @brief 텍스처 아틀라스에 표시될 문자 정보입니다.
 */
struct Glyph
{
    public ushort codePoint;
    public int x;
    public int y;
    public int width;
    public int height;
    public int xoffset;
    public int yoffset;
    public int xadvance;
}


/**
 * @brief 텍스처 아틀라스의 크기입니다.
 * 
 * @see https://docs.unrealengine.com/4.27/en-US/RenderingAndGraphics/Textures/SupportAndSettings/
 */
enum EResolution
{
    SIZE_16 = 16,
    SIZE_32 = 32,
    SIZE_64 = 64,
    SIZE_128 = 128,
    SIZE_256 = 256,
    SIZE_512 = 512,
    SIZE_1024 = 1024,
    SIZE_2048 = 2048,
    SIZE_4096 = 4096,
    SIZE_8192 = 8192,
}


/**
 * @brief 트루 타입 폰트를 로딩하고 폰트 텍스처 아틀라스를 생성합니다.
 * 
 * @note 텍스처 아틀라스의 크기는 정사각형입니다.
 */
class TTFont : IContent
{
    /**
     * @brief 트루 타입 폰트를 로딩하고 폰트 텍스처 아틀라스를 생성하는 클래스의 생성자입니다.
     * 
     * @note 텍스처 아틀라스에 표시될 문자는 시작 문자와 끝 문자를 포함합니다.
     * 
     * @param path 트루 타입 폰트 리소스의 경로입니다.
     * @param beginCodePoint 텍스처 아틀라스에 표시할 시작 문자입니다.
     * @param endCodePoint 텍스처 아틀라스에 표시할 끝 문자입니다.
     * @param size 폰트의 크기입니다.
     * 
     * @throws
     * - 트루 타입 폰트 파일 로딩에 실패하면 예외를 던집니다.
     * - 폰트 텍스처 아틀라스 생성에 실패하면 예외를 던집니다.
     */
    public TTFont(string path, ushort beginCodePoint, ushort endCodePoint, int size)
    {
        beginCodePoint_ = beginCodePoint;
        endCodePoint_ = endCodePoint;

        IntPtr font = SDL_ttf.TTF_OpenFont(path, size);

        if(font == IntPtr.Zero)
        {
            throw new Exception("failed to open true type font...");
        }

        EResolution resolution = TryFindTextureAtlasSize(size, endCodePoint_ - beginCodePoint_ + 1);
        textureAtlas_ = CreateTextureAtlas(font, resolution);
        
        SDL_ttf.TTF_CloseFont(font);
    }


    /**
     * @brief 트루 타입 폰트를 로딩하고 폰트 텍스처 아틀라스를 생성하는 클래스의 Getter입니다.
     */
    public ushort BeginCodePoint
    {
        get => beginCodePoint_;
    }

    public ushort EndCodePoint
    {
        get => endCodePoint_;
    }

    public IntPtr TextureAtlas
    {
        get => textureAtlas_;
    }


    /**
     * @brief 해당 코드 포인트를 소유하고 있는지 검사합니다.
     * 
     * @param codePoint 검사를 수행할 코드 포인트입니다.
     * 
     * @return 폰트가 입력 받은 코드 포인트를 소유하고 있다면 true, 그렇지 않으면 false를 반환합니다.
     */
    public bool HaveCodePoint(ushort codePoint)
    {
        return (beginCodePoint_ <= codePoint && codePoint <= endCodePoint_);
    }


    /**
     * @brief 코드 포인트에 대응하는 글리프 정보를 얻습니다.
     * 
     * @param codePoint 글리프 정보를 얻을 코드 포인트입니다.
     * 
     * @throws 코드 포인트에 대응하는 글리프 정보가 없으면 예외를 던집니다.
     * 
     * @return 코드 포인트에 대응하는 글리프 정보를 반환합니다.
     */
    public Glyph GetGlyph(ushort codePoint)
    {
        if(!HaveCodePoint(codePoint))
        {
            throw new Exception("can't find glyph info from code point...");
        }

        int index = (int)(codePoint - beginCodePoint_);
        return glyphs_[index];
    }


    /**
     * @brief 문자열의 크기를 측정합니다.
     * 
     * @param text 측정할 문자열입니다.
     * @param width[out] 문자열의 가로 크기입니다.
     * @param height[out] 문자열의 세로 크기입니다.
     * @param padding 문자열 사이의 간격입니다. 기본 값은 2입니다.
     * 
     * @return 측정에 성공하면 true, 그렇지 않으면 false를 반환합니다.
     */
    public bool MeasureText(string text, out int width, out int height)
    {
        width = 0;
        height = -1;

        for(int index = 0; index < text.Length; ++index)
        {
            if(!HaveCodePoint((ushort)text[index]))
            {
                return false;
            }

            Glyph glyph = GetGlyph((ushort)text[index]);

            width += glyph.xadvance;
            height = Math.Max(height, glyph.height);
        }

        return true;
    }


    /**
     * @brief 생성된 텍스처 아틀라스 리소스를 정리합니다.
     */
    public void Release()
    {
        if (textureAtlas_ != IntPtr.Zero)
        {
            SDL.SDL_DestroyTexture(textureAtlas_);
        }
    }


    /**
     * @brief 텍스처 아틀라스를 생성할 수 있는지 확인합니다.
     * 
     * @param fontSize 폰트의 크기입니다.
     * @param resolution 텍스처 아틀라스의 크기입니다.
     * @param countCodePoint 텍스처 아틀라스에 표시할 코드 포인트의 수입니다.
     */
    private bool CanGenerateTextureAtlas(int fontSize, EResolution resolution, int countCodePoint)
    {
        bool bCanGenerateTextureAtlas = true;

        int atlasX = 0, atlasY = 0;
        int atlasSize = (int)resolution;

        for (int count = 0; count < countCodePoint; ++count)
        {
            if(atlasY + fontSize > atlasSize)
            {
                bCanGenerateTextureAtlas = false;
                break;
            }

            if(atlasX + fontSize <= atlasSize)
            {
                atlasX += fontSize;
            }
            else
            {
                atlasX = 0;
                atlasY += fontSize;
            }
        }
        
        return bCanGenerateTextureAtlas;
    }


    /**
     * @brief 텍스처 아틀라스의 크기를 찾습니다.
     * 
     * @param fontSize 폰트의 크기입니다.
     * @param countCodePoint 텍스처 아틀라스에 표시할 코드 포인트의 수입니다.
     * 
     * @throws 텍스처 아틀라스의 크기를 찾는 데 실패하면 예외를 던집니다.
     */
    private EResolution TryFindTextureAtlasSize(int fontSize, int countCodePoint)
    {
        EResolution[] resolutions =
{
            EResolution.SIZE_16,
            EResolution.SIZE_32,
            EResolution.SIZE_64,
            EResolution.SIZE_128,
            EResolution.SIZE_256,
            EResolution.SIZE_512,
            EResolution.SIZE_1024,
            EResolution.SIZE_2048,
            EResolution.SIZE_4096,
            EResolution.SIZE_8192,
        };

        bool bIsFindResolution = false;
        EResolution targetResolution = EResolution.SIZE_16;

        foreach (EResolution resolution in resolutions)
        {
            if (CanGenerateTextureAtlas(fontSize, resolution, countCodePoint))
            {
                targetResolution = resolution;
                bIsFindResolution = true;
                break;
            }
        }

        if (!bIsFindResolution)
        {
            throw new Exception("failed to find texture atlas resolution...");
        }

        return targetResolution;
    }


    /**
     * @brief 텍스처 아틀라스를 생성합니다.
     * 
     * @param font 트루 타입 폰트 리소스입니다.
     * @param resolution 텍스처 아틀라스의 크기입니다.
     * 
     * @throws 텍스처 아틀라스 생성에 실패하면 예외를 던집니다.
     * 
     * @return 텍스처 아틀라스의 포인터를 반환합니다.
     */
    private IntPtr CreateTextureAtlas(IntPtr font, EResolution resolution)
    {
        int atlasX = 0;
        int atlasY = 0;
        int atlasSize = (int)resolution;
        int maxRowHeight = 0;

        IntPtr atlasSurface = SDL.SDL_CreateRGBSurfaceWithFormat(0, atlasSize, atlasSize, 32, SDL.SDL_PIXELFORMAT_RGBA8888);
        if (atlasSurface == IntPtr.Zero)
        {
            throw new Exception("failed to create texture atlas surface...");
        }

        SDL.SDL_Color color;
        color.r = 255;
        color.g = 255;
        color.b = 255;
        color.a = 255;

        for (ushort codePoint = beginCodePoint_; codePoint <= endCodePoint_; ++codePoint)
        {
            if (SDL_ttf.TTF_GlyphMetrics(font, codePoint, out int minx, out int maxx, out int miny, out int maxy, out int advance) == -1)
            {
                throw new Exception("failed to get glyph info...");
            }

            if (atlasX + advance >= atlasSize)
            {
                atlasX = 0;
                atlasY += (maxRowHeight + 1);
                maxRowHeight = 0;
            }

            Glyph glyph;
            glyph.codePoint = codePoint;
            glyph.x = atlasX;
            glyph.y = atlasY;
            glyph.width = maxx - minx;
            glyph.height = maxy - miny + 1;
            glyph.xoffset = minx;
            glyph.yoffset = miny;
            glyph.xadvance = advance;

            glyphs_.Add(glyph);

            SDL.SDL_Rect glyphRect;
            glyphRect.x = glyph.x;
            glyphRect.y = glyph.y + glyph.yoffset;
            glyphRect.w = glyph.width;
            glyphRect.h = glyph.height;

            IntPtr glyphSurface = SDL_ttf.TTF_RenderGlyph_Blended(font, codePoint, color);

            if (SDL.SDL_BlitSurface(glyphSurface, IntPtr.Zero, atlasSurface, ref glyphRect) != 0)
            {
                throw new Exception("failed to blit surface to surface...");
            }

            SDL.SDL_FreeSurface(glyphSurface);

            atlasX += (glyph.xadvance + 1);
            maxRowHeight = Math.Max(maxRowHeight, glyph.height);
        }

        IntPtr textureAtlas = SDL.SDL_CreateTextureFromSurface(RenderManager.Get().GetRendererPtr(), atlasSurface);
        if(textureAtlas == IntPtr.Zero)
        {
            throw new Exception("failed to create texture atlas...");
        }
        
        return textureAtlas;
    }


    /**
     * @brief 텍스처 아틀라스에 표시할 시작 문자입니다.
     */
    private ushort beginCodePoint_;


    /**
     * @brief 텍스처 아틀라스에 표시할 끝 문자입니다.
     */
    private ushort endCodePoint_;


    /**
     * @brief 텍스처 아틀라스입니다.
     */
    private IntPtr textureAtlas_;


    /**
     * @brief 텍스처 아틀라스에 표시된 문자 정보입니다.
     */
    List<Glyph> glyphs_ = new List<Glyph>();
}