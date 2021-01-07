using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingWallpapers
{
    // 下载进度参数类
    public class ProgressEventArgs : EventArgs
    {
        // 进度的百分比值
        public int ProgressValue { get; set; }
        // 是否完成了所有图片的下载
        public bool Complete { get; set; }
        // 是否发生异常
        public bool IsException { get; set; }
        // 异常消息
        public string ExceptionInfo { get; set; }
        // 下载的图片列表信息，未完成时为null
        public List<PictureInfo> Pictures { get; set; }
    }
}
