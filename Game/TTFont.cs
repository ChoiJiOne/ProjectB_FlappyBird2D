using System;
using System.IO;
using System.Text.RegularExpressions;
using SDL2;
using System.Collections.Generic;

/**
 * @brief 폰트 텍스처 아틀라스 내 문자 정보입니다.
 */
class Glyph
{
    public int codePoint = 0;
    public int x0 = 0;
    public int y0 = 0;
    public int x1 = 0;
    public int y1 = 0;
    public float xoffset = 0.0f;
    public float yoffset = 0.0f;
    public float xoffset2 = 0.0f;
    public float yoffset2 = 0.0f;
    public float xadvance = 0.0f;
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
        ParseGlyphInfo(glyphPath);
        LoadTextureAtlas(textureAtlasPath);
    }


    /**
     * @brief 트루 타입 폰트 속성에 대한 대한 Getter/Setter 입니다.
     */
    public IntPtr Resource
    {
        get => textureAtlas_;
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
     * @brief 텍스트의 크기를 측정합니다.
     * 
     * @param text 측정할 텍스트의 크기입니다.
     * @param outWidth[out] 전체 텍스트의 가로 크기입니다.
     * @param outHeight[out] 전체 텍스트의 세로 크기입니다.
     * 
     * @throws 유효하지 않은 텍스트의 크기를 측정하면 예외를 던집니다.
     */
    public void MeasureText(string text, out int outWidth, out int outHeight)
    {
        if(!IsValidText(text))
        {
            throw new Exception("invalid text in font...");
        }

        int textWidth = 0;
        int textHeight = -1;

        char[] characters = text.ToCharArray();

        foreach (char character in characters)
        {
            int currentWidth = (int)(glyphs_[character].xadvance);
            int currentHeight = glyphs_[character].y1 - glyphs_[character].y0;

            textWidth += currentWidth;

            if (currentHeight > textHeight)
            {
                textHeight = currentHeight;
            }
        }

        outWidth = textWidth;
        outHeight = textHeight;
    }


    /**
     * @brief 텍스트가 유효한지 검사합니다.
     * 
     * @param text 검사를 수행할 텍스트입니다.
     * 
     * @return 텍스트가 유효하다면 true, 그렇지 않으면 false를 반환합니다.
     */
    public bool IsValidText(string text)
    {
        char[] characters = text.ToCharArray();

        foreach(char character in characters)
        {
            if(!glyphs_.ContainsKey(character))
            {
                return false;
            }
        }

        return true;
    }


    /**
     * @brief 글리프 정보를 얻습니다.
     * 
     * @param character 글리프 정보를 얻을 문자입니다.
     * 
     * @return 문자에 대응하는 글리프를 반환합니다.
     */
    public Glyph GetGlyph(char character)
    {
        if (!glyphs_.ContainsKey(character))
        {
            throw new Exception("invalid character in font...");
        }

        return glyphs_[character];
    }


    /**
     * @brief 글리프 정보를 포함한 ini 파일을 파싱합니다.
     * 
     * @param glyphPath 글리프 정보를 포함한 ini 파일 경로입니다.
     * 
     * @throws
     * - 글리프 정보 파일이 ini 파일이 아니라면 예외를 던집니다.
     * - 글리프 정보 파일이 유효하지 않으면 예외를 던집니다.
     */
    private void ParseGlyphInfo(string glyphPath)
    {
        string glyphFileContent = File.ReadAllText(glyphPath);
        Regex sectionRegex = new Regex(@"\[(.*?)\](.*?)(?=\[|\z)", RegexOptions.Singleline);

        MatchCollection sectionMatches = sectionRegex.Matches(glyphFileContent);

        foreach (Match sectionMatche in sectionMatches)
        {
            string section = sectionMatche.Groups[1].Value.Trim();
            string context = sectionMatche.Groups[2].Value.Trim();

            string[] lines = context.Split(new[] { "\r\n" }, StringSplitOptions.None);
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            foreach (string line in lines)
            {
                string[] keyValue = line.Split('=');
                keyValues.Add(keyValue[0], keyValue[1]);
            }

            if (Int32.TryParse(section, out int codePoint))
            {
                glyphs_.Add((char)(codePoint), new Glyph());

                glyphs_[(char)(codePoint)].codePoint = codePoint;
                glyphs_[(char)(codePoint)].x0 = Int32.Parse(keyValues["x0"]);
                glyphs_[(char)(codePoint)].y0 = Int32.Parse(keyValues["y0"]);
                glyphs_[(char)(codePoint)].x1 = Int32.Parse(keyValues["x1"]);
                glyphs_[(char)(codePoint)].y1 = Int32.Parse(keyValues["y1"]);
                glyphs_[(char)(codePoint)].xoffset = float.Parse(keyValues["xoffset"]);
                glyphs_[(char)(codePoint)].yoffset = float.Parse(keyValues["yoffset"]);
                glyphs_[(char)(codePoint)].xoffset2 = float.Parse(keyValues["xoffset2"]);
                glyphs_[(char)(codePoint)].yoffset2 = float.Parse(keyValues["yoffset2"]);
                glyphs_[(char)(codePoint)].xadvance = float.Parse(keyValues["xadvance"]);
            }
            else
            {
                if (section.Equals("Info"))
                {
                    beginCodePoint_ = Int32.Parse(keyValues["BeginCodePoint"]);
                    endCodePoint_ = Int32.Parse(keyValues["EndCodePoint"]);
                    textureAtlasSize_ = Int32.Parse(keyValues["BitmapSize"]);
                    fontSize_ = float.Parse(keyValues["FontSize"]);
                }
            }
        }
    }


    /**
     * @brief 트루 타입 폰트의 텍스처 아틀라스를 로딩합니다.
     * 
     * @param textureAtlasPath 트루 타입 폰트의 텍스처 아틀라스 경로입니다.
     */
    private void LoadTextureAtlas(string textureAtlasPath)
    {
        IntPtr textureAtlasSurface = SDL_image.IMG_Load(textureAtlasPath);

        if (textureAtlasSurface == IntPtr.Zero)
        {
            throw new Exception("failed to load font texture atlas image...");
        }

        textureAtlas_ = SDL.SDL_CreateTextureFromSurface(RenderManager.Get().GetRendererPtr(), textureAtlasSurface);
        SDL.SDL_FreeSurface(textureAtlasSurface);

        if (textureAtlas_ == IntPtr.Zero)
        {
            throw new Exception("failed to create font texture atlas resource...");
        }
    }


    /**
     * @brief 코드 포인트의 시작점입니다.
     */
    private int beginCodePoint_ = 0;


    /**
     * @brief 코드 포인트의 끝점입니다.
     */
    private int endCodePoint_ = 0;


    /**
     * @brief 문자의 글리프 정보입니다.
     */
    private Dictionary<char, Glyph> glyphs_ = new Dictionary<char, Glyph>();


    /**
     * @brief 텍스처 아틀라스의 크기입니다.
     * 
     * @note 텍스처 아틀라스의 가로 세로 크기는 동일합니다.
     */
    private int textureAtlasSize_ = 0;


    /**
     * @brief 텍스처 아틀라스의 리소스입니다.
     */
    private IntPtr textureAtlas_;


    /**
     * @brief 폰트의 크기입니다.
     */
    private float fontSize_ = 0.0f;
}