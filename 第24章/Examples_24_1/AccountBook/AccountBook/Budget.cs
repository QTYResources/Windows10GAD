using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook
{
    /// <summary>
    /// 预算实体类
    /// </summary>
    public class Budget
    {
        //预算名称
        public string ItemName { get; set; }
        //预算金额大小
        public int Limit { get; set; }
    }
}
