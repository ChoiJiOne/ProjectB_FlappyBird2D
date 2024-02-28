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
#include "Background.h"
#include "BoundBox2D.h"
#include "BoundCircle2D.h"
#include "Button.h"
#include "ConfigManager.h"
#include "EntityManager.h"
#include "GameTimer.h"
#include "GeometryPass2D.h"
#include "GlyphPass2D.h"
#include "InputManager.h"
#include "Panel.h"
#include "MainTitle.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "SDLManager.h"
#include "Shader.h"
#include "SpritePass2D.h"
#include "StringUtils.h"
#include "Texture2D.h"
#include "TTFont.h"

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

	InputManager::Get().AddWindowEventAction(EWindowEvent::CLOSE, [&]() {bIsDone = true; }, true);

	RUID font90ID = ResourceManager::Get().Create<TTFont>("Resource/Font/Flappy_Font.ttf", 0x00, 127, 90.0f);
	RUID font64ID = ResourceManager::Get().Create<TTFont>("Resource/Font/Flappy_Font.ttf", 0x00, 127, 64.0f);
	RUID font32ID = ResourceManager::Get().Create<TTFont>("Resource/Font/Flappy_Font.ttf", 0x00, 127, 32.0f);
	
	//std::vector<EUID> entites = 
	//{
	//	EntityManager::Get().Create<Background>(),
	//	EntityManager::Get().Create<MainTitle>(font90ID),
	//	EntityManager::Get().Create<Button>("Resource/Button/Start.json", font32ID, EMouseButton::Left, [&]() { SDL_Log("CLICK START!"); }),
	//	EntityManager::Get().Create<Button>("Resource/Button/Setting.json", font32ID, EMouseButton::Left, [&]() { SDL_Log("CLICK SETTING!"); }),
	//	EntityManager::Get().Create<Button>("Resource/Button/Quit.json", font32ID, EMouseButton::Left, [&]() { bIsDone = true; }),
	//};

	std::vector<EUID> entites = 
	{
		EntityManager::Get().Create<Background>(),
		EntityManager::Get().Create<Panel>("Resource/Panel/Setting.json", font64ID),
		EntityManager::Get().Create<Panel>("Resource/Panel/Background.json", font32ID),
		EntityManager::Get().Create<Panel>("Resource/Panel/Level.json", font32ID),
		EntityManager::Get().Create<Panel>("Resource/Panel/Bird.json", font32ID),

		EntityManager::Get().Create<Button>("Resource/Button/Day.json", font32ID, EMouseButton::Left, [&]() {
			ConfigManager::Get().SetCurrentBackgroundID(ConfigManager::EBackground::Day);
		}),
		EntityManager::Get().Create<Button>("Resource/Button/Night.json", font32ID, EMouseButton::Left, [&]() {
			ConfigManager::Get().SetCurrentBackgroundID(ConfigManager::EBackground::Night);
		}),

		EntityManager::Get().Create<Button>("Resource/Button/Easy.json", font32ID, EMouseButton::Left, [&]() {
			ConfigManager::Get().SetCurrentLevel(ConfigManager::ELevel::Easy);
		}),
		EntityManager::Get().Create<Button>("Resource/Button/Normal.json", font32ID, EMouseButton::Left, [&]() {
			ConfigManager::Get().SetCurrentLevel(ConfigManager::ELevel::Normal);
		}),
		EntityManager::Get().Create<Button>("Resource/Button/Hard.json", font32ID, EMouseButton::Left, [&]() {
			ConfigManager::Get().SetCurrentLevel(ConfigManager::ELevel::Hard);
		}),

		EntityManager::Get().Create<Button>("Resource/Button/Blue.json", font32ID, EMouseButton::Left, [&]() {
			ConfigManager::Get().SetCurrentBird(ConfigManager::EBird::Blue);
		}),
		EntityManager::Get().Create<Button>("Resource/Button/Red.json", font32ID, EMouseButton::Left, [&]() {
			ConfigManager::Get().SetCurrentBird(ConfigManager::EBird::Red);
		}),
		EntityManager::Get().Create<Button>("Resource/Button/Yellow.json", font32ID, EMouseButton::Left, [&]() {
			ConfigManager::Get().SetCurrentBird(ConfigManager::EBird::Yellow);
		}),
		
		EntityManager::Get().Create<Button>("Resource/Button/Back.json", font32ID, EMouseButton::Left, [&]() {
			SDL_Log("CLICK BACK!");
		}),
	};

	GameTimer timer;
	timer.Reset();

	while (!bIsDone)
	{
		timer.Tick();
		InputManager::Get().Tick();

		EntityManager::Get().UpdateBatch(entites, timer.GetDeltaSeconds());
		
		RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);

		EntityManager::Get().RenderBatch(entites);

		RenderManager::Get().EndFrame();
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