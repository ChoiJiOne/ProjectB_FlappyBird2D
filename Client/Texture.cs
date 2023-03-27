using System;
using SDL2;


/**
 * @brief 텍스처를 로딩하고 관리하는 클래스입니다.
 */
class Texture
{
    /**
     * @brief 텍스처를 로딩하고 관리하는 클래스의 생성자입니다.
     * 
     * @param Renderer 텍스처를 생성하기 위한 렌더러입니다.
     * @param Path 텍스처 리소스의 경로입니다.
     */
    public Texture(IntPtr Renderer, string Path)
    {
        IntPtr Surface = SDL_image.IMG_Load(Path);

        if(Surface == IntPtr.Zero)
        {
            throw new Exception("failed to load texture image...");
        }

        Texture_ = SDL.SDL_CreateTextureFromSurface(Renderer, Surface);
        SDL.SDL_FreeSurface(Surface);

        if(Texture_ == IntPtr.Zero)
        {
            throw new Exception("failed to create texture resource...");
        }

        if(SDL.SDL_QueryTexture(Texture_, out uint _, out int _, out Width_, out Height_) != 0)
        {
            throw new Exception("failed to query texture resource info...");
        }
    }


    /**
     * @brief 텍스처 리소스의 데이터를 명시적으로 정리합니다.
     */
    public void Release()
    {
        if(Texture_ != IntPtr.Zero)
        {
            SDL.SDL_DestroyTexture(Texture_);
        }
    }


    /**
     * @brief 텍스처 속성에 대한 대한 Getter/Setter 입니다.
     */
    public int Width
    {
        get => Width_;
    }

    public int Height
    {
        get => Height_;
    }

    public IntPtr Resource
    {
        get => Texture_;
    }


    /**
     * @brief 텍스처의 가로 크기입니다.
     */
    private int Width_ = 0;


    /**
     * @brief 텍스처의 세로 크기입니다.
     */
    private int Height_ = 0;


    /**
     * @brief 텍스처 리소스의 포인터입니다.
     */
    private IntPtr Texture_;
}