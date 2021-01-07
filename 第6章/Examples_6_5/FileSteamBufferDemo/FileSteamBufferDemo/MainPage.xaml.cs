using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FileSteamBufferDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private StorageFile sampleFile;
        private string filename = "sampleFile.dat";
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async void bt_create_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            sampleFile = await storageFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            OutputTextBlock.Text = "文件 '" + sampleFile.Name + "' 已经创建好";
        }

        private async void bt_writebuffer_Click(object sender, RoutedEventArgs e)
        {
            StorageFile file = sampleFile;
            if (file != null)
            {
                try
                {
                    string userContent = "测试的文本消息";
                    IBuffer buffer;
                    using (InMemoryRandomAccessStream memoryStream = new InMemoryRandomAccessStream())
                    {
                        using (DataWriter dataWriter = new DataWriter(memoryStream))
                        {
                            dataWriter.WriteInt32(Encoding.UTF8.GetByteCount(userContent));
                            dataWriter.WriteString(userContent);
                            buffer = dataWriter.DetachBuffer();
                        }
                    }
                    await FileIO.WriteBufferAsync(file, buffer);
                    OutputTextBlock.Text = "长度为 " + buffer.Length + " bytes 的文本信息写入到了文件 '" + file.Name + "':" + Environment.NewLine + Environment.NewLine + userContent;
                }
                catch (Exception exce)
                {
                    OutputTextBlock.Text = "异常：" + exce.Message;
                }
            }
            else
            {
                OutputTextBlock.Text = "请先创建文件";
            }
        }

        private async void bt_readbuffer_Click(object sender, RoutedEventArgs e)
        {
            StorageFile file = sampleFile;
            if (file != null)
            {
                try
                {
                    IBuffer buffer = await FileIO.ReadBufferAsync(file);
                    using (DataReader dataReader = DataReader.FromBuffer(buffer))
                    {
                        Int32 stringSize = dataReader.ReadInt32();
                        string fileContent = dataReader.ReadString((uint)stringSize);
                        OutputTextBlock.Text = "长度为 " + buffer.Length + " bytes 的文本信息从文件 '" + file.Name + "'读取出来，其中字符床的长度为" + stringSize + " bytes :"
                            + Environment.NewLine + fileContent;
                    }
                }
                catch (Exception exce)
                {
                    OutputTextBlock.Text = "异常：" + exce.Message;
                }
            }
            else
            {
                OutputTextBlock.Text = "请先创建文件";
            }
        }

        private async void bt_writestream_Click(object sender, RoutedEventArgs e)
        {
            StorageFile file = sampleFile;
            if (file != null)
            {
                try
                {
                    string userContent = "测试的文本消息";
                    using (StorageStreamTransaction transaction = await file.OpenTransactedWriteAsync())
                    {
                        using (DataWriter dataWriter = new DataWriter(transaction.Stream))
                        {
                            dataWriter.WriteInt32(Encoding.UTF8.GetByteCount(userContent));
                            dataWriter.WriteString(userContent);
                            transaction.Stream.Size = await dataWriter.StoreAsync();
                            await transaction.CommitAsync();
                            OutputTextBlock.Text = "使用stream把信息写入了文件 '" + file.Name + "' :" + Environment.NewLine + userContent;
                        }
                    }
                }
                catch (Exception exce)
                {
                    OutputTextBlock.Text = "异常：" + exce.Message;
                }
            }
            else
            {
                OutputTextBlock.Text = "请先创建文件";
            }
        }

        private async void bt_readstream_Click(object sender, RoutedEventArgs e)
        {
            StorageFile file = sampleFile;
            if (file != null)
            {
                try
                {
                    using (IRandomAccessStream readStream = await file.OpenAsync(FileAccessMode.Read))
                    {
                        using (DataReader dataReader = new DataReader(readStream))
                        {
                            UInt64 size = readStream.Size;
                            if (size <= UInt32.MaxValue)
                            {
                                await dataReader.LoadAsync(sizeof(Int32));
                                Int32 stringSize = dataReader.ReadInt32();
                                await dataReader.LoadAsync((UInt32)stringSize);
                                string fileContent = dataReader.ReadString((uint)stringSize);
                                OutputTextBlock.Text = "使用stream把信息从文件 '" + file.Name + "' 读取出来，其中字符床的长度为" + stringSize + " bytes :"
                                    + Environment.NewLine + fileContent;
                            }
                            else
                            {
                                OutputTextBlock.Text = "文件 " + file.Name + " 太大，不能再单个数据块中读取";
                            }
                        }
                    }
                }
                catch (Exception exce)
                {
                    OutputTextBlock.Text = "异常：" + exce.Message;
                }
            }
            else
            {
                OutputTextBlock.Text = "请先创建文件";
            }
        }
    }
}
