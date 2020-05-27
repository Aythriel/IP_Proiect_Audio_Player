using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// File: DefaultNextSong.cs 
// Author: Bogdan Vacariuc
// Done: May 2020
// Purpose: A strategy for generating the index of the next melody 
//that will be played when the PlayMode is set to DEFAULT.

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
