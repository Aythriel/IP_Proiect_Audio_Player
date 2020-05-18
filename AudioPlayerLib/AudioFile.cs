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
            _path = path;
            string[] firstSplitComponents = _path.Split('.');
            _format = firstSplitComponents[1];
            string[] secondSplitComponents = firstSplitComponents[0].Split('\\');
            _name = secondSplitComponents[secondSplitComponents.Length - 1];
        }
    }
}
