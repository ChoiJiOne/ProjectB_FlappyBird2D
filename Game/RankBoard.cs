using System.Collections.Generic;


/**
 * @brief 플레이어의 랭킹을 보여주는 오브젝트입니다.
 */
class RankBoard : GameObject
{
    /**
     * @brief 플레이어의 랭킹을 보여주는 오브젝트에 대한 Getter/Setter입니다.
     */
    public Vector2<float> BasePosition
    {
        get => basePosition_;
        set => basePosition_ = value;
    }

    public float Stride
    {
        get => stride_;
        set => stride_ = value;
    }

    public List<KeyValuePair<string, int>> TopPlayRank
    {
        get => topPlayRank_;
        set => topPlayRank_ = value;
    }

    public string NewGamePlayTime
    {
        get => newGamePlayTime_;
        set => newGamePlayTime_ = value;
    }

    public int NewCountPassPipe
    {
        get => newCountPassPipe_;
        set => newCountPassPipe_ = value;
    }


    /**
     * @brief 플레이어의 랭킹을 보여주는 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public override void Tick(float deltaSeconds)
    {
        TTFont font = ContentManager.Get().GetTTFont("SeoulNamsanEB");

        Vector2<float> position;
        position.x = basePosition_.x;
        position.y = basePosition_.y;

        RenderManager.Get().DrawText(ref font, "TOP 3", position, Color.BLUE);

        foreach(KeyValuePair<string, int> topPlayRank in topPlayRank_)
        {
            position.y += stride_;
            RenderManager.Get().DrawText(ref font, string.Format("{0} | {1}", topPlayRank.Key, topPlayRank.Value), position, Color.WHITE);
        }

        position.y += stride_;
        RenderManager.Get().DrawText(ref font, "YOUR", position, Color.RED);

        position.y += stride_;
        RenderManager.Get().DrawText(ref font, string.Format("{0} | {1}", newGamePlayTime_, newCountPassPipe_), position, Color.WHITE);
    }


    /**
     * @brief UI의 기준 위치입니다.
     */
    private Vector2<float> basePosition_;


    /**
     * @brief 표시될 문자열 간의 위치 간격입니다.
     */
    private float stride_;


    /**
     * @brief 플레이어의 TOP 3 랭킹 기록입니다.
     */
    private List<KeyValuePair<string, int>> topPlayRank_ = null;


    /**
     * @brief 가장 최근의 게임 플레이가 끝난 시각입니다.
     */
    private string newGamePlayTime_;


    /**
     * @brief 가장 최근에 플레이어가 통과한 파이프의 수입니다.
     */
    private int newCountPassPipe_ = 0;
}