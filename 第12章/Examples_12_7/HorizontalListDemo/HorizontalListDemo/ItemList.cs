using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace HorizontalListDemo
{
    public class ItemList : IList
    {

        public ItemList()
        {
            Count = 100;
        }

        public int Count { get; set; }

        public object this[int index]
        {
            get
            {
                int imageIndex = 5 - index % 5;
                Debug.WriteLine("加载的集合索引是：" + index);
                return new Item { ImageName = "图片" + index, Image = new BitmapImage(new Uri("ms-appx:///Images/" + imageIndex + ".jpg", UriKind.RelativeOrAbsolute)) };
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #region IList Members

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class Item
    {
        public BitmapImage Image { get; set; }
        public string ImageName { get; set; }
    }
}
