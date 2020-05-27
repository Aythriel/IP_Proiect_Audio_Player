using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// File: AudioPlayerLib.cs 
// Author: Bogdan Vacariuc
// Done: May 2020
// Purpose: Interface for PlayMode types
namespace AudioPlayerLib
{
    interface IPlayStrategy
    {
        int NextSong(int current, int total);
    }
}
