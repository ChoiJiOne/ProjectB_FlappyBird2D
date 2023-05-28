// @third party code - BEGIN
#include <sqlite3.h>
// @third party code - END

#include <vector>
#include <unordered_map>

#include "DatabaseModule.h"

namespace DatabaseModule
{
	/**
	 * @brief 생성한 데이터베이스 핸들 수입니다.
	 */
	static int32_t countOfDBHandle = 0;

	/**
	 * @brief 데이터베이스 모듈이 관리하는 데이터베이스 핸들입니다.
	 */
	static std::vector<sqlite3*> databaseHandles;
	
	int32_t CreateDatabaseHandle(const char* path)
	{
		sqlite3* databaseHandle = nullptr;
		int32_t result = sqlite3_open(path, &databaseHandle);
		
		if (result == SQLITE_OK)
		{
			databaseHandles.push_back(databaseHandle);
			return countOfDBHandle++;
		}
		else
		{
			sqlite3_close(databaseHandle);
			return -1;
		}
	}

	void Cleanup()
	{
		for (sqlite3* databaseHandle : databaseHandles)
		{
			sqlite3_close(databaseHandle);
			databaseHandle = nullptr;
		}
	}
}