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
#include "GlyphPass2D.h"
#include "RenderManager.h"
#include "ResourceManager.h"
#include "SDLManager.h"
#include "Shader.h"
#include "SpritePass2D.h"
#include "Texture2D.h"
#include "TTFont.h"

int32_t WINAPI wWinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPWSTR pCmdLine, _In_ int32_t nCmdShow)
{
	CHECK(CrashModule::RegisterExceptionFilter());

	SDLManager::Get().Startup();
	ResourceManager::Get().Startup();
	RenderManager::Get().Startup();

	RenderManager::Get().SetDepthMode(false);
	RenderManager::Get().SetAlphaBlendMode(true);
	RenderManager::Get().SetMultisampleMode(true);

	RUID backgroundID = ResourceManager::Get().Create<Texture2D>("Resource/Texture/Background_Day.png");

	RUID fontID = ResourceManager::Get().Create<TTFont>("Resource/Font/Flappy_Font.ttf", 32, 127, 64.0f);

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

		static float time = 0.0f;
		time += 0.001f;
		if (time >= 1.0f)
		{
			time -= 1.0f;
		}

		RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);

		RenderManager::Get().RenderHorizonScrollSprite2D(backgroundID, time);
		RenderManager::Get().RenderText2D(fontID, L"Hello, World!", Vec2f(310.0f, 410.0f), Vec4f(0.5f, 0.5f, 0.5f, 0.7f));
		RenderManager::Get().RenderText2D(fontID, L"Hello, World!", Vec2f(300.0f, 400.0f), Vec4f(1.0f, 0.0f, 1.0f, 1.0f));
				
		RenderManager::Get().EndFrame();
	}

	ResourceManager::Get().Shutdown();
	RenderManager::Get().Shutdown();
	SDLManager::Get().Shutdown();

	CrashModule::UnregisterExceptionFilter();
	return 0;
}