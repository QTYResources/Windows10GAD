using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Windows.Networking.BackgroundTransfer;

namespace TransferDownloadDemo
{
    public class TransferModel : INotifyPropertyChanged
    {
        public DownloadOperation DownloadOperation { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }

        private int _progress;
        public int Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
                RaisePropertyChanged("Progress");
            }
        }
        private ulong _totalBytesToReceive;
        public ulong TotalBytesToReceive
        {
            get
            {
                return _totalBytesToReceive;
            }
            set
            {
                _totalBytesToReceive = value;
                RaisePropertyChanged("TotalBytesToReceive");
            }
        }

        private ulong _bytesReceived;
        public ulong BytesReceived
        {
            get
            {
                return _bytesReceived;
            }
            set
            {
                _bytesReceived = value;
                RaisePropertyChanged("BytesReceived");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}