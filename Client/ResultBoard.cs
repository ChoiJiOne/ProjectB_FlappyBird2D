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

        Vector2<float> center;
        float width = 200.0f, height = 50.0f;

        center.x = 400.0f;
        center.y = 350.0f;
        Texture userIDTexture = ContentManager.Get().GetTexture("UserID");
        RenderManager.Get().DrawTexture(ref userIDTexture, center, width, height);

        center.x = 400.0f;
        center.y = 450.0f;
        Texture scoreTexture = ContentManager.Get().GetTexture("Score");
        RenderManager.Get().DrawTexture(ref scoreTexture, center, width, height);
    }


    /**
     * @brief 플레이어의 아이디입니다.
     */
    int playerID_ = 0;


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