#pragma once

#include <string>
#include <vector>
#include <memory>
#include <stdexcept>
#include <windows.h>

#include "Macro.h"


/**
 * @brief 표준 문자열에 대한 핼퍼 기능을 수행합니다.
 * 
 * @note 이 클래스는 멤버 함수 모두가 정적인 정적(static) 클래스입니다.
 */
class StringHelper
{
public:
	/**
	 * @brief 형식화된 문자열을 반환합니다. 이 메서드는 snprintf를 대체하기 위한 메서드입니다.
	 *
	 * @see https://stackoverflow.com/questions/2342162/stdstring-formatting-like-sprintf
	 *
	 * @param formatString UTF-8 기반의 형식 문자열입니다.
	 * @param ... argument 포멧팅을 수행할 데이터를 지정하는 인수입니다.
	 *
	 * @return 포멧팅이 완료된 문자열을 반환합니다.
	 */
	template<typename ... Args>
	static inline std::string Format(const std::string& formatString, Args ... argument)
	{
		size_t size = static_cast<size_t>(std::snprintf(nullptr, 0, formatString.c_str(), argument ...)) + 1;

		auto buffer = std::make_unique<char[]>(size);
		std::snprintf(buffer.get(), size, formatString.c_str(), argument ...);

		return std::string(buffer.get(), buffer.get() + size - 1);
	}


	/**
	 * @brief 형식화된 문자열을 반환합니다. 이 메서드는 swprintf를 대체하기 위한 메서드입니다.
	 *
	 * @see https://stackoverflow.com/questions/2342162/stdstring-formatting-like-sprintf
	 *
	 * @param formatString UTF-16 기반의 형식 문자열입니다.
	 * @param ... argument 포멧팅을 수행할 데이터를 지정하는 인수입니다.
	 *
	 * @return 포멧팅이 완료된 문자열을 반환합니다.
	 */
	template<typename ... Args>
	static inline std::wstring Format(const std::wstring& formatString, Args ... argument)
	{
		size_t size = static_cast<size_t>(std::swprintf(nullptr, 0, formatString.c_str(), argument ...)) + 1;

		auto buffer = std::make_unique<wchar_t[]>(size);
		std::swprintf(buffer.get(), size, formatString.c_str(), argument ...);

		return std::wstring(buffer.get(), buffer.get() + size - 1);
	}


	/**
	 * @brief 문자열을 특정 기준으로 나누어서 벡터로 만듭니다.
	 *
	 * @param textToSplit 특정 기준으로 나누어서 벡터로 만들 문자열입니다.
	 * @param splitDelimiter 문자열을 나눌 기준입니다.
	 *
	 * @return 나누어진 문자열의 벡터를 반환합니다.
	 */
	static inline std::vector<std::string> Split(const std::string& textToSplit, const std::string& splitDelimiter)
	{
		std::string splitText = textToSplit;
		std::vector<std::string> tokens;
		std::size_t position = 0;

		while ((position = splitText.find(splitDelimiter)) != std::string::npos)
		{
			tokens.push_back(splitText.substr(0, position));
			splitText.erase(0, position + splitDelimiter.length());
		}

		tokens.push_back(splitText);
		return tokens;
	}


	/**
	 * @brief 문자열을 특정 기준으로 나누어서 벡터로 만듭니다.
	 *
	 * @param textToSplit 특정 기준으로 나누어서 벡터로 만들 문자열입니다.
	 * @param splitDelimiter 문자열을 나눌 기준입니다.
	 *
	 * @return 나누어진 문자열의 벡터를 반환합니다.
	 */
	static inline std::vector<std::wstring> Split(const std::wstring& textToSplit, const std::wstring& splitDelimiter)
	{
		std::wstring splitText = textToSplit;
		std::vector<std::wstring> tokens;
		std::size_t position = 0;

		while ((position = splitText.find(splitDelimiter)) != std::wstring::npos)
		{
			tokens.push_back(splitText.substr(0, position));
			splitText.erase(0, position + splitDelimiter.length());
		}

		tokens.push_back(splitText);
		return tokens;
	}


	/**
	 * @brief UTF-8 문자열을 UTF-16 문자열로 변환합니다.
	 *
	 * @see https://learn.microsoft.com/ko-kr/windows/win32/api/stringapiset/nf-stringapiset-multibytetowidechar
	 *
	 * @param utf8String UTF-8 기반의 문자열입니다.
	 *
	 * @return 변환된 UTF-16 문자열을 반환합니다.
	 */
	static inline std::wstring Convert(const std::string& utf8String)
	{
		int32_t size = MultiByteToWideChar(CP_UTF8, 0, &utf8String[0], static_cast<int32_t>(utf8String.size()), nullptr, 0);
		CHECK((size != 0), "failed to convert UTF-8 to UTF-16");

		std::wstring utf16String(size, 0);
		CHECK((MultiByteToWideChar(CP_UTF8, 0, &utf8String[0], static_cast<int32_t>(utf8String.size()), &utf16String[0], size) != 0), "failed to convert UTF-8 to UTF-16");

		return utf16String;
	}


	/**
	 * @brief UTF-16 문자열을 UTF-8 문자열로 변환합니다.
	 *
	 * @see https://learn.microsoft.com/en-us/windows/win32/api/stringapiset/nf-stringapiset-widechartomultibyte
	 *
	 * @param utf16String UTF-16 기반의 문자열입니다.
	 *
	 * @return 변환된 UTF-8 문자열을 반환합니다.
	 */
	static inline std::string Convert(const std::wstring& utf16String)
	{
		int32_t size = WideCharToMultiByte(CP_ACP, 0, &utf16String[0], static_cast<int32_t>(utf16String.size()), nullptr, 0, nullptr, nullptr);
		CHECK((size != 0), "failed to convert UTF-16 to UTF-8");

		std::string utf8String(size, 0);
		CHECK((WideCharToMultiByte(CP_UTF8, 0, &utf16String[0], static_cast<int32_t>(utf16String.size()), &utf8String[0], size, nullptr, nullptr) != 0), "failed to convert UTF-16 to UTF-8");

		return utf8String;
	}


	/**
	 * @brief 문자열에 대응하는 해쉬 값을 추출합니다.
	 *
	 * @param utf8String 해쉬 값을 추출할 UTF-8 문자열입니다.
	 *
	 * @return 입력한 문자열로부터 추출한 해쉬값을 반환합니다.
	 */
	static inline std::size_t GetUTF8Hash(const std::string& utf8String)
	{
		return std::hash<std::string>{}(utf8String);
	}


	/**
	 * @brief 문자열에 대응하는 해쉬 값을 추출합니다.
	 *
	 * @param utf16String 해쉬 값을 추출할 UTF-16 문자열입니다.
	 *
	 * @return 입력한 문자열로부터 추출한 해쉬값을 반환합니다.
	 */
	static inline std::size_t GetUTF16Hash(const std::wstring& utf16String)
	{
		return std::hash<std::wstring>{}(utf16String);
	}
};