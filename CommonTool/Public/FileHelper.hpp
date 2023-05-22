#pragma once

#include "Macro.h"


/**
 * @brief 파일 처리에 대한 핼퍼 기능을 수행합니다.
 * 
 * @note 이 클래스는 멤버 함수 모두가 정적인 정적(static) 클래스입니다.
 */
class FileHelper
{
public:
	/**
	 * @brief 파일을 읽고 버퍼에 저장합니다.
	 *
	 * @note 파일 경로의 문자열은 UTF-8 입니다.
	 *
	 * @see https://learn.microsoft.com/ko-kr/windows/win32/api/fileapi/nf-fileapi-createfilea
	 *
	 * @param path 파일의 경로입니다.
	 * @param outBuffer[out] 읽은 파일을 저장할 버퍼입니다.
	 *
	 * @throws
	 * 파일 생성에 실패하면 C++ 표준 예외를 던집니다.
	 * 파일 읽기에 실패하면 C++ 표준 예외를 던집니다.
	 */
	static inline void ReadBufferFromFile(const std::string& path, std::vector<uint8_t>& outBuffer)
	{
		HANDLE fileHandle = CreateFileA(path.c_str(), GENERIC_READ, FILE_SHARE_READ, nullptr, OPEN_EXISTING, 0, nullptr);
		CHECK((fileHandle != INVALID_HANDLE_VALUE), "failed to create file");

		DWORD fileSize = GetFileSize(fileHandle, nullptr);

		outBuffer.resize(fileSize);
		DWORD bytesRead;

		CHECK(ReadFile(fileHandle, &outBuffer[0], fileSize, &bytesRead, nullptr), "failed read file...");
		CHECK(CloseHandle(fileHandle), "failed to close file...");
	}


	/**
	 * @brief 파일을 읽고 버퍼에 저장합니다.
	 *
	 * @note 파일 경로의 문자열은 UTF-16 입니다.
	 *
	 * @param path 파일의 경로입니다.
	 * @param outBuffer[out] 읽은 파일을 저장할 버퍼입니다.
	 *
	 * @throws 파일 읽기에 실패하면 C++ 표준 예외를 던집니다.
	 */
	static inline void ReadBufferFromFile(const std::wstring& path, std::vector<uint8_t>& outBuffer)
	{
		HANDLE fileHandle = CreateFileW(path.c_str(), GENERIC_READ, FILE_SHARE_READ, nullptr, OPEN_EXISTING, 0, nullptr);
		CHECK((fileHandle != INVALID_HANDLE_VALUE), "failed to create file...");

		DWORD fileSize = GetFileSize(fileHandle, nullptr);

		outBuffer.resize(fileSize);
		DWORD bytesRead;

		CHECK(ReadFile(fileHandle, &outBuffer[0], fileSize, &bytesRead, nullptr), "failed read file...");
		CHECK(CloseHandle(fileHandle), "failed to close file...");
	}
};