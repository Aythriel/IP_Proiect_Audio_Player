// ================================
// File: Playlist.cs
// Author: Teodorovici Silviu
// Purpose: Manages a list of songs (audio files) to be used by the music player.
// ================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AudioPlayerLib
{
    class Playlist
    {
        private static Playlist _instance;

        private List<AudioFile> _songs;
        private ListBox _listBoxSongs;

        public int Size { get { return _songs.Count; } }

        public void setControl(ListBox listBoxSongs)
        {
            this._listBoxSongs = listBoxSongs;
        }

        private Playlist()
        {
            _songs = new List<AudioFile>();
        }

        // Method to access the playlist instance (creates it if there isn't one already)
        public static Playlist Instance()
        {
            if (_instance == null)
                _instance = new Playlist();

            return _instance;
        }

        // If the path points to a file it will add it to the playlist if the format is supported
        // If the path point to a directory it will add any supported files from it to the playlist (doesn't check subdirectories)
        // Returns the number of songs added to the playlist
        public int AddSongs(string path)
        {
            int count = 0;

            if (path != "")
            {
               
                
                string[] files = Directory.GetFiles(path);

                foreach (string file in files)
                {
                    if (AudioFile.AcceptsFormat(file))
                    {
                        _songs.Add(new AudioFile(file));
                        _listBoxSongs.Items.Add(file);
                        count++;
                    }
                }

            }

            return count;
        }

        // Removes a song from the playlist, taking its index as argument
        // Returns false if index is out of range, true otherwise
        public bool RemoveSong(int idx)
        {
            if (idx < 0 || idx >= Size)
                return false;

            _songs.RemoveAt(idx);
            _listBoxSongs.Items.RemoveAt(idx);
            return true;
        }

        // Returns the audio file at the index given as argument
        // Returns null if index is out of range
        public AudioFile GetSong(int idx)
        {
            if (idx < 0 || idx >= Size)
                return null;

            return _songs[idx];
        }

        // Removes all songs from playlist
        public void Clear()
        {
            _songs.Clear();
            _listBoxSongs.Items.Clear();
        }
    }
}