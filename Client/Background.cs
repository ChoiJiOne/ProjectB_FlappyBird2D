using System;
using SDL2;


/**
 * @brief 게임의 백그라운드 오브젝트입니다.
 */
class Background : IGameObject
{
    /**
     * @brief 게임의 백그라운드 속성에 대한 Getter/Setter 입니다.
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


    /**
     * @brief 백그라운드 게임 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public void Update(float deltaSeconds)
    {
        
    }


    /**
     * @brief 백그라운드 게임 오브젝트를 화면에 그립니다.
     * 
     * @param Renderer 오브젝트를 화면에 그리기 위한 렌더러입니다.
     */
    public void Render(IntPtr renderer)
    {
        SDL.SDL_Rect backgroundRect;
        backgroundRect.x = (int)(rigidBody_.Center.x - rigidBody_.Width / 2.0f);
        backgroundRect.y = (int)(rigidBody_.Center.y - rigidBody_.Height / 2.0f);
        backgroundRect.w = (int)(rigidBody_.Width);
        backgroundRect.h = (int)(rigidBody_.Height);

        SDL.SDL_RenderCopy(
            renderer,
            texture_.Resource,
            IntPtr.Zero,
            ref backgroundRect
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
     * @brief 게임 백그라운드 오브젝트의 강체입니다.
     */
    private RigidBody rigidBody_;


    /**
     * @brief 게임의 백그라운드 오브젝트 텍스처입니다.
     */
    private Texture texture_;
}