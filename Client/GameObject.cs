using System;


/**
 * @brief 게임 내의 오브젝트를 나타내는 인터페이스입니다.
 */
public interface IGameObject
{
    /**
     * @brief 게임 오브젝트를 업데이트합니다.
     * 
     * @param deltaSeconds 초단위 델타 시간값입니다.
     */
    public void Update(float deltaSeconds);


    /**
     * @brief 게임 오브젝트를 화면에 그립니다.
     * 
     * @param renderer 오브젝트를 화면에 그리기 위한 렌더러입니다.
     */
    public void Render(IntPtr renderer);


    /**
     * @brief 게임 오브젝트 내의 리소스를 정리합니다.
     */
    public void Cleanup();
}