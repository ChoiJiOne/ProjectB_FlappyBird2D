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
        if(InputManager.Get().GetKeyPressState(EVirtualKey.CODE_SPACE) == EPressState.PRESSED)
        {
            bIsMove_ = true;
        }

        if (bIsMove_)
        {
            Vector2<float> center = rigidBody_.Center;
            center.y += (deltaSeconds * 10.0f);
            rigidBody_.Center = center;
        }
    }
    

    /**
     * @brief 게임의 플레이어가 조종하는 새 오브젝트를 화면에 그립니다.
     */
    public void Render()
    {
        RenderManager.Get().DrawTexture(ref texture_, rigidBody_.Center, rigidBody_.Width, rigidBody_.Height);
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