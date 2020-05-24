using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//makes it so when building in debug, privates from this class are visible to unit test projects
#if DEBUG
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("UnitTesting")]
#endif
namespace AudioPlayerLib
{
    public class MusicPlayer
    {
        private Playlist _playList;
    }
}
