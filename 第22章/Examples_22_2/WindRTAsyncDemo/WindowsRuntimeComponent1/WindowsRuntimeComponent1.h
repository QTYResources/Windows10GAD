#pragma once
#include "ppltasks.h" 
using namespace concurrency;
using namespace Windows::Foundation;

namespace WindowsRuntimeComponent1
{
	public delegate void CurrentValue(int sum);
	public ref class WindowsPhoneRuntimeComponent sealed
	{
	public:
		WindowsPhoneRuntimeComponent();
		int Add(int x, int y);
		IAsyncOperation<int>^ AddAdync(int x, int y);
		IAsyncOperationWithProgress<int, double>^ AddWithProgressAsync(int x, int y);
		event CurrentValue^ currentValue;
	};
}