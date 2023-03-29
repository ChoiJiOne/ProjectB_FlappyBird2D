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

    public bool Plable
    {
        get => bIsPlay_;
        set => bIsPlay_ = value;
    }

    public float PlayTime
    {
        get => playTime_;
        set => playTime_ = value;
    }


    /**
     * @brief 백그라운드 게임 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public void Update(float deltaSeconds)
    {
        if(bIsPlay_)
        {
            playTime_ += deltaSeconds;
        }
    }


    /**
     * @brief 백그라운드 게임 오브젝트를 화면에 그립니다.
     */
    public void Render()
    {
        RenderManager.Get().DrawTexture(ref texture_, rigidBody_.Center, rigidBody_.Width, rigidBody_.Height);
    }


    /**
     * @brief 백그라운드 게임 오브젝트 내의 리소스를 정리합니다.
     */
    public void Cleanup()
    {
        texture_.Release();
    }


    /**
     * @brief 현재 게임이 플레이중인지 확인합니다.
     */
    private bool bIsPlay_ = false;


    /**
     * @brief 플레이한 시간입니다.
     */
    private float playTime_ = 0.0f;


    /**
     * @brief 게임 백그라운드 오브젝트의 강체입니다.
     */
    private RigidBody rigidBody_;


    /**
     * @brief 게임의 백그라운드 오브젝트 텍스처입니다.
     */
    private Texture texture_;
}