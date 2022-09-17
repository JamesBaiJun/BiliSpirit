using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unosquare.FFME;

namespace BiliSpirit.ViewModels
{
    [POCOViewModel]
    public class PlayerViewModel
    {
        public PlayerViewModel()
        {

        }
        public virtual TimeSpan? CurrentTime { get; set; }
        public virtual TimeSpan? FileDuration { get; set; }
        public virtual double CurrentProgress { get; set; }
        public MediaElement MediaPlayer { get; set; }

        public void Loaded(MediaElement media)
        {
            MediaPlayer = media;
            MediaPlayer.RenderingVideo += MediaPlayer_RenderingVideo;
        }

        private void MediaPlayer_RenderingVideo(object? sender, Unosquare.FFME.Common.RenderingVideoEventArgs e)
        {
            //刷新当前时间
            CurrentTime = MediaPlayer.ActualPosition;
            CurrentProgress = (double)(MediaPlayer.ActualPosition?.TotalSeconds / FileDuration?.TotalSeconds);
        }
    }
}
