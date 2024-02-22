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
#include "GeometryPass2D.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "SDLManager.h"
#include "Shader.h"
#include "SpritePass2D.h"
#include "Texture2D.h"

int32_t WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR pCmdLine, _In_ int32_t nCmdShow)
{
	CHECK(CrashModule::RegisterExceptionFilter());

	SDLManager::Get().Startup();
	RenderManager::Get().Startup();
	ResourceManager::Get().Startup();

	RenderManager::Get().SetDepthMode(false);
	RenderManager::Get().SetAlphaBlendMode(true);
	RenderManager::Get().SetMultisampleMode(true);

	Texture2D* background = ResourceManager::Get().CreateResource<Texture2D>("Background");
	background->Initialize("Resource/Texture/Background.png");

	Texture2D* bird = ResourceManager::Get().CreateResource<Texture2D>("Bird"); 
	bird->Initialize("Resource/Texture/bird.png");

	GeometryPass2D* pass2d = ResourceManager::Get().CreateResource<GeometryPass2D>("GeometryPass2D");
	pass2d->Initialize();

	SpritePass2D* passSprite2d = ResourceManager::Get().CreateResource<SpritePass2D>("SpritePass2D");
	passSprite2d->Initialize();

	Mat4x4f ortho = MathModule::CreateOrtho(0.0f, 600.0f, 800.0f, 0.0f, -1.0f, 1.0f);

	std::vector<Vec2f> positions;
	for (int32_t count = 0; count < 1000; ++count)
	{
		positions.push_back(Vec2f(
			MathModule::GenerateRandomFloat(0.0f, 600.0f),
			MathModule::GenerateRandomFloat(0.0f, 800.0f)
		));
	}

	std::vector<Vec2f> polygon =
	{
		Vec2f(10.0f, 10.0f),
		Vec2f(20.0f, 100.0f),
		Vec2f(50.0f, 150.0f),
		Vec2f(70.0f, 120.0f),
		Vec2f(60.0f, 1.0f),
	};

	SDL_Event e;
	bool bIsDone = false;
	while (!bIsDone)
	{
		while (SDL_PollEvent(&e))
		{
			switch (e.type)
			{
			case SDL_QUIT:
				bIsDone = true;
				break;
			}
		}

		RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);
		
		static float time = 0.0f;
		time += 0.001f;

		//passSprite2d->DrawVerticalScrollSprite2D(background, time);
		passSprite2d->DrawVerticalScrollSprite2D(
			ortho,
			background,
			Vec2f(300.0f, 400.0),
			600.0f,
			800.0f,
			0.0f,
			time
		);

		passSprite2d->DrawOutlineSprite2D(ortho, bird, Vec2f(300.0f, 400.0), 50.0f, 50.0f, 0.0f, Vec4f(1.0f, 0.0f, 0.0f, 1.0f));

		RenderManager::Get().EndFrame();
	}

	ResourceManager::Get().Shutdown();
	RenderManager::Get().Shutdown();
	SDLManager::Get().Shutdown();

	CrashModule::UnregisterExceptionFilter();
	return 0;
}