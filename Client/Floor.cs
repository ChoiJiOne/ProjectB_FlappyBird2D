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
     * @brief 바닥 텍스처를 설정합니다.
     * 
     * @note 이미 텍스처가 로딩되어 있다면, 기존의 텍스처 리소스를 삭제합니다.
     * 
     * @param FloorTexture 설정할 백그라운드 텍스처입니다.
     * 
     * @throws 텍스처 리소스 생성에 실패하면 표준 예외를 던집니다.
     */
    public void SetTexture(Texture FloorTexture)
    {
        Texture_ = FloorTexture;
    }


    /**
     * @brief 바닥의 강체를 설정합니다.
     * 
     * @note 이미 설정되어 있다면, 기존의 강체를 덮어 씁니다.
     * 
     * @param FloorRigidBody 설정할 강체입니다.
     */
    public void SetRigidBody(RigidBody FloorRigidBody)
    {
        RigidBody_ = FloorRigidBody;
    }


    /**
     * @brief 백그라운드 게임 오브젝트를 업데이트합니다.
     * 
     * @param DeltaSeconds 초단위 델타 시간값입니다.
     */
    public void Update(float DeltaSeconds)
    {
        if (bIsMove_)
        {
            AccumulateTime_ += DeltaSeconds;
        }

        if(AccumulateTime_ > Speed_)
        {
            AccumulateTime_ -= Speed_;
        }
    }


    /**
     * @brief 백그라운드 게임 오브젝트를 화면에 그립니다.
     * 
     * @param Renderer 오브젝트를 화면에 그리기 위한 렌더러입니다.
     */
    public void Render(IntPtr Renderer)
    {
        float Lerp = AccumulateTime_ / Speed_;

        float Width = Texture_.Width;
        float Height = Texture_.Height;

        SDL.SDL_Rect LeftSrcRect;
        LeftSrcRect.x = (int)(Width * Lerp);
        LeftSrcRect.y = 0;
        LeftSrcRect.w = (int)(Width * (1.0f - Lerp));
        LeftSrcRect.h = (int)Height;

        SDL.SDL_Rect LeftDstRect;
        LeftDstRect.x = (int)(RigidBody_.Center.X - RigidBody_.Width / 2.0f);
        LeftDstRect.y = (int)(RigidBody_.Center.Y - RigidBody_.Height / 2.0f);
        LeftDstRect.w = (int)(RigidBody_.Width * (1.0f - Lerp));
        LeftDstRect.h = (int)(RigidBody_.Height);

        SDL.SDL_RenderCopy(
            Renderer,
            Texture_.Resource,
            ref LeftSrcRect,
            ref LeftDstRect
        );

        SDL.SDL_Rect RightSrcRect;
        RightSrcRect.x = 0;
        RightSrcRect.y = 0;
        RightSrcRect.w = (int)(Width * Lerp);
        RightSrcRect.h = (int)Height;

        SDL.SDL_Rect RightDstRect;
        RightDstRect.x = (int)(RigidBody_.Center.X - RigidBody_.Width / 2.0f + RigidBody_.Width * (1.0f - Lerp));
        RightDstRect.y = (int)(RigidBody_.Center.Y - RigidBody_.Height / 2.0f);
        RightDstRect.w = (int)(RigidBody_.Width * Lerp);
        RightDstRect.h = (int)(RigidBody_.Height);
        
        SDL.SDL_RenderCopy(
             Renderer,
             Texture_.Resource,
             ref RightSrcRect,
             ref RightDstRect
         );
    }


    /**
     * @brief 백그라운드 게임 오브젝트 내의 리소스를 정리합니다.
     */
    public void Cleanup()
    {
        Texture_.Release();
    }


    /**
     * @brief 게임의 바닥이 현재 이동 중인지 확인합니다.
     */
    bool bIsMove_ = false;


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
     * @brief 게임 바닥 오브젝트의 강체입니다.
     */
    private RigidBody RigidBody_;


    /**
     * @brief 게임의 바닥 오브젝트 텍스처입니다.
     */
    private Texture Texture_;
}