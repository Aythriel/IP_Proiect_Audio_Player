using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerLib
{
    class DefaultNextSong : IPlayStrategy
    {
        public int NextSong(int current, int total)
        {
            return (current + 1) % total;
        }
    }
}
