using System;
using SDL2;


/**
 * @brief 게임의 바닥 오브젝트입니다.
 */
class Floor : IGameObject
{
    /**
     * @brief 게임의 바닥 오브젝트 속성에 대한 Getter/Setter 입니다.
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

    public float Speed
    {
        get => Speed_;
        set => Speed_ = value;
    }

    public bool Movable
    {
        get => bIsMove_;
        set => bIsMove_ = value;
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
        if (TextureSurface == IntPtr.Zero)
        {
            return false;
        }

        Texture_ = SDL.SDL_CreateTextureFromSurface(Renderer, TextureSurface);
        SDL.SDL_FreeSurface(TextureSurface);

        return (Texture_ != IntPtr.Zero);
    }


    /**
     * @brief 백그라운드 게임 오브젝트를 업데이트합니다.
     * 
     * @param DeltaSeconds 초단위 델타 시간값입니다.
     */
    public void Update(float DeltaSeconds)
    {
        AccumulateTime_ += DeltaSeconds;
    }


    /**
     * @brief 백그라운드 게임 오브젝트를 화면에 그립니다.
     * 
     * @param Renderer 오브젝트를 화면에 그리기 위한 렌더러입니다.
     */
    public void Render(IntPtr Renderer)
    {
        SDL.SDL_Rect SpriteRect;
        SpriteRect.x = (int)(Center_.X - Width_ / 2.0f);
        SpriteRect.y = (int)(Center_.Y - Height_ / 2.0f);
        SpriteRect.w = (int)(Width_);
        SpriteRect.h = (int)(Height_);

        SDL.SDL_RenderCopy(
            Renderer,
            Texture_,
            IntPtr.Zero,
            ref SpriteRect
        );
    }


    /**
     * @brief 백그라운드 게임 오브젝트 내의 리소스를 정리합니다.
     */
    public void Cleanup()
    {
        if (Texture_ != IntPtr.Zero)
        {
            SDL.SDL_DestroyTexture(Texture_);
        }
    }


    /**
     * @brief 게임의 바닥이 현재 이동 중인지 확인합니다.
     */
    bool bIsMove_ = false;


    /**
     * @brief 게임의 바닥 오브젝트 중심 좌표입니다.
     */
    private Vector2<float> Center_;


    /**
     * @brief 게임의 바닥 오브젝트 가로 크기입니다.
     */
    private float Width_ = 0.0f;


    /**
     * @brief 게임의 바닥 오브젝트 세로 크기입니다.
     */
    private float Height_ = 0.0f;


    /**
     * @brief 게임 바닥 오브젝트의 누적 시간입니다.
     * 
     * @note 단위는 초단위입니다.
     */
    private float AccumulateTime_ = 0.0f;


    /**
     * @brief 게임의 바닥 이동 속도입니다.
     */
    private float Speed_ = 0.0f;


    /**
     * @brief 게임의 바닥 오브젝트 텍스처입니다.
     */
    private IntPtr Texture_;
}