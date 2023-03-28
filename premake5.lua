-- FlappyBird2D 게임 개발 작업 영역입니다.
workspace "FlappyBird2D"
    -- 빌드 구성 요소입니다.
    configurations { 
        "Debug", 
        "Release", 
        "Shipping" 
    }

    -- 플랫폼을 설정합니다.
    platforms { "Win64" }

    -- 솔루션 경로를 설정합니다.
    location "%{wks.name}"

    -- 클라이언트 소스 경로를 설정합니다.
    client="%{wks.location}/../Client"

    -- 서버 소스 경로를 설정합니다.
    server="%{wks.location}/../Server"

    -- 서드파티 라이브러리 경로를 설정합니다.
    thirdparty="%{wks.location}/../ThirdParty"

    -- 클라이언트 프로젝트입니다.
    project "Client"
        -- 프로젝트의 종류를 설정합니다.
        kind "ConsoleApp"

        -- 프로젝트의 언어를 설정합니다.
        language "C#"

        -- C# 버전을 8.0으로 설정합니다.
        csversion("8.0")

        -- 포인터 사용을 위해 활성화합니다.
        clr "Unsafe"

        -- 경고 무시 옵션 추가
        disablewarnings { 
            "MSB3245", 
            "MSB3246", 
        }

        -- 소스 코드 경로를 추가합니다.
        includedirs { 
            "%{client}",
            "%{thirdparty}/Include",
        }

        -- 파일을 추가합니다.
        files { 
            "%{client}/*",
            "%{thirdparty}/Include/*",
        }

        -- 라이브러리 경로를 추가합니다.
        libdirs { "%{thirdparty}/Lib", }

        -- 외부 라이브러리를 추가합니다.
        links { 
            "SDL2.lib", 
            "SDL2main.lib",
            "SDL2_image.lib", 
            "SDL2_mixer.lib", 
            "SDL2_ttf.lib", 
        }

        -- 명령행 인수를 설정합니다.
        debugargs {
            "Crash=%{wks.location}..\\Crash\\",
            "Content=%{wks.location}..\\Content\\",
        }

        -- 빌드 구성 요소의 속성을 설정합니다.
        filter "configurations:Debug"
            defines { "DEBUG" }
            runtime  "Debug"
            optimize "Off"
            symbols "On"

            -- 디버그 모드의 명령행 인수를 추가합니다.
            debugargs { "Mode=Debug", }
        
        filter "configurations:Release"
            defines { "NDEBUG", "RELEASE" }
            runtime "Release"
            optimize "On"
            symbols "On"

            -- 릴리즈 모드의 명령행 인수를 추가합니다.
            debugargs { "Mode=Release", }

        filter "configurations:Shipping"
            defines { "NDEBUG", "SHIPPING" }
            runtime "Release"
            optimize "Full"
            symbols "Off"

            -- 쉽핑 모드의 명령행 인수를 추가합니다.
            debugargs { "Mode=Shipping", }


    -- 서버 프로젝트입니다.
    project "Server"
        -- 프로젝트의 종류를 설정합니다.
        kind "ConsoleApp"

        -- 프로젝트의 언어를 설정합니다.
        language "C#"

        -- C# 버전을 8.0으로 설정합니다.
        csversion("8.0")

        -- 포인터 사용을 위해 활성화합니다.
        clr "Unsafe"

        -- 소스 코드 경로를 추가합니다.
        includedirs { "%{server}", }

        -- 파일을 추가합니다.
        files { "%{server}/*", }

        -- 빌드 구성 요소의 속성을 설정합니다.
        filter "configurations:Debug"
            defines { "DEBUG" }
            runtime  "Debug"
            optimize "Off"
            symbols "On"
        
        filter "configurations:Release"
            defines { "NDEBUG", "RELEASE" }
            runtime "Release"
            optimize "On"
            symbols "On"

        filter "configurations:Shipping"
            defines { "NDEBUG", "SHIPPING" }
            runtime "Release"
            optimize "Full"
            symbols "Off"
