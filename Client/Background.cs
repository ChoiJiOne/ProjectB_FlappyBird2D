using System;
using SDL2;


/**
 * @brief 게임의 백그라운드 오브젝트입니다.
 */
class Background : GameObject
{
    /**
     * @brief 게임의 백그라운드 속성에 대한 Getter/Setter 입니다.
     */
    public RigidBody Body
    {
        get => rigidBody_;
    }


    /**
     * @brief 백그라운드의 바디를 생성합니다.
     * 
     * @param center 백그라운드 바디의 중심 좌표입니다.
     * @param width 백그라운드 바디의 가로 크기입니다.
     * @param height 백그라운드 바디의 세로 크기입니다.
     */
    public void CreateBody(Vector2<float> center, float width, float height)
    {
        rigidBody_ = new RigidBody(center, width, height);
    }


    /**
     * @brief 백그라운드 게임 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public override void Update(float deltaSeconds)
    {
    }


    /**
     * @brief 백그라운드 게임 오브젝트를 화면에 그립니다.
     */
    public override void Render()
    {
        Texture backgroundTexture = ContentManager.Get().GetTexture("Background");

        RenderManager.Get().DrawTexture(
            ref backgroundTexture, 
            rigidBody_.Center, 
            rigidBody_.Width, 
            rigidBody_.Height
        );
    }


    /**
     * @brief 게임 백그라운드 오브젝트의 강체입니다.
     */
    private RigidBody rigidBody_;
}