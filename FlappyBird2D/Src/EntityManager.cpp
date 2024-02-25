#include "Assertion.h"
#include "EntityManager.h"

EntityManager& EntityManager::Get()
{
	static EntityManager instance;
	return instance;
}

void EntityManager::Startup()
{
	CHECK(!bIsStartup_);

	cache_ = std::array<std::unique_ptr<IEntity>, MAX_RESOURCE_SIZE>();

	bIsStartup_ = true;
}

void EntityManager::Shutdown()
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

void EntityManager::Destroy(const EUID& entityID)
{
	CHECK(0 <= entityID && entityID < cacheSize_);

	if (cache_[entityID] && cache_[entityID]->IsInitialized())
	{
		cache_[entityID]->Release();
		cache_[entityID].reset();
	}
}