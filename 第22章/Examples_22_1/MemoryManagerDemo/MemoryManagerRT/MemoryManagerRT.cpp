// MemoryManagerRT.cpp
#include "pch.h"
#include "MemoryManagerRT.h"

using namespace MemoryManagerRT;
using namespace Platform;
using namespace Windows::System;

CMemoryManagerRT::CMemoryManagerRT()
{
}

uint64 CMemoryManagerRT::GetProcessCommittedBytes()
{
	return MemoryManager::AppMemoryUsage;
}

uint64 CMemoryManagerRT::GetProcessCommittedLimit()
{
	return MemoryManager::AppMemoryUsageLimit;
}
