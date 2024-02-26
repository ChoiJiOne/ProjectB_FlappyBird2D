#pragma once


/**
 * @brief 어플리케이션 크래시에 대한 기능을 제공합니다.
 */
namespace CrashModule
{
	/**
	 * @brief 크래시 모듈의 에러 메시지를 얻습니다.
	 *
	 * @return 크래시 모듈의 C스타일 에러 메시지를 반환합니다.
	 */
	const char* GetErrorMessage();


	/**
	 * @brief 최상위 예외 처리기를 등록합니다.
	 * 
	 * @return 최상위 예외 처리기 등록에 성공하면 true, 그렇지 않으면 false를 반환합니다.
	 *
	 * @see https://learn.microsoft.com/ko-kr/windows/win32/api/errhandlingapi/nf-errhandlingapi-setunhandledexceptionfilter
	 */
	bool RegisterExceptionFilter();


	/**
	 * @brief 최상위 예외 처리기 등록을 해제합니다.
	 */
	void UnregisterExceptionFilter();
}