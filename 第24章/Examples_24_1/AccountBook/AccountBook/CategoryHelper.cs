using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook
{
    /// <summary>
    /// 类别操作帮助类
    /// </summary>
    public class CategoryHelper
    {
        private List<Category> _data;

        public async Task<bool> LoadFromFile()
        {
            this._data = await StorageFileHelper.ReadAsync<List<Category>>("Category.dat");
            return (this._data != null);
        }

        public async void SaveToFile()
        {
            await StorageFileHelper.WriteAsync<List<Category>>(this._data, "Category.dat");
        }

        public async Task<List<Category>> Getdata()
        {
            if (this._data == null)
            {
                bool isExist = await LoadFromFile();
                if (!isExist)
                {
                    this._data = new List<Category>();
                }
            }
            return this._data;
        }
    }
}
