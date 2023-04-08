/**
 * @brief 게임의 결과를 출력하는 보드 오브젝트입니다.
 */
class ResultBoard : GameObject
{
    /**
     * @brief 게임 스코어 보드 오브젝트의 속성에 대한 Getter/Setter입니다.
     */
    public int PlayerID
    {
        get => playerID_;
        set => playerID_ = value;
    }

    public float UIWidth
    {
        get => uiWidth_;
        set => uiWidth_ = value;
    }

    public float UIHeight
    {
        get => uiHeight_;
        set => uiHeight_ = value;
    }

    public Vector2<float> PlayerIDPosition
    {
        get => playerIDPosition_;
        set => playerIDPosition_ = value;
    }

    public Vector2<float> PlayerScorePosition
    {
        get => playerScorePosition_;
        set => playerScorePosition_ = value;
    }

    public RigidBody BoardBody
    {
        get => rigidBody_;
    }


    /**
     * @brief 게임의 결과를 출력하는 보드 오브젝트의 바디를 생성합니다.
     * 
     * @param center 게임의 결과를 출력하는 보드 오브젝트의 화면 상 중심 좌표입니다.
     * @param width 게임의 결과를 출력하는 보드 오브젝트의 가로 크기입니다.
     * @param height 게임의 결과를 출력하는 보드 오브젝트의 세로 크기입니다.
     */
    public void CreateBody(Vector2<float> center, float width, float height)
    {
        rigidBody_ = new RigidBody(center, width, height);
    }


    /**
     * @brief 게임의 결과를 출력하는 보드 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public override void Tick(float deltaSeconds)
    {
        Texture slateTexture = ContentManager.Get().GetTexture("Slate");
        RenderManager.Get().DrawTexture(ref slateTexture, rigidBody_.Center, rigidBody_.Width, rigidBody_.Height);

        Texture userIDTexture = ContentManager.Get().GetTexture("UserID");
        RenderManager.Get().DrawTexture(ref userIDTexture, playerIDPosition_, uiWidth_, uiHeight_);

        Texture scoreTexture = ContentManager.Get().GetTexture("Score");
        RenderManager.Get().DrawTexture(ref scoreTexture, playerScorePosition_, uiWidth_, uiHeight_);
    }


    /**
     * @brief 플레이어의 아이디입니다.
     */
    int playerID_ = 0;


    /**
     * @brief 보드 내의 UI 가로 크기입니다.
     */
    float uiWidth_ = 0.0f;


    /**
     * @brief 보드 내의 UI 세로 크기입니다.
     */
    float uiHeight_ = 0.0f;


    /**
     * @brief 플레이어 아이디가 표시될 위치입니다.
     */
    Vector2<float> playerIDPosition_;


    /**
     * @brief 플레이어 스코어가 표시될 위치입니다.
     */
    Vector2<float> playerScorePosition_;


    /**
     * @brief 결과를 출력하는 보드의 바디입니다.
     */
    RigidBody rigidBody_;
}