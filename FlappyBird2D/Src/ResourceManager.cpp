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
	
	resourceCache_ = std::unordered_map<std::string, std::unique_ptr<IResource>>();

	bIsStartup_ = true;
}

void ResourceManager::Shutdown()
{
	CHECK(bIsStartup_);

	for (auto& resource : resourceCache_)
	{
		resource.second->Release();
		resource.second.reset();
	}

	bIsStartup_ = false;
}

void ResourceManager::DestroyResource(const std::string& signature)
{
	if (IsValidKey(signature))
	{
		IResource* resource = resourceCache_.at(signature).get();
		if (resource && resource->IsInitialized())
		{
			resource->Release();
		}

		resourceCache_.erase(signature);
	}
}

bool ResourceManager::IsValidKey(const std::string& key)
{
	return resourceCache_.find(key) != resourceCache_.end();
}