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
#include "BoundBox2D.h"
#include "BoundCircle2D.h"
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

	Vec4f color = Vec4f(0.0f, 0.0f, 1.0f, 1.0f);

	BoundBox2D box0(Vec2f(300.0f, 400.0f), 200.0f, 100.0f);
	BoundBox2D box1(Vec2f(300.0f, 400.0f), 200.0f, 100.0f);
	BoundCircle2D circle0(Vec2f(300.0f, 400.0f), 100.0f);
	BoundCircle2D circle1(Vec2f(300.0f, 400.0f), 100.0f);
	
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

		int32_t x = 0 , y = 0;
		SDL_GetMouseState(&x, &y);
		box0.SetCenter(Vec2f(static_cast<int32_t>(x), static_cast<int32_t>(y)));
		if (box0.Intersect(&circle0))
		{
			color = Vec4f(1.0f, 0.0f, 0.0f, 1.0f);
		}
		else
		{
			color = Vec4f(0.0f, 0.0f, 1.0f, 1.0f);
		}

		//circle0.SetCenter(Vec2f(static_cast<int32_t>(x), static_cast<int32_t>(y)));

		//if (circle0.Intersect(&circle1))
		//{
		//	color = Vec4f(1.0f, 0.0f, 0.0f, 1.0f);
		//}
		//else
		//{
		//	color = Vec4f(0.0f, 0.0f, 1.0f, 1.0f);
		//}

		//box0.SetCenter(Vec2f(static_cast<int32_t>(x), static_cast<int32_t>(y)));

		//if (box0.Intersect(&box1))
		//{
		//	color = Vec4f(1.0f, 0.0f, 0.0f, 1.0f);
		//}
		//else
		//{
		//	color = Vec4f(0.0f, 0.0f, 1.0f, 1.0f);
		//}

		RenderManager::Get().BeginFrame(0.0f, 0.0f, 0.0f, 1.0f);

		RenderManager::Get().RenderSprite2D(backgroundID);

		RenderManager::Get().RenderRectangle2D(box0.GetCenter(), box0.GetWidth(), box0.GetHeight(), 0.0f, color);
		//RenderManager::Get().RenderRectangle2D(box1.GetCenter(), box1.GetWidth(), box1.GetHeight(), 0.0f, color);
		RenderManager::Get().RenderCircle2D(circle0.GetCenter(), circle0.GetRadius(), color);
		//RenderManager::Get().RenderCircle2D(circle1.GetCenter(), circle1.GetRadius(), color);

		RenderManager::Get().EndFrame();
	}

	ResourceManager::Get().Shutdown();
	RenderManager::Get().Shutdown();
	SDLManager::Get().Shutdown();

	CrashModule::UnregisterExceptionFilter();
	return 0;
}