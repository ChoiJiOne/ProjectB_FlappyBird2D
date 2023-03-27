using System;
using SDL2;


/**
 * @brief 게임의 백그라운드 오브젝트입니다.
 */
class Background : IGameObject
{
    /**
     * @brief 게임의 백그라운드 오브젝트 속성에 대한 Getter/Setter 입니다.
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


    /**
     * @brief 백그라운드 텍스처를 설정합니다.
     * 
     * @note 이미 텍스처가 로딩되어 있다면, 기존의 텍스처 리소스를 삭제합니다.
     * 
     * @param BGTexture 설정할 백그라운드 텍스처입니다.
     * 
     * @throws 텍스처 리소스 생성에 실패하면 표준 예외를 던집니다.
     */
    public void SetTexture(Texture BGTexture)
    {
        BGTexture_ = BGTexture;
    }


    /**
     * @brief 백그라운드 게임 오브젝트를 업데이트합니다.
     * 
     * @param DeltaSeconds 초단위 델타 시간값입니다.
     */
    public void Update(float DeltaSeconds)
    {
        
    }


    /**
     * @brief 백그라운드 게임 오브젝트를 화면에 그립니다.
     * 
     * @param Renderer 오브젝트를 화면에 그리기 위한 렌더러입니다.
     */
    public void Render(IntPtr Renderer)
    {
        SDL.SDL_Rect BGSpriteRect;
        BGSpriteRect.x = (int)(Center_.X - Width_ / 2.0f);
        BGSpriteRect.y = (int)(Center_.Y - Height_ / 2.0f);
        BGSpriteRect.w = (int)(Width_);
        BGSpriteRect.h = (int)(Height_);

        SDL.SDL_RenderCopy(
            Renderer,
            BGTexture_.Resource,
            IntPtr.Zero,
            ref BGSpriteRect
        );
    }


    /**
     * @brief 백그라운드 게임 오브젝트 내의 리소스를 정리합니다.
     */
    public void Cleanup()
    {
        BGTexture_.Release();
    }


    /**
     * @brief 게임의 백그라운드 오브젝트 중심 좌표입니다.
     */
    private Vector2<float> Center_;


    /**
     * @brief 게임의 백그라운드 오브젝트 가로 크기입니다.
     */
    private float Width_ = 0.0f;


    /**
     * @brief 게임의 백그라운드 오브젝트 세로 크기입니다.
     */
    private float Height_ = 0.0f;


    /**
     * @brief 게임의 백그라운드 오브젝트 텍스처입니다.
     */
    private Texture BGTexture_;
}