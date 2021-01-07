using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook
{
    /// <summary>
    /// 类别实体类
    /// </summary>
    public class Category
    {
        //账单类型 0表示收入 1表示支出
        public short Type { get; set; }
        //类别名称
        public string Name { get; set; }

    }
}
