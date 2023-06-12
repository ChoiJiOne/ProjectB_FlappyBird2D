using System;
using System.Data;
using System.Collections.Generic;


/**
 * @brief 플레이어의 랭킹을 보여주는 씬입니다.
 */
class RankScene : Scene
{
    /**
     * @brief 랭킹 씬에 대한 Getter/Setter 입니다.
     */
    public string GamePlayTime
    {
        get => gamePlayTime_;
        set => gamePlayTime_ = value;
    }

    public int CountPassPipe
    {
        get => countPassPipe_;
        set => countPassPipe_ = value;
    }


    /**
     * @brief 플레이어의 랭킹 씬에 입장합니다.
     */
    public override void Entry()
    {
        base.Entry();

        gameObjectSignatures_ = new List<string>();
        gameObjectSignatures_.Add("OkButton");

        Button okButton = new Button();
        okButton.UpdateOrder = 6;
        okButton.Active = true;
        okButton.UITexture = "OkButton";
        okButton.EventAction = () =>
        {
            DetectSwitch = true;

            Sound doneSound = ContentManager.Get().GetSound("Done") as Sound;
            doneSound.Reset();
            doneSound.Play();
        };
        okButton.ReduceRatio = 0.95f;
        okButton.CreateUIBody(new Vector2<float>(500.0f, 600.0f), 160.0f, 60.0f);

        List<KeyValuePair<string, int>> topPlayRank = GetTop3PlayRank();

        WorldManager.Get().AddGameObject("OkButton", okButton);
    }


    /**
     * @brief 플레이의 랭킹 씬으로부터 나갑니다.
     */
    public override void Leave()
    {
        CleanupGameObjects();
        base.Leave();
    }


    /**
     * @brief 데이터베이스로부터 상위 3개의 기록을 가져옵니다.
     * 
     * @return 상위 3개의 기록을 반환합니다.
     */
    private List<KeyValuePair<string, int>> GetTop3PlayRank()
    {
        List<KeyValuePair<string, int>> topPlayRank = new List<KeyValuePair<string, int>>();

        Database playRank = ContentManager.Get().GetDatabase("PlayRank");
        DataSet playRankDataSet = playRank.Select("SELECT * FROM PlayRank ORDER BY Pipe DESC LIMIT 3;");
        DataTable playRankDataTable = playRankDataSet.Tables[0];

        foreach (DataRow dataRows in playRankDataTable.Rows)
        {
            string playTime = (string)(dataRows.ItemArray[0]);
            Int64 pipe = (Int64)(dataRows.ItemArray[1]);

            topPlayRank.Add(new KeyValuePair<string, int>(playTime, (int)pipe));
        }

        return topPlayRank;
    }


    /**
     * @brief 게임 플레이가 끝난 시각입니다.
     */
    private string gamePlayTime_;


    /**
     * @brief 플레이어가 통과한 파이프의 수입니다.
     */
    private int countPassPipe_ = 0;
}