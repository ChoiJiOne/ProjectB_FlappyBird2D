using System;
using System.Runtime.InteropServices;


/**
 * @brief 콘솔 창 제어를 수행하는 핸들러입니다.
 * 
 * @note 이 클래스는 정적 클래스로 인스턴스 생성 없이 바로 사용할 수 있습니다.
 */
class ConsoleHandler
{
    /*******************************************************
     * @brief Begin Windows API
     *******************************************************/
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    const int SW_HIDE = 0;
    const int SW_SHOW = 5;
    /*******************************************************
     * @brief End Windows API
     *******************************************************/


    /**
     * @brief 콘솔 창의 시야 설정을 수행합니다.
     * 
     * @param bIsVisible true 값이면 콘솔 창을 화면에 띄우고, false 값이면 콘솔 창을 숨깁니다.
     * 
     * @throws 콘솔 창 제어에 실패하면 예외를 던집니다.
     */
    public static void SetVisible(bool bIsVisible)
    {
        int CmdShow = bIsVisible ? SW_SHOW : SW_HIDE;
        IntPtr consoleWindowHandle = GetConsoleWindow();

        if(!ShowWindow(consoleWindowHandle, CmdShow))
        {
            throw new Exception("failed to set console window visible...");
        }
    }
}