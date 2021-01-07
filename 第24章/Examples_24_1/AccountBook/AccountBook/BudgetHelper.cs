using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook
{
    /// <summary>
    /// 预算操作帮助类
    /// </summary>
    public class BudgetHelper
    {
        private List<Budget> _data;

        public async Task<bool> LoadFromFile()
        {
            this._data = await StorageFileHelper.ReadAsync<List<Budget>>("Budget.dat");
            return (this._data != null);
        }

        public async void SaveToFile()
        {
            await StorageFileHelper.WriteAsync<List<Budget>>(this._data, "Budget.dat");
        }

        public async Task<List<Budget>> Getdata()
        {
            if (this._data == null)
            {
               bool isExist = await LoadFromFile();
                if(!isExist)
                {
                    this._data = new List<Budget>();
                }
            }
            return this._data;
        }
    }
}
