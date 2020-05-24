using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AudioPlayerLib
{
    class AudioFile
    {
        private readonly string _format;
        private readonly string _path;
        private readonly string _name;

        public string Format => _format;
        public string Path => _path;
        public string Name => _name;


        public AudioFile(string path)
        {
            if (path == null)
                throw new Exception("Attempted to create an AudioFile with null path.");
            if (System.IO.Path.GetFileName(path) == String.Empty)
                throw new Exception("Attempted to create an AudioFile with a path that doesn't point to a file.");
            if (AcceptsFormat(path) == false)
                throw new Exception("Attempted to create an AudioFile with an unaccepted file extension.");
            
            _path = path;
            _name = System.IO.Path.GetFileNameWithoutExtension(path);
            _format = System.IO.Path.GetExtension(path);
        }
      
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
