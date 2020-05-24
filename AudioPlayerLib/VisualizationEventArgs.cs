using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerLib
{
    class VisualizationEventArgs:EventArgs
    {

        public readonly string name;
        public readonly WaveStream waveStream;

        public VisualizationEventArgs(string name, NAudio.Wave.WaveStream waveStream) : base()
        {
            this.name = name;
            this.waveStream = waveStream;
        }
    }
}
