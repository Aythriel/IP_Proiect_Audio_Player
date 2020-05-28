using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AudioPlayerLib.MusicPlayer;

namespace AudioPlayerLib
{
    public class PlayModeEventArgs
    {

        public readonly PlayMode _type;

        public PlayModeEventArgs(PlayMode type) : base() {
            _type = type;
        }
    }
}
