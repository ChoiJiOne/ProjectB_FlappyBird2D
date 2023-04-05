using System;


/**
 * @brief 게임 내의 버튼 UI 오브젝트입니다.
 */
class Button : GameObject
{
    /**
     * @brief 버튼 UI 속성에 대한 Getter/Setter 입니다.
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

    public Action EventAction
    {
        get => eventAction_;
        set => eventAction_ = value;
    }

    public float ReduceRatio
    {
        get => reduceRatio_;
        set => reduceRatio_ = value;
    }


    /**
     * @brief 버튼 UI의 바디를 생성합니다.
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
     * @brief 게임 내의 버튼 UI 오브젝트를 업데이트합니다.
     */
    public override void Tick(float deltaSeconds)
    {
        EPressState mousePressState = InputManager.Get().GetMousePressState(EMouseButton.LEFT);

        bIsMouseOverButton_ = IsMouseOverButton();
        bIsPressButton_ = (bIsMouseOverButton_ && (mousePressState == EPressState.PRESSED || mousePressState == EPressState.HELD));

        if(bIsMouseOverButton_ && mousePressState == EPressState.RELEASED)
        {
            eventAction_();
        }

        float textureWidth = uiRigidBody_.Width;
        float textureHeight = uiRigidBody_.Height;

        textureWidth *= bIsPressButton_ ? reduceRatio_ : 1.0f;
        textureHeight *= bIsPressButton_ ? reduceRatio_ : 1.0f;

        Texture uiButtonTexture = ContentManager.Get().GetTexture(uiTextureSignature_);

        RenderManager.Get().DrawTexture(ref uiButtonTexture, uiRigidBody_.Center, textureWidth, textureHeight);
    }


    /**
     * @brief 마우스 위치가 UI 버튼 위에 있는지 확인합니다.
     * 
     * @return 마우스 위치가 UI 버튼 위에 있으면 true, 그렇지 않으면 false를 반환합니다.
     */
    private bool IsMouseOverButton()
    {
        Vector2<float> mousePosition;
        mousePosition.x = (float)(InputManager.Get().CurrCursorPosition.x);
        mousePosition.y = (float)(InputManager.Get().CurrCursorPosition.y);

        uiRigidBody_.GetBoundPosition(out Vector2<float> minPosition, out Vector2<float> maxPosition);

        return (minPosition.x <= mousePosition.x && mousePosition.x <= maxPosition.x)
            && (minPosition.y <= mousePosition.y && mousePosition.y <= maxPosition.y);
    }


    /**
     * @brief 마우스 위치가 UI 버튼 위에 있는지 확인합니다.
     */
    private bool bIsMouseOverButton_ = false;


    /**
     * @brief 마우스로 UI 버튼을 클릭했는지 확인합니다.
     */
    private bool bIsPressButton_ = false;


    /**
     * @brief UI 버튼 텍스처 시그니처 입니다.
     */
    private string uiTextureSignature_;


    /**
     * @brief UI 버튼의 바디입니다.
     */
    private RigidBody uiRigidBody_;


    /**
     * @brief UI 버튼 클릭 시 동작할 액션입니다.
     */
    Action eventAction_ = () => { };


    /**
     * @brief UI 버튼 클릭 시 줄어들 텍스처의 비율입니다.
     */
    float reduceRatio_ = 1.0f;
}