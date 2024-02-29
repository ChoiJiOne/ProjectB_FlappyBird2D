#include <array>
#include <cstdint>
#include <windows.h>
#include <vector>

#include <glad/glad.h>
#include <SDL.h>

#include "CrashModule.h"
#include "FileModule.h"
#include "MathModule.h"

#include "Assertion.h"
#include "AudioManager.h"
#include "ConfigManager.h"
#include "EntityManager.h"
#include "GameTimer.h"
#include "InputManager.h"
#include "PlayScene.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "SDLManager.h"
#include "SettingScene.h"
#include "StartScene.h"


/**
 * @brief 게임 애플리케이션 클래스입니다.
 */
class Application
{
public:
	/**
	 * @brief 게임 애플리케이션의 생성자입니다.
	 */
	explicit Application()
	{
		CHECK(CrashModule::RegisterExceptionFilter());

		SDLManager::Get().Startup();
		InputManager::Get().Startup();
		AudioManager::Get().Startup();
		RenderManager::Get().Startup();
		ResourceManager::Get().Startup();
		EntityManager::Get().Startup();
		ConfigManager::Get().Startup();

		RenderManager::Get().SetDepthMode(false);
		RenderManager::Get().SetAlphaBlendMode(true);
		RenderManager::Get().SetMultisampleMode(true);
	}


	/**
	 * @brief 게임 애플리케이션의 가상 소멸자입니다.
	 */
	virtual ~Application()
	{
		ConfigManager::Get().Shutdown();
		EntityManager::Get().Shutdown();
		ResourceManager::Get().Shutdown();
		RenderManager::Get().Shutdown();
		AudioManager::Get().Shutdown();
		InputManager::Get().Shutdown();
		SDLManager::Get().Shutdown();

		CrashModule::UnregisterExceptionFilter();
	}


	/**
	 * @brief 게임 애플리케이션의 복사 생성자 및 대입 연산자를 명시적으로 삭제합니다.
	 */
	DISALLOW_COPY_AND_ASSIGN(Application);


	/**
	 * @brief 애플리케이션 초기화를 수행합니다.
	 */
	void Startup()
	{
		quitLoopEvent_ = [&]() { bIsDone_ = true; };

		InputManager::Get().AddWindowEventAction(EWindowEvent::CLOSE, quitLoopEvent_, true);

		startScene_ = std::make_unique<StartScene>();
		settingScene_ = std::make_unique<SettingScene>();
		playScene_ = std::make_unique<PlayScene>();
		
		StartScene* startScene = reinterpret_cast<StartScene*>(startScene_.get());
		startScene->SetQuitLoopEvent(quitLoopEvent_);
		startScene->SetSettingScene(settingScene_.get());
		startScene->SetPlayScene(playScene_.get());

		SettingScene* settingScene = reinterpret_cast<SettingScene*>(settingScene_.get());
		settingScene->SetStartScene(startScene);

		PlayScene* playScene = reinterpret_cast<PlayScene*>(playScene_.get());
		playScene->SetStartScene(startScene);

		currentScene_ = startScene;
	}


	/**
	 * @brief 애플리케이션을 실행합니다.
	 */
	void Run()
	{
		currentScene_->Enter();
		timer_.Reset();

		while (!bIsDone_)
		{
			timer_.Tick();
			InputManager::Get().Tick();

			currentScene_->Tick(timer_.GetDeltaSeconds());

			if (currentScene_->IsDetectSwitch())
			{
				currentScene_->Exit();
				currentScene_ = currentScene_->GetLink();
				currentScene_->Enter();
			}
		}
	}


	/**
	 * @brief 애플리케이션의 실행을 종료합니다.
	 */
	void Shutdown()
	{
	}


private:
	/**
	 * @brief 루프 종료 여부입니다.
	 */
	bool bIsDone_ = false;


	/**
	 * @brief 루프 종료 이벤트입니다.
	 */
	std::function<void()> quitLoopEvent_;


	/**
	 * @brief 게임 타이머입니다.
	 */
	GameTimer timer_;


	/**
	 * @brief 시작 씬입니다.
	 */
	std::unique_ptr<IScene> startScene_ = nullptr;


	/**
	 * @brief 세팅 씬입니다.
	 */
	std::unique_ptr<IScene> settingScene_ = nullptr;


	/**
	 * @brief 플레이 씬입니다.
	 */
	std::unique_ptr<IScene> playScene_ = nullptr;


	/**
	 * @brief 현재 씬입니다.
	 */
	IScene* currentScene_ = nullptr;
};

int32_t WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR pCmdLine, _In_ int32_t nCmdShow)
{
	std::unique_ptr<Application> app = std::make_unique<Application>();
	app->Startup();
	app->Run();
	app->Shutdown();
	return 0;
}