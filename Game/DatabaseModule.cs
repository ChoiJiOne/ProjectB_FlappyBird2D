using System.Runtime.InteropServices;


/**
 * @brief 데이터베이스 모듈 라이브러리의 C# 바인딩입니다.
 */
class DatabaseModule
{
	/**
	 * @brief 데이터베이스 핸들을 생성합니다.
	 * 
	 * @param path DB 파일의 경로입니다.
	 * 
	 * @return 데이터베이스 핸들의 아이디 값을 반환합니다. 생성에 실패하면 -1을 반환합니다.
	 */
	[DllImport("DatabaseModule.dll", EntryPoint = "CreateDatabaseHandle", CallingConvention = CallingConvention.Cdecl)]
	public static extern int CreateDatabaseHandle([MarshalAs(UnmanagedType.LPStr)] string path);


	/**
	 * @brief 데이터베이스 모듈 내의 데이터베이스 핸들을 정리합니다.
	 */
	[DllImport("DatabaseModule.dll", EntryPoint = "Cleanup", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Cleanup();
}