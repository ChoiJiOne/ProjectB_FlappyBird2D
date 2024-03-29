#pragma once

#include <functional>
#include <vector>

#include "IEntity.h"
#include "IScene.h"


/**
 * @brief 랭킹 씬입니다.
 */
class RankScene : public IScene
{
public:
	/**
	 * @brief 랭킹 씬의 디폴트 생성자입니다.
	 */
	RankScene() = default;


	/**
	 * @brief 랭킹 씬의 가상 소멸자입니다.
	 */
	virtual ~RankScene() {}


	/**
	 * @brief 랭킹 씬의 복사 생성자 및 대입 연산자를 명시적으로 삭제합니다.
	 */
	DISALLOW_COPY_AND_ASSIGN(RankScene);


	/**
	 * @brief 랭킹 씬의 한 프레임을 수행합니다.
	 *
	 * @param deltaSeconds 초 단위 델타 시간값입니다.
	 */
	virtual void Tick(float deltaSeconds) override;


	/**
	 * @brief 랭킹 씬에 진입합니다.
	 */
	virtual void Enter() override;


	/**
	 * @brief 랭킹 씬에서 나갑니다.
	 */
	virtual void Exit() override;


	/**
	 * @brief 백그라운드 엔티티의 ID 값을 설정합니다.
	 *
	 * @param id 설정할 백그라운드 엔티티의 ID 값입니다.
	 */
	void SetBackgroundID(const EUID& id) { backgroundID_ = id; }


	/**
	 * @brief 바닥 엔티티의 ID 값을 설정합니다.
	 *
	 * @param id 설정할 바닥 엔티티의 ID 값입니다.
	 */
	void SetLandID(const EUID& id) { landID_ = id; }


	/**
	 * @brief 파이프 컨트롤러 엔티티의 ID 값을 설정합니다.
	 * 
	 * @param id 설정할 파이프 컨트롤러 엔티티의 ID 값입니다.
	 */
	void SetPipeController(const EUID& id) { pipeController_ = id; }


	/**
	 * @brief 시작 씬을 연결합니다.
	 *
	 * @param startScene 설정할 시작 씬의 포인터입니다.
	 */
	void SetStartScene(IScene* startScene) { startScene_ = startScene; }


	/**
	 * @brief 루프 종료 이벤트를 설정합니다.
	 *
	 * @param quitLoopEvent 설정할 루프 종료 이벤트입니다.
	 */
	void SetQuitLoopEvent(const std::function<void()>& quitLoopEvent) { quitLoopEvent_ = quitLoopEvent; }


private:
	/**
	 * @brief 루프 종료 이벤트입니다.
	 */
	std::function<void()> quitLoopEvent_;
	

	/**
	 * @brief 시작 씬 입니다.
	 */
	IScene* startScene_ = nullptr;


	/**
	 * @brief 백그라운드 엔티티의 ID 값입니다.
	 */
	EUID backgroundID_ = -1;


	/**
	 * @brief 바닥 엔티티의 ID 값입니다.
	 */
	EUID landID_ = -1;


	/**
	 * @brief 파이프 컨트롤러 엔티티의 ID 값입니다.
	 */
	EUID pipeController_ = -1;


	/**
	 * @brief 엔티티 목록입니다.
	 */
	std::vector<EUID> entityIDs_;
};