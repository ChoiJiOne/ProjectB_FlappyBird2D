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
     * @param renderer 텍스처를 생성하기 위한 렌더러입니다.
     * @param path 텍스처 리소스의 경로입니다.
     * 
     * @throws
     * - 텍스처 이미지 로딩에 실패하면 예외를 던집니다.
     * - 텍스처 리소스 생성에 실패하면 예외를 던집니다.
     */
    public Texture(IntPtr renderer, string path)
    {
        IntPtr surface = SDL_image.IMG_Load(path);

        if(surface == IntPtr.Zero)
        {
            throw new Exception("failed to load texture image...");
        }

        texture_ = SDL.SDL_CreateTextureFromSurface(renderer, surface);
        SDL.SDL_FreeSurface(surface);

        if(texture_ == IntPtr.Zero)
        {
            throw new Exception("failed to create texture resource...");
        }

        if(SDL.SDL_QueryTexture(texture_, out uint _, out int _, out width_, out height_) != 0)
        {
            throw new Exception("failed to query texture resource info...");
        }
    }


    /**
     * @brief 텍스처 속성에 대한 대한 Getter/Setter 입니다.
     */
    public int Width
    {
        get => width_;
    }

    public int Height
    {
        get => height_;
    }

    public IntPtr Resource
    {
        get => texture_;
    }


    /**
     * @brief 텍스처 리소스의 데이터를 명시적으로 정리합니다.
     */
    public void Release()
    {
        if(texture_ != IntPtr.Zero)
        {
            SDL.SDL_DestroyTexture(texture_);
        }
    }


    /**
     * @brief 텍스처의 가로 크기입니다.
     */
    private int width_ = 0;


    /**
     * @brief 텍스처의 세로 크기입니다.
     */
    private int height_ = 0;


    /**
     * @brief 텍스처 리소스의 포인터입니다.
     */
    private IntPtr texture_;
}