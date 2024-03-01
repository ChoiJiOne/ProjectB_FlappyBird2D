#pragma once

#include "IEntity.h"
#include "IResource.h"


/**
 * @brief 백그라운드 엔티티입니다.
 */
class Background : public IEntity
{
public:
	/**
	 * @brief 백그라운드 엔티티의 생성자입니다.
	 */
	explicit Background();


	/**
	 * @brief 백그라운드 엔티티의 생성자입니다.
	 * 
	 * @param scrollSpeed 백그라운드의 스크롤 속도입니다.
	 */
	explicit Background(float scrollSpeed);


	/**
	 * @brief 백그라운드 엔티티의 가상 소멸자입니다.
	 */
	virtual ~Background();


	/**
	 * @brief 백그라운드 엔티티의 복사 생성자와 대입 연산자를 명시적으로 삭제합니다.
	 */
	DISALLOW_COPY_AND_ASSIGN(Background);


	/**
	 * @brief 백그라운드를 업데이트합니다.
	 * 
	 * @param deltaSeconds 델타 시간 값입니다.
	 */
	virtual void Tick(float deltaSeconds) override;


	/**
	 * @brief 백그라운드를 화면에 그립니다.
	 */
	virtual void Render() override;


	/**
	 * @brief 백그라운드 내의 리소스를 할당 해제합니다.
	 */
	virtual void Release() override;


private:
	/**
	 * @brief 백그라운드가 움직일 수 있는지 확인합니다.
	 */
	bool bCanMove_ = false;


	/**
	 * @brief 백그라운드의 스크롤 위치입니다.
	 */
	float scrollX_ = 0.0f;


	/**
	 * @brief 백그라운드의 최대 스크롤 위치입니다.
	 */
	float maxScrollX_ = 0.0f;


	/**
	 * @brief 백그라운드의 스크롤 속도입니다.
	 * 
	 * @note 속도의 단위는 픽셀/초 입니다.
	 */
	float scrollSpeed_ = 0.0f;


	/**
	 * @brief 백그라운드 텍스처 리소스의 ID입니다.
	 */
	RUID textureID_ = -1;
};