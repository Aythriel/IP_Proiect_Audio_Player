using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerLib
{
    class VisualizationEventArgs
    {
        public readonly int totalTime;
        public readonly int currentTime;
        public readonly string name;

        public VisualizationEventArgs(string name, int tota, int curr) : base()
        {
            this.name = name;
            this.totalTime = tota;
            this.currentTime = curr;
        }
    }
}
