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

        private void log(string msg) => textBox_Log.Text += msg + "\r\n";

        public Form1()
        {
            InitializeComponent();
            listBox_playlist.Items.AddRange(Directory.GetFiles("Songs"));
            cnt = 0;
            listBox_playlist.SelectedIndex = cnt;
            currentAudioFile = new AudioFile(listBox_playlist.Items[cnt].ToString());
            SetButtonsInitialStates();
            audioPlayer = new AudioPlayer
                (
                    StartingPlayingEventHandler, 
                    PausedPlayerEventHandler, 
                    StoppedPlayerEventHandler,
                    VisualizationChangedHandler
                );
        }

        private void SetButtonsInitialStates()
        {
            button_Play.Enabled = true;
            button_Pause.Enabled = false;
            button_Stop.Enabled = false;
            button_Next.Enabled = false;
            button_Previous.Enabled = false;
        }

        private void button_Play_Click(object sender, EventArgs e)
        {
            audioPlayer.PlaySong(currentAudioFile);
        }

        void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

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
            button_Next.Enabled = true;
            button_Previous.Enabled = true;
        }

        private void PausedPlayerEventHandler(object sender, PausedPlayerEventArgs e)
        {
            button_Play.Enabled = true;
            button_Pause.Enabled = false;
            button_Stop.Enabled = true;
            button_Next.Enabled = false;
            button_Previous.Enabled = false;
        }

        private void StoppedPlayerEventHandler(object sender, StoppedPlayerEventArgs e)
        {
            if (e.type.Equals("EOF"))
            {
                log("Eof Stopped Occured");
                if (e.nextSongType.Equals("NEXT") && cnt < listBox_playlist.Items.Count - 1)
                {
                    ++cnt;
                    currentAudioFile = new AudioFile(listBox_playlist.Items[cnt].ToString());
                    audioPlayer.PlaySong(currentAudioFile);
                    listBox_playlist.SelectedIndex = cnt;
                }
                else if (e.nextSongType.Equals("PREV") && cnt > 0)
                {
                    --cnt;
                    currentAudioFile = new AudioFile(listBox_playlist.Items[cnt].ToString());
                    audioPlayer.PlaySong(currentAudioFile);
                    listBox_playlist.SelectedIndex = cnt;
                }
                else
                {
                    cnt = 0;
                    listBox_playlist.SelectedIndex = cnt;
                    SetButtonsInitialStates();
                    currentAudioFile = new AudioFile(listBox_playlist.Items[cnt].ToString());
                    audioPlayer.PlaySong(currentAudioFile);
                }
                
            }
            else if (e.type.Equals("USR"))
            {
                log("Usr Stopped Occured");
                button_Play.Enabled = true;
                button_Pause.Enabled = false;
                button_Stop.Enabled = false;
                button_Next.Enabled = false;
                button_Previous.Enabled =false;
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

        private void button_Previous_Click(object sender, EventArgs e)
        {
            audioPlayer.PlayPreviousSong();
        }

        private void button_Next_Click(object sender, EventArgs e)
        {
            audioPlayer.PlayNextSong();
        }

        private void listBox_playlist_Click(object sender, EventArgs e)
        {
            if (audioPlayer._audioPlayerState == AudioPlayer.AudioPlayerState.Stopped)
            {
                int idx = listBox_playlist.SelectedIndex;
                if (idx != -1)
                {
                    cnt = idx;
                    listBox_playlist.SelectedIndex = cnt;
                    currentAudioFile = new AudioFile(listBox_playlist.Items[cnt].ToString());
                    audioPlayer.PlaySong(currentAudioFile);
                }
            }
            
        }
    }
}
