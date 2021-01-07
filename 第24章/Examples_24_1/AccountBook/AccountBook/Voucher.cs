using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook
{
    public class Voucher
    {
        //金额
        public double Money { get; set; }
        //账单类型 0表示收入 1表示支出
        public short Type { get; set; }
        //说明
        public string Desc { get; set; }
        //时间
        public DateTime DT { get; set; }
        //唯一id
        public Guid ID { get; set; }
        //图片
        public byte[] Picture { get; set; }
        //图片高度
        public int PictureHeight { get; set; }
        //图片宽度
        public int PictureWidth { get; set; }
        //类别
        public string Category { get; set; }
    }
}
