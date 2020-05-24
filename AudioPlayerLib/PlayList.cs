using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AudioPlayerLib
{
    class Playlist
    {
        private List<AudioFile> _songs;
        
        public int Size { get { return _songs.Count; } }

        public Playlist()
        {
            _songs = new List<AudioFile>();
        }

        public Playlist(string path)
        {
            _songs = new List<AudioFile>();

            if(Path.GetFileName(path) != String.Empty)
            {
                if (AudioFile.AcceptsFormat(path))
                    _songs.Add(new AudioFile(path));
            }
            else
            {
                string[] files = Directory.GetFiles(path);

                foreach(string file in files)
                {
                    if (AudioFile.AcceptsFormat(file))
                        _songs.Add(new AudioFile(file));
                }
            }
        }

        public int AddSongs(string path)
        {
            int count = 0;
            if (Path.GetFileName(path) != String.Empty)
            {
                if (AudioFile.AcceptsFormat(path))
                {
                    _songs.Add(new AudioFile(path));
                    count++;
                }
            }
            else
            {
                string[] files = Directory.GetFiles(path);

                foreach (string file in files)
                {
                    if (AudioFile.AcceptsFormat(file))
                    {
                        _songs.Add(new AudioFile(file));
                        count++;
                    }
                }
            }

            return count;
        }

        public bool RemoveSong(int idx)
        {
            if (idx < 0 || idx >= Size)
                return false;

            _songs.RemoveAt(idx);
            return true;
        }

        public AudioFile GetSong(int idx)
        {
            if (idx < 0 || idx >= Size)
                return null;

            return _songs[idx];
        }

        public void Clear()
        {
            _songs.Clear();
        }
    }
}
