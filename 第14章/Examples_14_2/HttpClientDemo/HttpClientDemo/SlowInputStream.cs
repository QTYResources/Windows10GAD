using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace HttpClientDemo
{
    public class SlowInputStream : IInputStream
    {
        uint length;
        uint position;

        public SlowInputStream(uint length)
        {
            this.length = length;
            position = 0;
        }

        public IAsyncOperationWithProgress<IBuffer, uint> ReadAsync(IBuffer buffer, uint count, InputStreamOptions options)
        {
            return AsyncInfo.Run<IBuffer, uint>(async (cancellationToken, progress) =>
            {
                if (length - position < count)
                {
                    count = length - position;
                }

                byte[] data = new byte[count];
                for (uint i = 0; i < count; i++)
                {
                    data[i] = 64;
                }

                // 延迟一秒钟
                await Task.Delay(1000);

                position += count;
                progress.Report(count);

                return data.AsBuffer();
            });
        }

        public void Dispose()
        {
        }
    }
}
