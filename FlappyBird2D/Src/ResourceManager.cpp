#include "Assertion.h"
#include "ResourceManager.h"

ResourceManager& ResourceManager::Get()
{
	static ResourceManager instance;
	return instance;
}

void ResourceManager::Startup()
{
	CHECK(!bIsStartup_);
	
	bIsStartup_ = true;
}

void ResourceManager::Shutdown()
{
	CHECK(bIsStartup_);

	for (auto& cache : cache_)
	{
		if (cache && cache->IsInitialized())
		{
			cache->Release();
			cache.reset();
		}
	}

	bIsStartup_ = false;
}

void ResourceManager::Destroy(const RUID& resourceID)
{
	CHECK(0 <= resourceID && resourceID < cacheSize_);

	if (cache_[resourceID] && cache_[resourceID]->IsInitialized())
	{
		cache_[resourceID]->Release();
		cache_[resourceID].reset();
	}
}