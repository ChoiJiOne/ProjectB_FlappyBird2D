using System;
using System.IO;
using System.Collections.Generic;


/**
 * @brief 게임 내에 로그를 기록합니다.
 * 
 * @note 이 클래스는 정적 클래스로 인스턴스 생성 없이 바로 사용할 수 있습니다.
 */
class Logger
{
    /**
     * @brief 일반적인 로그를 기록합니다.
     * 
     * @note 디버그나 릴리즈 모드에서만 콘솔에 출력합니다.
     * 
     * @param message 로그 메시지입니다.
     */
    public static void Info(string message)
    {
        Record("INFO", message, System.ConsoleColor.White);
    }


    /**
     * @brief 경고 수준의 로그를 기록합니다.
     * 
     * @note 디버그나 릴리즈 모드에서만 콘솔에 출력합니다.
     * 
     * @param message 로그 메시지입니다.
     */
    public static void Warn(string message)
    {
        Record("WARN", message, System.ConsoleColor.Yellow);
    }


    /**
     * @brief 에러 수준의 로그를 기록합니다.
     * 
     * @note 디버그나 릴리즈 모드에서만 콘솔에 출력합니다.
     * 
     * @param message 로그 메시지입니다.
     */
    public static void Error(string message)
    {
        Record("ERROR", message, System.ConsoleColor.Red);
    }


    /**
     * @brief 로그 기록을 텍스트 파일로 출력합니다.
     * 
     * @param path 로그 파일이 저장될 경로와 파일 이름입니다.
     */
    public static void Export(string path)
    {
        File.WriteAllLines(path, logMessages_);
    }


    /**
     * @brief 로그를 기록합니다.
     * 
     * @param type 로그의 타입입니다.
     * @param message 로그 메시지입니다.
     * @param color 로그 메시지의 색상입니다.
     */
    private static void Record(string type, string message, System.ConsoleColor color)
    {
        string messageFormat = string.Format("[{0}|{1}] {2}", type, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), message);
        logMessages_.Add(messageFormat);

#if DEBUG || RELEASE
        System.Console.ForegroundColor = color;
        System.Console.WriteLine(messageFormat);
        System.Console.ResetColor();
#endif
    }


    /**
     * @brief 누적된 로그 메시지입니다.
     */
    private static List<string> logMessages_ = new List<string>();
}