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

    -- 오디오 모듈 경로를 설정합니다.
    audiomodule="%{wks.location}/../AudioModule"

    -- 서드파티 라이브러리 경로를 설정합니다.
    thirdparty="%{wks.location}/../ThirdParty"


    -- 오디오 모듈 프로젝트입니다.
    project "AudioModule"
        -- 오디오 모듈 프로젝트의 종류를 설정합니다.
        kind "SharedLib"

        -- 프로그래밍 언어를 설정합니다.
        language "C++"

        -- C++의 표준을 설정합니다.
        cppdialect "C++17"

        -- 오디오 모듈 소스 코드의 include 경로를 추가합니다.
        includedirs { 
            "%{audiomodule}/Source", 
            "%{audiomodule}/ThirdParty",
        }

        -- 오디오 모듈 소스 코드의 file을 추가합니다.
        files { 
            "%{audiomodule}/Source/*", 
            "%{audiomodule}/ThirdParty/*",
        }

        -- 빌드의 구성 요소별 설정을 수행합니다.
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
            "System",
            "AudioModule",
            "SDL2.lib", 
            "SDL2main.lib",
            "SDL2_image.lib",
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