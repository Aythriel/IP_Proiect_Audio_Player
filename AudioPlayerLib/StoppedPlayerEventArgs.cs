using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerLib
{
    public class StoppedPlayerEventArgs:EventArgs
    {
        public readonly string type;
        public readonly string nextSongType;

        public StoppedPlayerEventArgs(string type, string nextSongType) : base()
        {
            this.type = type;
            this.nextSongType = nextSongType;
        }
    }
}
