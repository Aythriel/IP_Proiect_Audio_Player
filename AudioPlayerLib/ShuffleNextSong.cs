using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// File: ShuffleNextSong.cs 
// Author: Bogdan Vacariuc
// Done: May 2020
// Purpose: A strategy for generating the index of the next melody 
// that will be played when the PlayMode is set to SHUFFLE.
namespace AudioPlayerLib
{
    class ShuffleNextSong : IPlayStrategy
    {
        private Random _random = new Random(DateTime.Now.Second);
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