#include <cstdint>


/**
 * @brief 데이터베이스 모듈입니다.
 */
namespace DatabaseModule
{
	/**
	 * @brief 데이터베이스 핸들을 생성합니다.
	 * 
	 * @param path DB 파일의 경로입니다.
	 * 
	 * @return 데이터베이스 핸들의 아이디 값을 반환합니다. 생성에 실패하면 -1을 반환합니다.
	 */
	extern "C" __declspec(dllexport) int32_t CreateDatabaseHandle(const char* path);


	/**
	 * @brief 데이터베이스 모듈 내의 데이터베이스 핸들을 정리합니다.
	 */
	extern "C" __declspec(dllexport) void Cleanup();



}