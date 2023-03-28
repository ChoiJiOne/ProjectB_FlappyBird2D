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
        get => speed_;
        set => speed_ = value;
    }

    public bool Movable
    {
        get => bIsMove_;
        set => bIsMove_ = value;
    }

    public Texture Texture
    {
        get => texture_;
        set => texture_ = value;
    }

    public RigidBody RigidBody
    {
        get => rigidBody_;
        set => rigidBody_ = value;
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
            accumulateTime_ += DeltaSeconds;
        }

        if(accumulateTime_ > speed_)
        {
            accumulateTime_ -= speed_;
        }
    }


    /**
     * @brief 백그라운드 게임 오브젝트를 화면에 그립니다.
     * 
     * @param Renderer 오브젝트를 화면에 그리기 위한 렌더러입니다.
     */
    public void Render(IntPtr Renderer)
    {
        float lerp = accumulateTime_ / speed_;

        float width = texture_.Width;
        float height = texture_.Height;

        SDL.SDL_Rect leftSrcRect;
        leftSrcRect.x = (int)(width * lerp);
        leftSrcRect.y = 0;
        leftSrcRect.w = (int)(width * (1.0f - lerp));
        leftSrcRect.h = (int)height;

        SDL.SDL_Rect leftDstRect;
        leftDstRect.x = (int)(rigidBody_.Center.x - rigidBody_.Width / 2.0f);
        leftDstRect.y = (int)(rigidBody_.Center.y - rigidBody_.Height / 2.0f);
        leftDstRect.w = (int)(rigidBody_.Width * (1.0f - lerp));
        leftDstRect.h = (int)(rigidBody_.Height);

        SDL.SDL_RenderCopy(
            Renderer,
            texture_.Resource,
            ref leftSrcRect,
            ref leftDstRect
        );

        SDL.SDL_Rect rightSrcRect;
        rightSrcRect.x = 0;
        rightSrcRect.y = 0;
        rightSrcRect.w = (int)(width * lerp);
        rightSrcRect.h = (int)height;

        SDL.SDL_Rect rightDstRect;
        rightDstRect.x = (int)(rigidBody_.Center.x - rigidBody_.Width / 2.0f + rigidBody_.Width * (1.0f - lerp));
        rightDstRect.y = (int)(rigidBody_.Center.y - rigidBody_.Height / 2.0f);
        rightDstRect.w = (int)(rigidBody_.Width * lerp);
        rightDstRect.h = (int)(rigidBody_.Height);
        
        SDL.SDL_RenderCopy(
             Renderer,
             texture_.Resource,
             ref rightSrcRect,
             ref rightDstRect
         );
    }


    /**
     * @brief 백그라운드 게임 오브젝트 내의 리소스를 정리합니다.
     */
    public void Cleanup()
    {
        texture_.Release();
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
    private float accumulateTime_ = 0.0f;


    /**
     * @brief 게임의 바닥 이동 속도입니다.
     */
    private float speed_ = 0.0f;


    /**
     * @brief 게임 바닥 오브젝트의 강체입니다.
     */
    private RigidBody rigidBody_;


    /**
     * @brief 게임의 바닥 오브젝트 텍스처입니다.
     */
    private Texture texture_;
}