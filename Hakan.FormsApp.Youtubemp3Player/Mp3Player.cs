using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hakan.FormsApp.Youtubemp3Player
{
    public class Mp3Player : IDisposable
    {
        public bool Repeat { get; set; }
        public static bool IsPlaying { get; set; }
        public static int Timer { get; set; }

        public Mp3Player(string FileName)
        {
            string Format = @"open ""{0}"" type MPEGVideo alias MediaFile";
            string command = string.Format(Format, FileName);
            mciSendString(command, null, 0, 0);
        }

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrRetyrbString, int uReturnLength, int hwdCallBack);

        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        public void play()
        {
            IsPlaying = true;
            string command = "play MediaFile";
            if (Repeat) command += " REPEAT";
            mciSendString(command, null, 0, 0);
        }

        public void stop()
        {
            IsPlaying = false;
            string command = "stop MediaFile";
            mciSendString(command, null, 0, 0);
        }

        public int length()
        {
            StringBuilder sb = new StringBuilder(128);
            mciSendString("status mediafile length", sb, 128, 0);
            return Convert.ToInt32(sb.ToString());
        }

        public void Dispose()
        {
            string command = "close MediaFile";
            mciSendString(command, null, 0, 0);
        }


    }
}
