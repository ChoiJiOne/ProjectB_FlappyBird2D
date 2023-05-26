#pragma once

#include <string>
#include <unordered_map>


/**
 * @brief 커맨드 라인을 파싱하고 인자 값의 쿼리를 수행하는 클래스입니다.
 *
 * @note 이 클래스는 모든 멤버 함수와 변수가 정적인 정적 클래스입니다.
 */
class CommandLine
{
public:
	/**
	 * @brief 명령행 인자 값을 파싱합니다.
	 *
	 * @param argc 명령행 인자의 수입니다.
	 * @param argv 명령행 인자입니다.
	 */
	static void Parse(int32_t argc, char* argv[]);


	/**
	 * @brief 실행 파일 경로를 얻습니다.
	 *
	 * @return 실행 파일 경로를 반환합니다.
	 */
	static std::string GetExecutePath() { return executePath_; }


	/**
	 * @brief 키 값이 유효한지 검사합니다.
	 *
	 * @param key 유효한지 검사할 키 값입니다.
	 *
	 * @return 키 값에 대응하는 값이 있다면 true, 그렇지 않다면 false를 반환합니다.
	 */
	static bool IsValid(const std::string& key);


	/**
	 * @brief 키 값에 대응하는 값을 얻습니다.
	 *
	 * @param key 얻으려고 하는 값의  키 값입니다.
	 *
	 * @return 키 값에 대응하는 값을 반환합니다.
	 */
	static std::string GetValue(const std::string& key);


private:
	/**
	 * @brief 실행 파일 경로입니다.
	 */
	static std::string executePath_;


	/**
	 * @brief 명령행 인자의 키-값 쌍입니다.
	 */
	static std::unordered_map<std::string, std::string> arguments_;
};