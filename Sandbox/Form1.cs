using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyAudioPlayerNAudio
{
    public partial class Form1 : Form
    {
        int cnt;
        AudioFile currentAudioFile;
        AudioPlayer audioPlayer;
        public Form1()
        {
            InitializeComponent();
            listBox_playlist.Items.AddRange(Directory.GetFiles("Songs"));
            cnt = 0;
            currentAudioFile = new AudioFile(listBox_playlist.Items[cnt++].ToString());
            audioPlayer = new AudioPlayer
                (
                    StartingPlayingEventHandler, 
                    PausedPlayerEventHandler, 
                    StoppedPlayerEventHandler,
                    VisualizationChangedHandler
                );
        }

        private void button_Play_Click(object sender, EventArgs e)
        {
            audioPlayer.PlaySong(currentAudioFile);
        }

        private void button_Pause_Click(object sender, EventArgs e)
        {
            audioPlayer.PauseSong();
        }

        private void button_Stop_Click(object sender, EventArgs e)
        {
            audioPlayer.StopSong();
        }

        private void StartingPlayingEventHandler(object sender, StartedPlayerEventArgs e)
        {
            button_Play.Enabled = false;
            button_Pause.Enabled = true;
            button_Stop.Enabled = true;
        }

        private void PausedPlayerEventHandler(object sender, PausedPlayerEventArgs e)
        {
            button_Play.Enabled = true;
            button_Pause.Enabled = false;
            button_Stop.Enabled = true;
        }

        private void StoppedPlayerEventHandler(object sender, StoppedPlayerEventArgs e)
        {
            if (e.type.Equals("EOF"))
            {
                currentAudioFile = new AudioFile(listBox_playlist.Items[cnt++].ToString());
                audioPlayer.PlaySong(currentAudioFile);
            }
            else if (e.type.Equals("USR"))
            {
                button_Play.Enabled = true;
                button_Pause.Enabled = false;
                button_Stop.Enabled = false;
            }
        }

        private void VisualizationChangedHandler(object sender, VisualizationEventArgs e)
        {
            if (!label_nowPlayingSong.Text.Equals(e.name))
                label_nowPlayingSong.Text = e.name;
            if (progressBar1.Maximum != e.totalTime)
                progressBar1.Maximum = e.totalTime;
            if (progressBar1.Value != e.currentTime)
                progressBar1.Value = e.currentTime;
        }
    }
}
