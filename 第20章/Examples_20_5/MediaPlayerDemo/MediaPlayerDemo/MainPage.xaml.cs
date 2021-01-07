using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaPlayerDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool _updatingMediaTimeline;
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (mediaPlayer.CanPause)
            {
                mediaPlayer.Pause();
                lblStatus.Text = "暂停";
            }
            else
            {
                lblStatus.Text = "不能暂停，请重试!";
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
            mediaPlayer.Position = System.TimeSpan.FromSeconds(0);
            lblStatus.Text = "停止";
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
        }

        private void btnMute_Click(object sender, RoutedEventArgs e)
        {
            lblSoundStatus.Text = "声音关";
            mediaPlayer.IsMuted = true;
        }

        private void btnVolume_Click(object sender, RoutedEventArgs e)
        {
            lblSoundStatus.Text = "声音开";
            mediaPlayer.IsMuted = false;
        }

        private void mediaTimeline_ValueChanged_1(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (!_updatingMediaTimeline && mediaPlayer.CanSeek)
            {
                TimeSpan duration = mediaPlayer.NaturalDuration.TimeSpan;
                int newPosition = (int)(duration.TotalSeconds * mediaTimeline.Value);
                mediaPlayer.Position = new TimeSpan(0, 0, newPosition);
            }
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            _updatingMediaTimeline = false;
            mediaPlayer.Source = new Uri(txtUrl.Text);
            mediaPlayer.Position = System.TimeSpan.FromSeconds(0);
            mediaPlayer.DownloadProgressChanged += (s, ee) =>
            {
                lblDownload.Text = string.Format("下载 {0:0.0%}", mediaPlayer.DownloadProgress);
            };
            mediaPlayer.BufferingProgressChanged += (s, ee) =>
            {
                lblBuffering.Text = string.Format("缓冲 {0:0.0%}", mediaPlayer.BufferingProgress);
            };
            CompositionTarget.Rendering += (s, ee) =>
            {
                _updatingMediaTimeline = true;
                TimeSpan duration = mediaPlayer.NaturalDuration.TimeSpan;
                if (duration.TotalSeconds != 0)
                {
                    double percentComplete = mediaPlayer.Position.TotalSeconds / duration.TotalSeconds;
                    mediaTimeline.Value = percentComplete;
                    TimeSpan mediaTime = mediaPlayer.Position;
                    string text = string.Format("{0:00}:{1:00}",
                        (mediaTime.Hours * 60) + mediaTime.Minutes, mediaTime.Seconds);
                    if (lblStatus.Text != text)
                        lblStatus.Text = text;

                    _updatingMediaTimeline = false;
                }
            };
            mediaPlayer.Play();
        }

    }
}
