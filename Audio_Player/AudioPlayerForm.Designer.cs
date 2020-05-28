namespace Audio_Player
{
    partial class AudioPlayerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnAddSongs = new System.Windows.Forms.Button();
            this.listBoxSongs = new System.Windows.Forms.ListBox();
            this.gbConsole = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.cbShuffle = new System.Windows.Forms.CheckBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPrevSong = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.lblCurrentlyPlaying = new System.Windows.Forms.Label();
            this.btnAddPlaylist = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.timerVisUpdate = new System.Windows.Forms.Timer(this.components);
            this.progBarSong = new System.Windows.Forms.ProgressBar();
            this.picBoxVisualizations = new System.Windows.Forms.PictureBox();
            this.gbConsole.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxVisualizations)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddSongs
            // 
            this.btnAddSongs.Location = new System.Drawing.Point(707, 546);
            this.btnAddSongs.Name = "btnAddSongs";
            this.btnAddSongs.Size = new System.Drawing.Size(75, 37);
            this.btnAddSongs.TabIndex = 0;
            this.btnAddSongs.Text = "Add Songs";
            this.btnAddSongs.UseVisualStyleBackColor = true;
            this.btnAddSongs.Click += new System.EventHandler(this.btnAddSongs_Click);
            // 
            // listBoxSongs
            // 
            this.listBoxSongs.FormattingEnabled = true;
            this.listBoxSongs.Location = new System.Drawing.Point(608, 29);
            this.listBoxSongs.Name = "listBoxSongs";
            this.listBoxSongs.ScrollAlwaysVisible = true;
            this.listBoxSongs.Size = new System.Drawing.Size(292, 511);
            this.listBoxSongs.TabIndex = 2;
            // 
            // gbConsole
            // 
            this.gbConsole.Controls.Add(this.btnStop);
            this.gbConsole.Controls.Add(this.btnPause);
            this.gbConsole.Controls.Add(this.cbShuffle);
            this.gbConsole.Controls.Add(this.btnNext);
            this.gbConsole.Controls.Add(this.btnPlay);
            this.gbConsole.Controls.Add(this.btnPrevSong);
            this.gbConsole.Location = new System.Drawing.Point(26, 480);
            this.gbConsole.Name = "gbConsole";
            this.gbConsole.Size = new System.Drawing.Size(550, 103);
            this.gbConsole.TabIndex = 3;
            this.gbConsole.TabStop = false;
            this.gbConsole.Text = "Console Groupbox";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(106, 20);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(106, 78);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 4;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // cbShuffle
            // 
            this.cbShuffle.AutoSize = true;
            this.cbShuffle.Location = new System.Drawing.Point(318, 24);
            this.cbShuffle.Name = "cbShuffle";
            this.cbShuffle.Size = new System.Drawing.Size(59, 17);
            this.cbShuffle.TabIndex = 3;
            this.cbShuffle.Text = "Shuffle";
            this.cbShuffle.UseVisualStyleBackColor = true;
            this.cbShuffle.CheckedChanged += new System.EventHandler(this.cbShuffle_CheckedChanged);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(187, 49);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(106, 49);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPrevSong
            // 
            this.btnPrevSong.Location = new System.Drawing.Point(25, 49);
            this.btnPrevSong.Name = "btnPrevSong";
            this.btnPrevSong.Size = new System.Drawing.Size(75, 23);
            this.btnPrevSong.TabIndex = 0;
            this.btnPrevSong.Text = "Previous ";
            this.btnPrevSong.UseVisualStyleBackColor = true;
            this.btnPrevSong.Click += new System.EventHandler(this.btnPrevSong_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(143, 12);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(104, 23);
            this.btnAbout.TabIndex = 6;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(26, 12);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(91, 23);
            this.btnHelp.TabIndex = 7;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // lblCurrentlyPlaying
            // 
            this.lblCurrentlyPlaying.AutoSize = true;
            this.lblCurrentlyPlaying.Location = new System.Drawing.Point(23, 393);
            this.lblCurrentlyPlaying.Name = "lblCurrentlyPlaying";
            this.lblCurrentlyPlaying.Size = new System.Drawing.Size(116, 13);
            this.lblCurrentlyPlaying.TabIndex = 8;
            this.lblCurrentlyPlaying.Text = "Label: Currently playing";
            // 
            // btnAddPlaylist
            // 
            this.btnAddPlaylist.Location = new System.Drawing.Point(608, 546);
            this.btnAddPlaylist.Name = "btnAddPlaylist";
            this.btnAddPlaylist.Size = new System.Drawing.Size(93, 37);
            this.btnAddPlaylist.TabIndex = 10;
            this.btnAddPlaylist.Text = "New Playlist";
            this.btnAddPlaylist.UseVisualStyleBackColor = true;
            this.btnAddPlaylist.Click += new System.EventHandler(this.btnAddPlaylist_Click);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(788, 547);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(99, 36);
            this.btnRemoveSelected.TabIndex = 11;
            this.btnRemoveSelected.Text = "Remove Selected";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // progBarSong
            // 
            this.progBarSong.Location = new System.Drawing.Point(26, 409);
            this.progBarSong.Name = "progBarSong";
            this.progBarSong.Size = new System.Drawing.Size(560, 48);
            this.progBarSong.TabIndex = 15;
            // 
            // picBoxVisualizations
            // 
            this.picBoxVisualizations.Location = new System.Drawing.Point(26, 51);
            this.picBoxVisualizations.Name = "picBoxVisualizations";
            this.picBoxVisualizations.Size = new System.Drawing.Size(560, 299);
            this.picBoxVisualizations.TabIndex = 16;
            this.picBoxVisualizations.TabStop = false;
            // 
            // AudioPlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 599);
            this.Controls.Add(this.picBoxVisualizations);
            this.Controls.Add(this.progBarSong);
            this.Controls.Add(this.btnRemoveSelected);
            this.Controls.Add(this.btnAddPlaylist);
            this.Controls.Add(this.lblCurrentlyPlaying);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.gbConsole);
            this.Controls.Add(this.listBoxSongs);
            this.Controls.Add(this.btnAddSongs);
            this.Name = "AudioPlayerForm";
            this.Text = "Audio Player IP";
            this.gbConsole.ResumeLayout(false);
            this.gbConsole.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxVisualizations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddSongs;
        private System.Windows.Forms.ListBox listBoxSongs;
        private System.Windows.Forms.GroupBox gbConsole;
        private System.Windows.Forms.CheckBox cbShuffle;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPrevSong;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Label lblCurrentlyPlaying;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnAddPlaylist;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Timer timerVisUpdate;
        private System.Windows.Forms.ProgressBar progBarSong;
        private System.Windows.Forms.PictureBox picBoxVisualizations;
        private System.Windows.Forms.Button btnStop;
    }
}

