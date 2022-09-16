namespace Hakan.FormsApp.Youtubemp3Player
{
    public partial class Form1 : Form
    {
        private Mp3Player player = new Mp3Player();

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            player.stop();
        }

        private void AddUrl_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Mp3 Files|*.mp3";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    player.open(ofd.FileName);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            player.play();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}