using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerLib
{
    class ShuffleNextSong : IPlayStrategy
    {
        private static Random _random = new Random(DateTime.Now.Second);
        public int NextSong(int current, int total)
        {
            do
            {
                int nextSong = _random.Next(0, total);
                if (nextSong != current) return nextSong;
            } while (true);
        }
    }
}