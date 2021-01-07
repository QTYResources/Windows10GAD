#include "pch.h"
#include "WindowsRuntimeComponent1.h"

using namespace Platform;
using namespace WindowsRuntimeComponent1;

WindowsPhoneRuntimeComponent::WindowsPhoneRuntimeComponent()
{
}

int WindowsPhoneRuntimeComponent::Add(int x, int y)
{
	auto f1 = [x, &y]()->int
	{
		int z = x + y;
		return z;
	};
	y++;
	return f1();
}

IAsyncOperation<int>^ WindowsPhoneRuntimeComponent::AddAdync(int x, int y)
{
	auto f1 = [x, y]()->int
	{
		int z = x + y;
		return z;
	};
	y++;
	return create_async(f1);
}


IAsyncOperationWithProgress<int, double>^ WindowsPhoneRuntimeComponent::AddWithProgressAsync(int x, int y)
{
	return create_async([this, x, y]
		(progress_reporter<double> reporter, cancellation_token cts)->int
	{
		if (x<0 || y<0 || x>y)
		{
			throw ref new InvalidArgumentException();
		}

		int sum = 0;
		for (int n = x; n<y; n++)
		{
			if (cts.is_canceled())
			{
				cancel_current_task();
				return 0;
			}
			sum += n;
			reporter.report(n);
			this->currentValue(n);
		}
		return sum;
	});
}