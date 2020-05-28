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
using AudioPlayerLib;
using System.IO;

namespace Audio_Player
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.OpenFileDialog browseSongsDialog;
        private MusicPlayer ourMusicPlayer = null;
        private int currentPlayingIndex = -1;
        public Form1()
        {
            InitializeComponent();


            ourMusicPlayer = new MusicPlayer(PlayModeHandler, 
                PausedPlayerHandler, 
                StartedPlayingHandler, 
                StoppedPlayerHandler, 
                timerVisUpdate, progBarSong, picBoxVisualizations, listBoxSongs,
                UpdateVisualizeHandler);
            disableAllControls();
            
        }

        

        private void btnPause_Click(object sender, EventArgs e)
        {
            ourMusicPlayer.Pause();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (currentPlayingIndex != listBoxSongs.SelectedIndex)
                ourMusicPlayer.Stop();
            
            ourMusicPlayer.SelectSong(listBoxSongs.SelectedIndex);
            ourMusicPlayer.Play();
            lblCurrentlyPlaying.Text = "Currently playing" + listBoxSongs.SelectedItem.ToString();
            currentPlayingIndex = listBoxSongs.SelectedIndex;
        }
        

        private void PlayModeHandler(object sender, PlayModeEventArgs e)
        {

        }

        private void PausedPlayerHandler(object sender, PausedPlayerEventArgs e)
        {

        }

        private void StartedPlayingHandler(object sender, StartedPlayerEventArgs e)
        {

        }

        private void StoppedPlayerHandler(object sender, StoppedPlayerEventArgs e)
        {

        }

        private void UpdateVisualizeHandler(object sender, EventArgs e)
        {
            
        }

        private void btnAddPlaylist_Click(object sender, EventArgs e)
        {

            using (var songDirBrowser = new FolderBrowserDialog())
            {
                DialogResult result = songDirBrowser.ShowDialog();
                int numLoaded = 0; 
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(songDirBrowser.SelectedPath))
                {
                    numLoaded = ourMusicPlayer.LoadNewPlaylist(songDirBrowser.SelectedPath);
                    System.Windows.Forms.MessageBox.Show("Loaded " + numLoaded + " songs.", "Message");
                    enableAllControls();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Could not load songs -  invalid path" + numLoaded + " songs.", "Error");
                }
            }
           
        }

        private void btnAddSongs_Click(object sender, EventArgs e)
        {
            using (var songDirBrowser = new FolderBrowserDialog())
            {
                DialogResult result = songDirBrowser.ShowDialog();
                int numLoaded = 0;
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(songDirBrowser.SelectedPath))
                {
                    numLoaded = ourMusicPlayer.AddToPlaylist(songDirBrowser.SelectedPath);
                    System.Windows.Forms.MessageBox.Show("Added " + numLoaded + " songs.", "Message");
                    enableAllControls();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Could not add songs -  invalid path" + numLoaded + " songs.", "Error");
                }
            }
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if(listBoxSongs.SelectedIndex < listBoxSongs.Items.Count)
                ourMusicPlayer.RemoveFromPlaylist(listBoxSongs.SelectedIndex);

            if(listBoxSongs.Items.Count == 0)
            {
                disableAllControls();
            }
        }

        private void disableAllControls()
        {
            btnPlay.Enabled = false;
            btnPause.Enabled = false;
            btnNext.Enabled = false;
            btnPrevSong.Enabled = false;
            cbAutoplay.Checked = false;
            cbAutoplay.Enabled = false;
            cbShuffle.Checked = false;
            cbShuffle.Enabled = false;
        }

        private void enableAllControls()
        {
            btnPlay.Enabled = true;
            btnPause.Enabled = true;
            btnNext.Enabled = true;
            btnPrevSong.Enabled = true;
            cbAutoplay.Checked = false;
            cbAutoplay.Enabled = true;
            cbShuffle.Checked = false;
            cbShuffle.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ourMusicPlayer.Stop();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            string message = "Music Player projected developed as part of the \nProgramming Engineering course \nat the Computer Engineering Faculty of TUIasi.\n"
                +"Developed by:\n"
                +"Cana Andrei\n"
                +"Popovici Cosmin\n"
                +"Vacariuc Bogdan\n"
                +"Teodorovici Silviu\n"
                +@"https://github.com/Aythriel/IP_Proiect_Audio_Player";
            System.Windows.Forms.MessageBox.Show(message, "About");
        }
    }
}
