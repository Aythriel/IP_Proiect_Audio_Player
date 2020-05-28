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
        private Random _random;
        public int NextSong(int current, int total)
        {
            _random = new Random(DateTime.Now.Second);
            int nextSong = _random.Next(0, total);
            while (nextSong == current || nextSong == current + 1 || nextSong == current - 1)
            {
                nextSong = _random.Next(0, total);
            }
            return nextSong;
        }

        public int PrevSong(int current, int total)
        {
            if (current == 0)
            {
                return total - 1;
            } 
            else
            {
                return current - 1;
            }
        }
    }
}