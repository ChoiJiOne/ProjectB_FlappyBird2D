#pragma once

#include <windows.h>
#include <dbghelp.h>
#include <vector>
#include <string>


/**
 * @brief 프로그램 실행 중 발생한 치명적인 오류를 처리하는 클래스입니다.
 * 
 * @note 이 클래스는 모든 멤버 변수 및 함수가 정적인 정적 클래스입니다.
 */
class CrashHandler
{
public:
	/**
	 * @brief 크래시 덤프 파일 경로를 설정합니다.
	 * 
	 * @param crashDumpFilePath 설정할 크래시 덤프 파일 경로입니다.
	 */
	static void SetCrashDumpFilePath(const std::string& crashDumpFilePath)
	{
		crashDumpFilePath_ = crashDumpFilePath;
	}


	/**
	 * @brief 크래시 에러에 대한 정보를 설정합니다.
	 * 
	 * @param crashSourceFile 크래시 에러가 발생한 소스 파일입니다.
	 * @param crashErrorLine 크래시 에러가 발생한 소스 파일 내의 라인 번호입니다.
	 * @param crashErrorMessage 크래시 에러에 대한 메시지입니다.
	 */
	static void RecordCrashError(const std::string& crashSourceFile, const int32_t& crashErrorLine, const std::string& crashErrorMessage)
	{
		crashErrorSourceFileName_ = crashSourceFile;
		crashErrorLine_ = crashErrorLine;
		lastCrashErrorMessage_ = crashErrorMessage;
	}


	/**
	 * @brief 애플리케이션의 크래시가 감지하고, 크래시 덤프와 콜스택을 기록합니다.
	 * 
	 * @param exceptionPointer 예외 정보에 대한 포인터 값입니다.
	 */
	static LONG DetectApplicationCrash(EXCEPTION_POINTERS* exceptionPointer);


private:
	/**
	 * @brief 크래시 정보를 기반으로 크래시 덤프 파일을 생성합니다.
	 *
	 * @param exceptionPointer 예외 정보에 대한 포인터 값입니다.
	 */
	static void GenerateCrashDumpFile(EXCEPTION_POINTERS* exceptionPointer);


private:
	/**
	 * @brief 크래시 덤프 파일이 저장될 경로입니다.
	 */
	static std::string crashDumpFilePath_;


	/**
	 * @brief 가장 최근에 등록된 충돌 에러 메시지입니다.
	 */
	static std::string lastCrashErrorMessage_;


	/**
	 * @brief 크래시가 발생한 소스 파일의 이름입니다.
	 */
	static std::string crashErrorSourceFileName_;


	/**
	 * @brief 크래시가 발생한 소스 파일 내의 라인 위치입니다.
	 */
	static int32_t crashErrorLine_;
};