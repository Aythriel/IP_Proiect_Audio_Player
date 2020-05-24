using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerLib
{
    interface IPlayStrategy
    {
        int NextSong(int current, int total);
    }
}
