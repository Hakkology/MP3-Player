namespace Hakan.FormsApp.Youtubemp3Player
{
    public partial class Form1 : Form
    {
        private Mp3Player player;

        public Form1()
        {
            InitializeComponent();
            uint CurrVol = 0;
            Mp3Player.waveOutGetVolume(IntPtr.Zero, out CurrVol);
            ushort CalcVol = (ushort)(CurrVol & 0x0000ffff);
            trackBar1.Value = CalcVol / (ushort.MaxValue / 10);
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (player != null)
                player.stop();
        }

        private void AddUrl_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Mp3 Files|*.mp3";
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    player = new Mp3Player(ofd.FileName);
                    player.Repeat = true;
                    textBox1.Text = ofd.FileName;
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (player != null)
                player.play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int NewVolume = ((ushort.MaxValue / 10) * trackBar1.Value);
            uint NewVolumeAllChannels = ((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16);
            Mp3Player.waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            Mp3Player.Timer++;
            if (player != null)
                if (Mp3Player.IsPlaying == true)
                {
                    timer1.Start();
                }
                else
                {
                    timer1.Stop();
                }
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {
            progressBar2.Maximum = player.length();
            progressBar2.Value = Mp3Player.Timer;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}