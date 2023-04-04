/**
 * @brief 게임 내의 슬레이트 UI 오브젝트입니다.
 */
class Slate : GameObject
{
    /**
     * @brief 슬레이트 UI 속성에 대한 Getter/Setter 입니다.
     */
    public RigidBody UIBody
    {
        get => uiRigidBody_;
    }

    public string UITexture
    {
        get => uiTextureSignature_;
        set => uiTextureSignature_ = value;
    }


    /**
     * @brief 슬레이트 UI의 바디를 생성합니다.
     * 
     * @param center 화면 상의 UI 중심 좌표입니다.
     * @param width UI 바디의 가로 크기입니다.
     * @param height UI 바디의 세로 크기입니다.
     */
    public void CreateUIBody(Vector2<float> center, float width, float height)
    {
        uiRigidBody_ = new RigidBody(center, width, height);
    }


    /**
     * @brief 게임 내의 슬레이트 UI 오브젝트를 업데이트합니다.
     */
    public override void Tick(float deltaSeconds)
    {
        Texture uiSlateTexture = ContentManager.Get().GetTexture(uiTextureSignature_);
        RenderManager.Get().DrawTexture(ref uiSlateTexture, uiRigidBody_.Center, uiRigidBody_.Width, uiRigidBody_.Height);
    }


    /**
     * @brief UI 슬레이트 텍스처 시그니처 입니다.
     */
    private string uiTextureSignature_;


    /**
     * @brief UI 슬레이트의 바디입니다.
     */
    private RigidBody uiRigidBody_;
}