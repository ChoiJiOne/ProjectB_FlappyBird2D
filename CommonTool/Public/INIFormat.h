#pragma once

#include "INISection.h"


/**
 * @brief INI 파일 포멧입니다.
 */
class INIFormat
{
public:
	/**
	 * @brief INI 파일 포멧의 디폴트 생성자입니다.
	 */
	INIFormat() = default;


	/**
	 * @brief INI 파일 포멧의 복사 생성자입니다.
	 * 
	 * @param instance 복사할 INI 파일 포멧의 인스턴스입니다.
	 */
	INIFormat(INIFormat&& instance) noexcept
		: iniSection_(instance.iniSection_) {}


	/**
	 * @brief INI 파일 포멧의 복사 생성자입니다.
	 *
	 * @param instance 복사할 INI 파일 포멧의 인스턴스입니다.
	 */
	INIFormat(const INIFormat& instance)
		: iniSection_(instance.iniSection_) {}


	/**
	 * @brief INI 파일 포멧의 가상 소멸자입니다.
	 */
	virtual ~INIFormat() {}


	/**
	 * @brief INI 파일 포멧의 대입 연산자입니다.
	 * 
	 * @param instance 대입할 INI 파일 포멧의 인스턴스입니다.
	 * 
	 * @return 대입한 INI 파일 포멧의 참조자를 반환합니다.
	 */
	INIFormat& operator=(INIFormat&& instance) noexcept
	{
		if (this == &instance) return *this;

		iniSection_ = instance.iniSection_;

		return *this;
	}


	/**
	 * @brief INI 파일 포멧의 대입 연산자입니다.
	 *
	 * @param instance 대입할 INI 파일 포멧의 인스턴스입니다.
	 *
	 * @return 대입한 INI 파일 포멧의 참조자를 반환합니다.
	 */
	INIFormat& operator=(const INIFormat& instance) noexcept
	{
		if (this == &instance) return *this;

		iniSection_ = instance.iniSection_;

		return *this;
	}


	/**
	 * @brief INI 파일 포멧에 섹션을 추가합니다.
	 * 
	 * @param section 추가할 섹션입니다.
	 * 
	 * @throws 키 값에 대응하는 섹션이 이미 존재하면 C++ 표준 예외를 던집니다.
	 */
	void AddSection(const std::string& key, const INISection& section);


	/**
	 * @brief INI 파일 포멧의 섹션을 얻습니다.
	 * 
	 * @param key 섹션의 키 값입니다.
	 * 
	 * @throws 키 값에 대응하는 섹션이 존재하지 않으면 C++ 표준 예외를 던집니다.
	 */
	INISection& GetSection(const std::string& key);


	/**
	 * @brief INI 파일 포멧의 섹션을 얻습니다.
	 *
	 * @param key 섹션의 키 값입니다.
	 *
	 * @throws 키 값에 대응하는 섹션이 존재하지 않으면 C++ 표준 예외를 던집니다.
	 */
	const INISection& GetSection(const std::string& key) const;


	/**
	 * @brief INI 파일 포멧 내의 섹션을 삭제합니다.
	 * 
	 * @note 키 값이 유효하지 않으면 아무 동작도 수행하지 않습니다.
	 * 
	 * @param key 삭제할 섹션의 키 값입니다.
	 */
	void RemoveSection(const std::string& key);


	/**
	 * @brief INI 파일 포멧 내의 섹션들을 얻습니다.
	 * 
	 * @return 키-섹션 데이터를 반환합니다.
	 */
	const std::map<std::string, INISection>& GetSections() const { return iniSection_; }


private:
	/**
	 * @brief 키 값에 대응하는 값이 존재하는지 확인합니다.
	 *
	 * @param key 값이 존재하는지 확인할 키 값입니다.
	 *
	 * @return 키 값에 대응하는 값이 존재하면 true, 그렇지 않으면 false를 반환합니다.
	 */
	bool IsValidKey(const std::string& key) const;

	
private:
	/**
	 * @brief INI 파일 내 섹션입니다.
	 */
	std::map<std::string, INISection> iniSection_;
};