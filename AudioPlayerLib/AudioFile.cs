// ================================
// File: AudioFile.cs
// Author: Teodorovici Silviu
// Purpose: Stores the path, name and file extension of an audio file supported by the music player.
// ================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AudioPlayerLib
{
    public class AudioFile
    {
        private readonly string _format;
        private readonly string _path;
        private readonly string _name;

        public string Format => _format;
        public string Path => _path;
        public string Name => _name;

        public AudioFile(string path)
        {
            if (path == null || path == "")
                throw new Exception("Attempted to create an AudioFile with null or empty path.");
            //if (System.IO.Path.GetFileName(path) == String.Empty)
            if (System.IO.Path.GetExtension(path) == String.Empty)
                    throw new Exception("Attempted to create an AudioFile with a path that doesn't point to a file.");
            if (AcceptsFormat(path) == false)
                throw new Exception("Attempted to create an AudioFile with an unaccepted file extension.");
            
            _path = path;
            _name = System.IO.Path.GetFileNameWithoutExtension(path);
            _format = System.IO.Path.GetExtension(path);
        }

        // Returns true if the file pointed by the path has an extension supported by the music player,
        // false otherwise (or if the path doesn't point to a file)
        public static bool AcceptsFormat(string path)
        {
            switch(System.IO.Path.GetExtension(path))
            {
                case ".mp3":
                case ".aac":
                case ".aiff":
                case ".wav":
                case ".flac":
                    return true;
                default:
                    return false;
            }
        }

    }
}
