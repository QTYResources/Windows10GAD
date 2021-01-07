using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBook
{
    public class FinancialNoteHelper
    {
        private List<FinancialNote> _data;

        public async Task<bool> LoadFromFile()
        {
            this._data = await StorageFileHelper.ReadAsync<List<FinancialNote>>("FinancialNote.dat");
            return (this._data != null);
        }

        public async void SaveToFile()
        {
            await StorageFileHelper.WriteAsync<List<FinancialNote>>(this._data, "FinancialNote.dat");
        }

        public async Task<List<FinancialNote>> Getdata()
        {
            if (this._data == null)
            {
                bool isExist = await LoadFromFile();
                if (!isExist)
                {
                    this._data = new List<FinancialNote>();
                }
            }
            return this._data;
        }
    }
}
