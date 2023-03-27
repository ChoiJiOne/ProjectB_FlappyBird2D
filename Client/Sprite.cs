using System;
using SDL2;


/**
 * @brief 게임 내의 스프라이트 객체입니다.
 */
class Sprite
{
    /**
     * @brief 스프라이트 객체의 디폴트 생성자입니다.
     */
    public Sprite()
    {
        Center_.X = 0.0f;
        Center_.Y = 0.0f;
    }


    /**
     * @brief 스프라이트 객체의 속성에 대한 Getter/Setter 입니다.
     */
    public Vector2<float> Center
    {
        get => Center_;
        set => Center_ = value;
    }

    public float Width
    {
        get => Width_;
        set => Width_ = value;
    }

    public float Height
    {
        get => Height_;
        set => Height_ = value;
    }

    public float Rotate
    {
        get => Rotate_;
        set => Rotate_ = value;
    }


    /**
     * @brief 텍스처를 로딩합니다.
     * 
     * @note 이미 텍스처가 로딩되어 있다면, 기존의 텍스처 리소스를 삭제합니다.
     * 
     * @param Renderer 텍스처 리소스를 생성할 때 사용할 렌더러입니다.
     * @param TexturePath 스프라이트 객체의 텍스처 리소스 경로입니다.
     * 
     * @return 텍스처 로딩에 성공하면 true, 그렇지 않으면 false를 반환합니다.
     */
    public bool LoadTexture(IntPtr Renderer, string TexturePath)
    {
        IntPtr TextureSurface = SDL_image.IMG_Load(TexturePath);
        if(TextureSurface == IntPtr.Zero)
        {
            return false;
        }

        Texture_ = SDL.SDL_CreateTextureFromSurface(Renderer, TextureSurface);
        SDL.SDL_FreeSurface(TextureSurface);

        return (Texture_ != IntPtr.Zero);
    }


    /**
     * @brief 스프라이트에 할당된 텍스처를 할당 해제합니다.
     */
    public void ReleaseTexture()
    {
        if(Texture_ != IntPtr.Zero)
        {
            SDL.SDL_DestroyTexture(Texture_);
        }
    }


    /**
     * @brief 스프라이트 객체를 업데이트합니다.
     * 
     * @param DeltaSeconds 초단위 델타 시간값입니다.
     */
    public void Update(float DeltaSeconds)
    {

    }


    /**
     * @brief 스프라이트 객체를 렌더링합니다.
     * 
     * @param Renderer 객체를 렌더링할 렌더러입니다.
     */
    public void Render(IntPtr Renderer)
    {
        SDL.SDL_Rect SpriteRect;
        SpriteRect.x = (int)(Center_.X - Width_ / 2.0f);
        SpriteRect.y = (int)(Center_.Y - Height_ / 2.0f);
        SpriteRect.w = (int)(Width_);
        SpriteRect.h = (int)(Height_);

        SDL.SDL_Point SpritePoint;
        SpritePoint.x = (int)(Center_.X);
        SpritePoint.y = (int)(Center_.Y);

        SDL.SDL_RenderCopyEx(
            Renderer,
            Texture_,
            IntPtr.Zero,
            ref SpriteRect,
            (double)(Rotate_),
            ref SpritePoint,
            SDL.SDL_RendererFlip.SDL_FLIP_NONE
        );
    }


    /**
     * @brief 스프라이트 객체의 중심 좌표입니다.
     */
    private Vector2<float> Center_;


    /**
     * @brief 스프라이트 객체의 가로 크기입니다.
     */
    private float Width_ = 0.0f;


    /**
     * @brief 스프라이트 객체의 세로 크기입니다.
     */
    private float Height_ = 0.0f;


    /**
     * @brief 스프라이트 객체의 방향입니다.
     * 
     * @note 이때, 방향 각도의 기준은 육십분법입니다.
     */
    private float Rotate_ = 0.0f;


    /**
     * @brief 스프라이트 객체의 텍스처입니다.
     */
    private IntPtr Texture_;
}