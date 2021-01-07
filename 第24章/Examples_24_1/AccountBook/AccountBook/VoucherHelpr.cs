using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook
{
    public class VoucherHelpr
    {
        //记账列表
        private List<Voucher> _data;

        // 添加一条记账记录
        public async void AddNew(Voucher item)
        {
            await Getdata();
            item.ID = Guid.NewGuid();
            this._data.Add(item);
        }
        // 读取记账列表
        public async Task<bool> LoadFromFile()
        {
            this._data = await StorageFileHelper.ReadAsync<List<Voucher>>("Voucher.dat");
            return (this._data != null);
        }
        // 保存记账列表
        public async void SaveToFile()
        {
            await StorageFileHelper.WriteAsync<List<Voucher>>(this._data, "Voucher.dat");
        }
        // 获取记账列表
        public async Task<List<Voucher>> Getdata()
        {
            if (this._data == null)
            {
                bool isExist = await LoadFromFile();
                if (!isExist)
                {
                    this._data = new List<Voucher>();
                }
            }
            return this._data;
        }
        // 移除一条记录
        public void Remove(Voucher item)
        {
            _data.Remove(item);
        }
    }
}
