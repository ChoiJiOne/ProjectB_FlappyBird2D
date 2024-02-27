#pragma once

#include "IManager.h"
#include "IResource.h"


/**
 * @brief 전체 설정을 관리하는 매니저입니다.
 * 
 * @note 이 클래스는 싱글턴입니다.
 */
class ConfigManager : public IManager
{
public:
	/**
	 * @brief 게임의 백그라운드 종류입니다.
	 */
	enum class EBackground : int32_t
	{
		Day   = 0x00,
		Night = 0x01,
	};


public:
	/**
	 * @brief 전체 설정을 관리하는 매니저의 복사 생성자와 대입 연산자를 명시적으로 삭제합니다.
	 */
	DISALLOW_COPY_AND_ASSIGN(ConfigManager);


	/**
	 * @brief 전체 설정을 관리하는 매니저의 참조 인스턴스를 얻습니다.
	 *
	 * @return 전체 설정을 관리하는 매니저의 참조 인스턴스를 반환합니다.
	 */
	static ConfigManager& Get();


	/**
	 * @brief 전체 설정을 관리하는 매니저의 사용을 시작합니다.
	 *
	 * @note 이 메서드는 한 번만 호출되어야 합니다.
	 */
	virtual void Startup() override;


	/**
	 * @brief 전체 설정을 관리하는 매니저의 사용을 종료합니다.
	 *
	 * @note
	 * - 애플리케이션 종료 전에 이 메서드를 반드시 호출하여 내부를 정리해야 합니다.
	 * - 이 메서드는 반드시 한 번만 호출되어야 합니다.
	 */
	virtual void Shutdown() override;


	/**
	 * @brief 현재 배경 리소스의 ID 값을 얻습니다.
	 * 
	 * @return 현재 배경 리소스의 ID 값을 반환합니다.
	 */
	RUID GetCurrentBackgroundID() { return currentBackgroundID_; }


	/**
	 * @brief 현재 배경을 설정합니다.
	 * 
	 * @param background 설정할 백그라운드입니다.
	 */
	void SetCurrentBackgroundID(const EBackground& background);


private:
	/**
	 * @brief 전체 설정을 관리하는 매니저에 디폴트 생성자와 빈 가상 소멸자를 삽입합니다.
	 */
	DEFAULT_CONSTRUCTOR_AND_VIRTUAL_DESTRUCTOR(ConfigManager);


private:
	/**
	 * @brief 현재 배경 리소스의 ID 값입니다.
	 */
	RUID currentBackgroundID_ = -1;

	
	/**
	 * @brief 낮 배경 리소스의 ID 값입니다.
	 */
	RUID dayBackgroundID_ = -1;


	/**
	 * @brief 밤 배경 리소스의 ID 값입니다.
	 */
	RUID nightBackgroundID_ = -1;
};