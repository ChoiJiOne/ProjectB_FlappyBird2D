using System;
using System.Collections.Generic; 


/**
 * @brief 파이프 오브젝트를 생성하고 추적하는 오브젝트입니다.
 */
class PipeDetector : GameObject
{
    /**
     * @brief 파이프 오브젝트를 생성하고 추적하는 오브젝트의 Setter/Getter입니다. 
     */
    public bool CanGeneratePipe
    {
        get => bCanGeneratePipe_;
        set => bCanGeneratePipe_ = value;
    }

    public int MaxPipeCount
    {
        get => maxPipeCount_;
        set => maxPipeCount_ = value;
    }

    public Vector2<float> RespawnPosition
    {
        get => respawnPosition_;
        set => respawnPosition_ = value;
    }

    public float PipeTopAndBottomGapLength
    {
        get => pipeTopAndBottomGapLength_;
        set => pipeTopAndBottomGapLength_ = value;
    }

    public float PipeToPipeGapLength
    {
        get => pipeToPipeGapLength_;
        set => pipeToPipeGapLength_ = value;
    }

    public float TotalPipeWidth
    {
        get => totalPipeWidth_;
        set => totalPipeWidth_ = value;
    }

    public float TotalPipeHeight
    {
        get => totalPipeHeight_;
        set => totalPipeHeight_ = value;
    }

    public float MinPipeHeight
    {
        get => minPipeHeight_;
        set => minPipeHeight_ = value;
    }

    public float PipeSpeed
    {
        get => pipeSpeed_;
        set => pipeSpeed_ = value;
    }

    public List<Pipe> DetectPipes
    {
        get => detectPipes_;
    }


    /**
     * @brief 파이프를 생성하고 추적하는 오브젝트를 업데이트합니다.
     */
    public override void Update(float deltaSeconds)
    {
        RemoveLeavePipeFromBackground();

        if (CanGenerateDetectPipe())
        {
            GenerateDetectPipe();
        }
    }


    /**
     * @brief 파이프를 생성하고 추적하는 오브젝트를 렌더링합니다.
     * 
     * @note 파이프 추적 객체는 렌더링을 수행하지 않습니다.
     */
    public override void Render() {}


    /**
     * @brief 추적 가능한 파이프 객체를 생성할 수 있는지 확인합니다.
     * 
     * @return 추적 가능한 파이프 객체를 생성할 수 있다면 true, 그렇지 않으면 false를 반환합니다.
     */
    private bool CanGenerateDetectPipe()
    {
        if (!bCanGeneratePipe_) return false;

        if (detectPipes_.Count >= maxPipeCount_) return false;

        if (detectPipes_.Count == 0) return true;

        Pipe pipe = detectPipes_[detectPipes_.Count - 1];
        float pipeXPosition = pipe.TopRigidBody.Center.x;
        float respawnXPosition = respawnPosition_.x;

        return (respawnXPosition - pipeXPosition) > pipeToPipeGapLength_;
    }


    /**
     * @brief 추적 가능한 파이프 객체를 생성합니다.
     */
    private void GenerateDetectPipe()
    {
        Random random = new Random();

        bool bIsFindRespawnPosition = false;
        while(!bIsFindRespawnPosition)
        {
            float randomY = (float)(random.NextDouble());
            randomY *= totalPipeHeight_;

            float minRandomY = randomY - pipeTopAndBottomGapLength_ / 2.0f;
            float maxRandomY = randomY + pipeTopAndBottomGapLength_ / 2.0f;

            if(minRandomY > minPipeHeight_ && maxRandomY < (totalPipeHeight_ - minPipeHeight_))
            {
                respawnPosition_.y = randomY;
                bIsFindRespawnPosition = true;
            }
        }

        float pipeWidth = totalPipeWidth_;
        float pipeTopHeight = respawnPosition_.y - pipeTopAndBottomGapLength_ / 2.0f;
        float pipeBottomHeight = totalPipeHeight_ - (pipeTopHeight + pipeTopAndBottomGapLength_);

        Vector2<float> topCenter = new Vector2<float>(
            respawnPosition_.x,
            pipeTopHeight / 2.0f
        );

        Vector2<float> bottomCenter = new Vector2<float>(
            respawnPosition_.x,
            totalPipeHeight_ - pipeBottomHeight / 2.0f
        );

        Pipe newPipe = new Pipe();
        newPipe.UpdateOrder = 1;
        newPipe.Movable = true;
        newPipe.Speed = 300.0f;
        newPipe.SignatureNumber = countGeneratePipe_;
        newPipe.TopRigidBody = new RigidBody(topCenter, pipeWidth, pipeTopHeight);
        newPipe.BottomRigidBody = new RigidBody(bottomCenter, pipeWidth, pipeBottomHeight);

        string pipeSignature = string.Format("Pipe{0}", countGeneratePipe_++);

        detectPipes_.Add(newPipe);
        WorldManager.Get().AddGameObject(pipeSignature, newPipe);
    }


    /**
     * @brief 추적 가능한 파이프 객체 중 백그라운드 밖으로 나간 파이프 객체를 삭제합니다.
     */
    void RemoveLeavePipeFromBackground()
    {
        List<Pipe> removePipes = new List<Pipe>();

        foreach(Pipe detectPipe in detectPipes_)
        {
            if(detectPipe.State == Pipe.EState.LEAVE)
            {
                removePipes.Add(detectPipe);
            }
        }

        foreach(Pipe removePipe in removePipes)
        {
            string pipeSignature = string.Format("Pipe{0}", removePipe.SignatureNumber);

            detectPipes_.Remove(removePipe);
            WorldManager.Get().RemoveGameObject(pipeSignature);
        }
    }


    /**
     * @brief 파이프를 생성할 수 있는지 확인합니다.
     */
    private bool bCanGeneratePipe_ = false;


    /**
     * @brief 최대 파이프 수입니다.
     */
    private int maxPipeCount_ = 0;


    /**
     * @brief 생성한 파이프의 수입니다.
     */
    private int countGeneratePipe_ = 0;


    /**
     * @brief 파이프의 생성 위치입니다.
     */
    private Vector2<float> respawnPosition_;


    /**
     * @brief 파이프 사이의 간격입니다.
     */
    private float pipeToPipeGapLength_ = 0.0f;


    /**
     * @brief 상단 파이프와 하단 파이프 사이의 간격입니다.
     */
    private float pipeTopAndBottomGapLength_ = 0.0f;


    /**
     * @brief 파이프의 가로 크기입니다.
     */
    private float totalPipeWidth_ = 0.0f;


    /**
     * @brief 간격을 포함한 파이프의 세로 크기입니다.
     */
    private float totalPipeHeight_ = 0.0f;


    /**
     * @brief 파이프의 최소 높이입니다.
     */
    private float minPipeHeight_ = 0.0f;


    /**
     * @brief 파이프의 이동 속도입니다.
     */
    private float pipeSpeed_ = 0.0f;


    /**
     * @brief 현재 추적 중인 파이프입니다.
     */
    private List<Pipe> detectPipes_ = new List<Pipe>();
}