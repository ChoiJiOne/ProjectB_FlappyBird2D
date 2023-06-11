using System.Collections.Generic;


/**
 * @brief 플레이어의 랭킹을 보여주는 씬입니다.
 */
class RankScene : Scene
{
    /**
     * @brief 플레이어의 랭킹 씬에 입장합니다.
     */
    public override void Entry()
    {
        base.Entry();

        gameObjectSignatures_ = new List<string>();
    }


    /**
     * @brief 플레이의 랭킹 씬으로부터 나갑니다.
     */
    public override void Leave()
    {
        base.Leave();
    }
}