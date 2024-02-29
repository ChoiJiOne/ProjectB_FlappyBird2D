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
#include "RenderManager.h"
#include "ResourceManager.h"
#include "SDLManager.h"
#include "SettingScene.h"
#include "StartScene.h"

bool bIsDone = false;

int32_t WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR pCmdLine, _In_ int32_t nCmdShow)
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

	std::function<void()> quitLoopEvent = [&]() { bIsDone = true; };

	InputManager::Get().AddWindowEventAction(EWindowEvent::CLOSE, quitLoopEvent, true);

	StartScene startScene;
	SettingScene settingScene;

	startScene.SetQuitLoopEvent(quitLoopEvent);
	startScene.SetSettingScene(&settingScene);

	settingScene.SetStartScene(&startScene);

	IScene* currentScene = &startScene;
	currentScene->Enter();
	
	GameTimer timer;
	timer.Reset();

	while (!bIsDone)
	{
		timer.Tick();
		InputManager::Get().Tick();

		currentScene->Tick(timer.GetDeltaSeconds());
		
		if (currentScene->IsDetectSwitch())
		{
			currentScene->Exit();
			currentScene = currentScene->GetLink();
			currentScene->Enter();
		}
	}
	
	ConfigManager::Get().Shutdown();
	EntityManager::Get().Shutdown();
	ResourceManager::Get().Shutdown();
	RenderManager::Get().Shutdown();
	AudioManager::Get().Shutdown();
	InputManager::Get().Shutdown();
	SDLManager::Get().Shutdown();

	CrashModule::UnregisterExceptionFilter();
	return 0;
}