using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if DEBUG
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("UnitTesting")]
#endif
namespace AudioPlayerLib
{
    class AudioFile
    {
        private string _format;
        private string _path;
        private string _name;

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

        public string Format { get { return _format; } }
        public string Path { get { return _path; } }
        public string Name { get { return _name; } }
    }
}
