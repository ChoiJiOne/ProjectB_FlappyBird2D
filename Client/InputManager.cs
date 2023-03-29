using System;
using SDL2;


/**
 * @brief 입력 처리를 수행하는 싱글턴 클래스입니다.
 */
class InputManager
{
    /**
     * @brief 입력 처리를 수행하는 매니저의 인스턴스를 얻습니다.
     * 
     * @return 입력 처리 매니저의 인스턴스를 반환합니다. 
     */
    public static InputManager Get()
    {
        if(inputManager_ == null)
        {
            inputManager_ = new InputManager();
        }

        return inputManager_;
    }


    /**
     * @brief 생성자는 외부에서 호출할 수 없도록 감춥니다.
     */
    private InputManager() { }


    /**
     * @brief 입력 처리를 수행하는 매니저의 인스턴스입니다.
     */
    private static InputManager inputManager_;
}