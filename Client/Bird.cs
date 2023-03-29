using System;
using SDL2;


/**
 * @brief 게임의 플레이어가 조종하는 새 오브젝트입니다.
 */
class Bird : IGameObject
{
    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트 속성에 대한 Getter/Setter 입니다.
     */
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

    public bool Movable
    {
        get => bIsMove_;
        set => bIsMove_ = value;
    }


    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public void Update(float deltaSeconds)
    {
        if (bIsMove_)
        {
        }
    }
    

    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트를 화면에 그립니다.
     * 
     * @param renderer 오브젝트를 화면에 그리기 위한 렌더러입니다.
     */
    public void Render(IntPtr renderer)
    {
        SDL.SDL_Rect birdRect;
        birdRect.x = (int)(rigidBody_.Center.x - rigidBody_.Width / 2.0f);
        birdRect.y = (int)(rigidBody_.Center.y - rigidBody_.Height / 2.0f);
        birdRect.w = (int)(rigidBody_.Width);
        birdRect.h = (int)(rigidBody_.Height);

        SDL.SDL_RenderCopy(renderer, texture_.Resource, IntPtr.Zero, ref birdRect);
    }


    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트 내의 리소스를 정리합니다.
     */
    public void Cleanup()
    {
        texture_.Release();
    }


    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트가 움직일 수 있는지 확인합니다.
     */
    private bool bIsMove_ = false;


    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트의 강체입니다.
     */
    private RigidBody rigidBody_;


    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트의 텍스처입니다.
     */
    private Texture texture_;
}