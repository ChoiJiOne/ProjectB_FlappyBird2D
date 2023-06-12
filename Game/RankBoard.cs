using System.Collections.Generic;


/**
 * @brief 플레이어의 랭킹을 보여주는 오브젝트입니다.
 */
class RankBoard : GameObject
{
    /**
     * @brief 플레이어의 랭킹을 보여주는 오브젝트에 대한 Getter/Setter입니다.
     */
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

    }


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