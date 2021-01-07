using System;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace ConverterDemo
{
    public class Clock : INotifyPropertyChanged
    {
        int hour, min, sec;

        public event PropertyChangedEventHandler PropertyChanged;

        public Clock()
        {
            OnTimerTick(null, null);

            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromSeconds(0.1);
            tmr.Tick += OnTimerTick;
            tmr.Start();
        }

        public int Hour
        {
            protected set
            {
                if (value != hour)
                {
                    hour = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Hour"));
                }
            }
            get
            {
                return hour;
            }
        }

        public int Minute
        {
            protected set
            {
                if (value != min)
                {
                    min = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Minute"));
                }
            }
            get
            {
                return min;
            }
        }

        public int Second
        {
            protected set
            {
                if (value != sec)
                {
                    sec = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Second"));
                }
            }
            get
            {
                return sec;
            }
        }


        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, args);
        }

        void OnTimerTick(object sender, object args)
        {
            DateTime dt = DateTime.Now;
            Hour = dt.Hour;
            Minute = dt.Minute;
            Second = dt.Second;
        }
    }
}
