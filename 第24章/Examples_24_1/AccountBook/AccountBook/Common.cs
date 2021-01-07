using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook
{
    public class Common
    {
        /// <summary>
        /// 获取预算的金额
        /// </summary>
        /// <param name="ItemName">类别名称</param>
        /// <returns></returns>
        public static async Task<double> GetLimitOf(string ItemName)
        {
            IEnumerable<Budget> source = from c in await App.budgetHelper.Getdata()
                                         where c.ItemName == ItemName
                                         select c;
            if (source.Count<Budget>() > 0)
            {
                return (double)source.FirstOrDefault<Budget>().Limit;
            }
            return -1.0;
        }
        /// <summary>
        /// 获取所有的记账记录
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<Voucher>> GetAllRecords()
        {
            return (from c in await App.voucherHelper.Getdata() select c);
        }
        /// <summary>
        /// 获取某月的所有记录
        /// </summary>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns>记账记录枚举集合</returns>
        public static async Task<IEnumerable<Voucher>> GetThisMonthAllRecords(int month, int year)
        {
            return (from c in await App.voucherHelper.Getdata()
                    where (c.DT.Month == month) && (c.DT.Year == year)
                    select c);
        }
        /// <summary>
        /// 获取某日的所有记录
        /// </summary>
        /// <param name="day">日</param>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns>记账记录枚举集合</returns>
        public static async Task<IEnumerable<Voucher>> GetThisDayAllRecords(int day, int month, int year)
        {
            return (from c in await App.voucherHelper.Getdata()
                    where (c.DT.Day == day) && (c.DT.Month == month) && (c.DT.Year == year)
                    select c);
        }
        /// <summary>
        /// 获取本年的所有记录
        /// </summary>
        /// <param name="year">年</param>
        /// <returns>记账记录枚举集合</returns>
        public static async Task<IEnumerable<Voucher>> GetThisYearAllRecords(int year)
        {
            return (from c in await App.voucherHelper.Getdata()
                    where c.DT.Year == year
                    select c);
        }
        /// <summary>
        /// 获取本月的类别总金额
        /// </summary>
        /// <param name="ItemName">类别名称</param>
        /// <param name="type">类别类型</param>
        /// <returns>金额</returns>
        public static  async Task<double> GetThisMonthSummaryOf(string ItemName, short type)
        {
            return ((IEnumerable<double>)(from c in await App.voucherHelper.Getdata()
                                          where ((c.DT.Month == DateTime.Now.Month) && (c.DT.Year == DateTime.Now.Year)) && (c.Type == type) && (c.Category == ItemName)
                                          select c.Money)).Sum();
        }
        /// <summary>
        ///  获取本月的类别总金额
        /// </summary>
        /// <param name="category">类别实体</param>
        /// <returns>金额</returns>
        public static  async Task<double> GetThisMonthSummaryOf(Category category)
        {
            return ((IEnumerable<double>)(from c in await App.voucherHelper.Getdata()
                                          where ((c.DT.Month == DateTime.Now.Month) && (c.DT.Year == DateTime.Now.Year)) && (c.Type == category.Type) && (c.Category == category.Name)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取总支出
        /// </summary>
        /// <returns>金额</returns>
        public static async Task<double> GetSummaryExpenses()
        {
            return ((IEnumerable<double>)(from c in await App.voucherHelper.Getdata()
                                          where c.Type == 1
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取本年的支出
        /// </summary>
        /// <returns>金额</returns>
        public static async Task<double> GetThisYearSummaryExpenses()
        {
            return ((IEnumerable<double>)(from c in await App.voucherHelper.Getdata()
                                          where ((c.DT.Year == DateTime.Now.Year)) && (c.Type == 1)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取某年的支出
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static async Task<double> GetYearSummaryExpenses(int year)
        {
            return ((IEnumerable<double>)(from c in await App.voucherHelper.Getdata()
                                          where ((c.DT.Year == year)) && (c.Type == 1)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取本月的支出
        /// </summary>
        /// <returns></returns>
        public static async Task<double> GetThisMouthSummaryExpenses()
        {
            return ((IEnumerable<double>)(from c in await App.voucherHelper.Getdata()
                                          where ((c.DT.Year == DateTime.Now.Year)) && ((c.DT.Month == DateTime.Now.Month)) && (c.Type == 1)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取某月的支出
        /// </summary>
        /// <param name="mouth">月份</param>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static async Task<double> GetMouthSummaryExpenses(int mouth, int year)
        {
            return ((IEnumerable<double>)(from c in await App.voucherHelper.Getdata()
                                          where ((c.DT.Year == year)) && ((c.DT.Month == mouth)) && (c.Type == 1)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取总收入
        /// </summary>
        /// <returns>金额</returns>
        public static async Task<double> GetSummaryIncome()
        {
            return ((IEnumerable<double>)(from c in await App.voucherHelper.Getdata()
                                          where c.Type == 0
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取本年的收入
        /// </summary>
        /// <returns>金额</returns>
        public static async Task<double> GetThisYearSummaryIncome()
        {
            return ((IEnumerable<double>)(from c in await App.voucherHelper.Getdata()
                                          where ((c.DT.Year == DateTime.Now.Year)) && (c.Type == 0)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取某年的收入
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static async Task<double> GetYearSummaryIncome(int year)
        {
            return ((IEnumerable<double>)(from c in await App.voucherHelper.Getdata()
                                          where ((c.DT.Year == year)) && (c.Type == 0)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取本月的收入
        /// </summary>
        /// <returns>金额</returns>
        public static  async Task<double> GetThisMouthSummaryIncome()
        {
            return ((IEnumerable<double>)(from c in await App.voucherHelper.Getdata()
                                          where ((c.DT.Year == DateTime.Now.Year)) && ((c.DT.Month == DateTime.Now.Month)) && (c.Type == 0)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 获取某月的收入
        /// </summary>
        /// <param name="mouth">月份</param>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static async Task<double> GetMouthSummaryIncome(int mouth, int year)
        {
            return ((IEnumerable<double>)(from c in await App.voucherHelper.Getdata()
                                          where ((c.DT.Year == year)) && ((c.DT.Month == mouth)) && (c.Type == 0)
                                          select c.Money)).Sum();
        }
        /// <summary>
        /// 查询记账记录
        /// </summary>
        /// <param name="begin">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <param name="keyWords">关键字</param>
        /// <returns>记账记录</returns>
        public static async Task<IEnumerable<Voucher>> Search(DateTime? begin, DateTime? end, string keyWords)
        {
            if (keyWords == "")
            {
                return (from c in await App.voucherHelper.Getdata()
                        where c.DT >= begin && c.DT <= end
                        select c);
            }
            else
            {
                return (from c in await App.voucherHelper.Getdata()
                        where c.DT >= begin && c.DT <= end && c.Desc.IndexOf(keyWords) >= 0
                        select c);
            }
        }
    }
}
