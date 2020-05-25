using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Audio_Player
{
    public partial class Form1 : Form
    {

        private System.Drawing.Bitmap _currentSongImage=null;
        private EventHandler _visualizerUpdateEvent;
        private EventHandler _pauseEvent;
        private EventHandler _startEvent;

        private AudioPlayerLib.AudioVisualizer audioVisualizer = null;
        public Form1()
        {
            InitializeComponent();

            audioVisualizer = new AudioPlayerLib.AudioVisualizer(pBar1, pictureBox1, timer1);
            audioVisualizer.SetSong(new AudioPlayerLib.AudioFile("D:\\test2.mp3"));
            _visualizerUpdateEvent += audioVisualizer.VisualUpdateEvent;
            _pauseEvent += audioVisualizer.OnPauseEvent;
            _startEvent += audioVisualizer.OnStartEvent;
        }

        
        private void timer1_Tick(object sender, EventArgs e) // every 100ms
        {
            _visualizerUpdateEvent(this, e);
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            _pauseEvent(this, e);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            _startEvent(this, e);
        }
    }
}
