using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerLib
{
    class AudioPlayer
    {
        public enum AudioPlayerState { Playing, Paused, Stopped };
        public enum StopCause { EofReached, UserTriggered };

        private AudioPlayerState _audioPlayerState;
        private StopCause _stopCause;
        private DirectSoundOut _directSoundOut;
        private WaveStream _waveStream;
        private WaveChannel32 _waveChannel32;

        public event Action PlaybackResumed;
        public event Action PlaybackStopped;
        public event Action PlaybackPaused;

        public AudioPlayer()
        {
            _audioPlayerState = AudioPlayerState.Stopped;

        }

        public void PlaySong(AudioFile audioFile)
        {
            if (_audioPlayerState == AudioPlayerState.Stopped)
            {
                _stopCause = StopCause.EofReached;
                _audioPlayerState = AudioPlayerState.Playing;
                switch (audioFile.Format.ToLower())
                {
                    case "mp3":
                        _waveStream = new Mp3FileReader(audioFile.Path);
                        break;
                    case "wav":
                        _waveStream = new WaveFileReader(audioFile.Path);
                        break;
                    case "aiff":
                        _waveStream = new AiffFileReader(audioFile.Path);
                        break;
                    default:
                        _waveStream = new AudioFileReader(audioFile.Path);
                        break;
                }
                _directSoundOut = new DirectSoundOut(150);

                _waveChannel32 = new WaveChannel32(_waveStream, 5, 0);
                _waveChannel32.PadWithZeroes = false;
                _directSoundOut.Init(_waveChannel32);
                _directSoundOut.Play();
            }
            else if (_audioPlayerState == AudioPlayerState.Paused)
            {
                _audioPlayerState = AudioPlayerState.Playing;
                _directSoundOut.Play();
            }
        }
        public void PauseSong()
        {
            if (_audioPlayerState == AudioPlayerState.Playing)
            {
                _audioPlayerState = AudioPlayerState.Paused;
                _directSoundOut.Pause();
                //to be continued with triggering specific pause action
            }
        }

        public void StopSong()
        {
            _directSoundOut.Stop();
            //to be continued with triggering specific pause action
        }


        //to be continued
    }
}
