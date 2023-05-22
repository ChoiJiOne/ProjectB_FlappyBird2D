#pragma once

#include <map>


/**
 * @brief INI의 섹션 영역입니다.
 */
class INISection
{
public:
	/**
	 * @brief INI 섹션 영역의 디폴트 생성자입니다.
	 */
	INISection() = default;


	/**
	 * @brief INI 섹션 영역의 복사 생성자입니다.
	 * 
	 * @param instance 복사할 INI 섹션 영역의 인스턴스입니다.
	 */
	INISection(INISection&& instance) noexcept
		: sectionData_(instance.sectionData_) {}


	/**
	 * @brief INI 섹션 영역의 복사 생성자입니다.
	 *
	 * @param instance 복사할 INI 섹션 영역의 인스턴스입니다.
	 */
	INISection(const INISection& instance)
		: sectionData_(instance.sectionData_) {}


	/**
	 * @brief INI 섹션 영역의 가상 소멸자입니다.
	 */
	virtual ~INISection() {}


	/**
	 * @brief INI 섹션 영역의 대입 연산자입니다.
	 * 
	 * @param instance 대입할 INI 섹션 영역의 인스턴스입니다.
	 * 
	 * @return 대입한 INI 섹션 영역을 반환합니다.
	 */
	INISection& operator=(INISection&& instance) noexcept
	{
		if (this == &instance) return *this;

		sectionData_ = instance.sectionData_;

		return *this;
	}


	/**
	 * @brief INI 섹션 영역의 대입 연산자입니다.
	 *
	 * @param instance 대입할 INI 섹션 영역의 인스턴스입니다.
	 *
	 * @return 대입한 INI 섹션 영역을 반환합니다.
	 */
	INISection& operator=(const INISection& instance)
	{
		if (this == &instance) return *this;

		sectionData_ = instance.sectionData_;

		return *this;
	}
	

	/**
	 * @brief 섹션에 키-값 데이터를 추가합니다.
	 * 
	 * @param key 추가할 키 값입니다.
	 * @param value 추가할 키 값에 대응하는 데이터입니다.
	 * 
	 * @throws 키 값이 이미 섹션 내에 존재하면 C++ 표준 예외를 던집니다.
	 */
	void AddData(const std::string& key, const std::string& value);

	
	/**
	 * @brief 섹션에 키-값 데이터를 강제로 추가합니다.
	 * 
	 * @note 이미 키 값이 섹션 내에 존재하면 덮어 씁니다.
	 * 
	 * @param key 추가할 키 값입니다.
	 * @param value 추가할 키 값에 대응하는 데이터입니다.
	 */
	void AddEnforceData(const std::string& key, const std::string& value);


	/**
	 * @brief 섹션 내 데이터를 얻습니다.
	 * 
	 * @param key 섹션 내에 키 값입니다.
	 * 
	 * @throws 키 값에 대응하는 값이 없으면 C++ 표준 예외를 던집니다.
	 * 
	 * @return 섹션 내 키-값 쌍을 반환합니다.
	 */
	const std::pair<std::string, std::string>& GetData(const std::string& key) const;


	/**
	 * @brief 섹션에 키와 그 키에 대응하는 값을 삭제합니다.
	 * 
	 * @note 키 값이 유효하지 않으면 아무 동작도 수행하지 않습니다.
	 * 
	 * @param key 섹션 내에 키 값입니다.
	 */
	void RemoveData(const std::string& key);


	/**
	 * @brief 키 값에 대응하는 값을 얻습니다.
	 * 
	 * @param key 섹션 내에 키 값입니다.
	 * 
	 * @throws 키 값에 대응하는 값이 존재하지 않으면 C++ 표준 예외를 던집니다.
	 * 
	 * @return 키 값에 대응하는 값을 반환합니다.
	 */
	const std::string& GetValue(const std::string& key) const;

	
	/**
	 * @brief 섹션의 키-값 데이터를 얻습니다.
	 * 
	 * @return 섹션의 키-값 데이터를 반환합니다.
	 */
	const std::map<std::string, std::string>& GetSectionData() const { return sectionData_; }


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
	 * @brief INI 섹션 내 키-값 쌍의 데이터입니다.
	 */
	std::map<std::string, std::string> sectionData_;
};