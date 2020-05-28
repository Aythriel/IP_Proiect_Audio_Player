using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.IO;
using System.Web;
using System.Windows.Forms;
using NAudio.Wave;

// File: AudioVisualizer.cs 
// Author: Cana Andrei - https://github.com/Aythriel
// Done: May 2020
// Purpose: A class that is a subpart of an audio player project. Its purpose is to generate pictures and swap
// them on a timer to visualize song data in the form of high/low waves. 

namespace AudioPlayerLib
{
    public class AudioVisualizer
    {

        //data structures used
        private Bitmap _currentSongImage = null;
        private Timer _visEventTimer = null;
        private ProgressBar _progBar = null;
        private PictureBox _picBox = null;
        private bool _paused = false;
        private string _currentSong = null;
        public AudioVisualizer(ProgressBar progBar, PictureBox picBox, Timer visEventTimer) 
        {
            if (null == progBar || null == picBox || null == visEventTimer)
                throw new NullReferenceException("Can't instantiate AudioVisualizer with null references in the constructor.");
            
            _progBar = progBar;
            _picBox = picBox;
            _visEventTimer = visEventTimer;
            _visEventTimer.Enabled = false;

            _progBar.Step = 1;
            _progBar.Value = 0;
        }



        public void SetSong(AudioFile file)
        {
            if (null == file)
                throw new NullReferenceException("Visualizer can't generate image for null AudioFile reference");
            if(file.Name!=this._currentSong) // do this to avoid double initialization on song playback or song unpause
            {
                _visEventTimer.Enabled = false;
                if(null!=_picBox.Image)
                    _picBox.Image.Dispose();
                _progBar.Value = 0;
                //CreateCurrentSongImage also initializes the progress bar maximum; great design - I know
                CreateCurrentSongImage(file);
                _visEventTimer.Enabled = true;
                this._currentSong = file.Name;
            }

        }


        public void OnPauseEvent(object sender, EventArgs args)
        {
            _visEventTimer.Enabled = false;
            _paused = true;
        }

        public void OnStartEvent(object sender, EventArgs args)
        {
            _visEventTimer.Enabled = true;
            _paused = false;
        }
        public void VisualUpdateEvent(object sender, EventArgs args)
        {

            if (_paused || _progBar.Value == _progBar.Maximum)
            {
                _visEventTimer.Enabled = false;
                return;
            }
                
            int intervalWidth = _currentSongImage.Width / _progBar.Maximum;
            int intervalIndex = _progBar.Value;


            int start = Math.Min((intervalIndex * intervalWidth), (_currentSongImage.Width - intervalWidth));
            
            int cutWidth = Math.Min((start + intervalWidth), _picBox.Width);

            //towards the end, the box must shrink to avoid overshooting
            if (start + cutWidth > _currentSongImage.Width)
                cutWidth = _currentSongImage.Width - start;

            Rectangle cuttingRectangle = new Rectangle(start, 0, cutWidth, _currentSongImage.Height);
            System.Drawing.Imaging.PixelFormat format =
                _currentSongImage.PixelFormat;


            if (_picBox.Image != null)
                _picBox.Image.Dispose(); //dispose of the image to avoid memory leaks

            //create image for next frame using the rectangle to cut
            _picBox.Image = _currentSongImage.Clone(cuttingRectangle, format); ;
            _picBox.Update();


            _progBar.PerformStep();
           
        }
        
        public void CreateCurrentSongImage(AudioFile audioFile)
        {
            try
            {
                int bytesPerSample = 0;
                WaveStream reader;
                switch (audioFile.Format.ToLower())
                {
                    case ".mp3":
                        reader = new Mp3FileReader(audioFile.Path);
                        break;
                    case ".wav":
                        reader = new WaveFileReader(audioFile.Path);
                        break;
                    case ".aiff":
                        reader = new AiffFileReader(audioFile.Path);
                        break;
                    default:
                        reader = new AudioFileReader(audioFile.Path);
                        break;
                }
                                
                using (NAudio.Wave.WaveChannel32 channelStream = new NAudio.Wave.WaveChannel32(reader))
                {
                        
                    //initialize the progress bar, once the audio stream has been created
                    _progBar.Maximum = (int)reader.TotalTime.TotalMilliseconds / _visEventTimer.Interval;

                    bytesPerSample = (reader.WaveFormat.BitsPerSample / 8) * channelStream.WaveFormat.Channels;

                    //Give a size to the bitmap; either a fixed size, or something based on the length of the audio
                    using (Bitmap bitmap = new Bitmap((int)Math.Round(reader.TotalTime.TotalSeconds * 80), 300))
                    {
                        int width = bitmap.Width;
                        int height = bitmap.Height;

                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.Clear(Color.White);
                            Pen bluePen = new Pen(Color.Blue);

                            int samplesPerPixel = (int)(reader.Length / (double)(width * bytesPerSample));
                            int bytesPerPixel = bytesPerSample * samplesPerPixel;
                            int bytesRead;
                            byte[] waveData = new byte[bytesPerPixel];

                            for (float x = 0; x < width; x++)
                            {
                                bytesRead = reader.Read(waveData, 0, bytesPerPixel);
                                if (bytesRead == 0)
                                    break;

                                short low = 0;
                                short high = 0;
                                for (int n = 0; n < bytesRead; n += 2)
                                {
                                    short sample = BitConverter.ToInt16(waveData, n);
                                    if (sample < low) low = sample;
                                    if (sample > high) high = sample;
                                }
                                float lowPercent = ((((float)low) - short.MinValue) / ushort.MaxValue);
                                float highPercent = ((((float)high) - short.MinValue) / ushort.MaxValue);
                                float lowValue = height * lowPercent;
                                float highValue = height * highPercent;
                                graphics.DrawLine(bluePen, x, lowValue, x, highValue);
                            }
                        }

                        _currentSongImage = new System.Drawing.Bitmap(bitmap);
                        bitmap.Dispose();
                    }
                }

                reader.Dispose();
                

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
