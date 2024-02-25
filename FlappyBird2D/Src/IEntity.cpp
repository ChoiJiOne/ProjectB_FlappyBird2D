#include "IEntity.h"

IEntity::~IEntity()
{
	if (bIsInitialized_)
	{
		Release();
	}
}