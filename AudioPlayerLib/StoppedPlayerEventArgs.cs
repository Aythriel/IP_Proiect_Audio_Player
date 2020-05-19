using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerLib
{
    class StoppedPlayerEventArgs:EventArgs
    {
        public readonly string type;

        public StoppedPlayerEventArgs(string type) : base()
        {
            this.type = type;
        }
    }
}
