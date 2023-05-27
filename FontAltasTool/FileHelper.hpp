#pragma once

#include <shlwapi.h>

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


	/**
	 * @brief 버퍼를 파일에 씁니다.
	 * 
	 * @note 파일 경로는 UTF-8 문자열입니다.
	 * 
	 * @param path 파일의 경로입니다.
	 * @param buffer 파일에 쓸 버퍼입니다.
	 * 
	 * @throws 파일 쓰기에 실패하면 C++ 표준 예외를 던집니다.
	 * 
	 * @see https://learn.microsoft.com/ko-kr/windows/win32/api/fileapi/nf-fileapi-createfilea
	 */
	static inline void WriteBufferFromFile(const std::string& path, const std::vector<uint8_t>& buffer)
	{
		HANDLE fileHandle = CreateFileA(path.c_str(), GENERIC_WRITE, FILE_SHARE_READ, nullptr, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
		CHECK((fileHandle != INVALID_HANDLE_VALUE), "failed to create file...");

		DWORD bytesWrite;

		CHECK(WriteFile(fileHandle, &buffer[0], static_cast<DWORD>(buffer.size()), &bytesWrite, nullptr), "failed to write file...");
		CHECK(CloseHandle(fileHandle), "failed to close file...");
	}


	/**
	 * @brief 버퍼를 파일에 씁니다.
	 *
	 * @note 파일 경로는 UTF-16 문자열입니다.
	 *
	 * @param path 파일의 경로입니다.
	 * @param buffer 파일에 쓸 버퍼입니다.
	 *
	 * @throws 파일 쓰기에 실패하면 C++ 표준 예외를 던집니다.
	 * 
	 * @see https://learn.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew
	 */
	static inline void WriteBufferFromFile(const std::wstring& path, const std::vector<uint8_t>& buffer)
	{
		HANDLE fileHandle = CreateFileW(path.c_str(), GENERIC_WRITE, FILE_SHARE_READ, nullptr, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
		CHECK((fileHandle != INVALID_HANDLE_VALUE), "failed to create file...");

		DWORD bytesWrite;

		CHECK(WriteFile(fileHandle, &buffer[0], static_cast<DWORD>(buffer.size()), &bytesWrite, nullptr), "failed to write file...");
		CHECK(CloseHandle(fileHandle), "failed to close file...");
	}


	/**
	 * @brief 경로가 디렉토리인지 검사합니다.
	 * 
	 * @note 디렉토리 경로는 UTF-8 문자열입니다.
	 * 
	 * @param directoryPath 디렉토리인지 검사할 경로입니다.
	 * 
	 * @return 경로가 디렉토리라면 true, 그렇지 않으면 false를 반환합니다.
	 */
	static inline bool IsValidDirectory(const std::string& directoryPath)
	{
		return PathIsDirectoryA(directoryPath.c_str());
	}


	/**
	 * @brief 경로가 디렉토리인지 검사합니다.
	 *
	 * @note 디렉토리 경로는 UTF-16 문자열입니다.
	 *
	 * @param directoryPath 디렉토리인지 검사할 경로입니다.
	 *
	 * @return 경로가 디렉토리라면 true, 그렇지 않으면 false를 반환합니다.
	 */
	static inline bool IsValidDirectory(const std::wstring& directoryPath)
	{
		return PathIsDirectoryW(directoryPath.c_str());
	}


	/**
	 * @brief 파일이 유효한지 검사합니다.
	 * 
	 * @note 디렉토리 경로는 UTF-8 문자열입니다.
	 * 
	 * @param filePath 유효한지 검사할 파일 경로입니다.
	 * 
	 * @return 파일이 유효하다면 true, 그렇지 않으면 false를 반환합니다.
	 */
	static inline bool IsValidFile(const std::string& filePath)
	{
		return PathFileExistsA(filePath.c_str());
	}


	/**
	 * @brief 파일이 유효한지 검사합니다.
	 *
	 * @note 디렉토리 경로는 UTF-16 문자열입니다.
	 *
	 * @param filePath 유효한지 검사할 파일 경로입니다.
	 *
	 * @return 파일이 유효하다면 true, 그렇지 않으면 false를 반환합니다.
	 */
	static inline bool IsValidFile(const std::wstring& filePath)
	{
		return PathFileExistsW(filePath.c_str());
	}
};