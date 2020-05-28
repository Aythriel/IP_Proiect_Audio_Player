// File: AudioPlayer.cs
// Author: Cosmin Popovici
// Done: May 2020
// Purpose: Class encapsulating the lowest level logic of the player 


using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AudioPlayerLib
{
    class AudioPlayer
    {
        public enum AudioPlayerState { Playing, Paused, Stopped };
        public enum StopCause { EofReached, UserTriggered };
        public enum NextSongType { NextSong, PrevSong, None };

        public AudioPlayerState _audioPlayerState;
        private StopCause _stopCause;
        private NextSongType _nextSongType;

        private DirectSoundOut _directSoundOut;
        private WaveStream _waveStream;
        private WaveChannel32 _waveChannel32;
        private Timer _timer;

        private AudioFile _currentFile;

        

        public AudioPlayer
            (
                StartedPlayingEventHandler startedPlayingNotification,
                PausedPlayerEventHandler pausedPlayerNotification,
                StoppedPlayerEventHandler stoppedPlayerNotification
            )
        {
            _stopCause = StopCause.EofReached;
            _audioPlayerState = AudioPlayerState.Stopped;
            _nextSongType = NextSongType.NextSong;
            StartedPlayingNotification += startedPlayingNotification;
            PausedPlayerNotification += pausedPlayerNotification;
            StoppedPlayerNotification += stoppedPlayerNotification;
        }

        //starting playing the ...player
        public void PlaySong(AudioFile audioFile)
        {
            _timer = new Timer(100);
            _timer.Enabled = true;
            _timer.Start();
            _currentFile = audioFile;
            if (_audioPlayerState == AudioPlayerState.Stopped)
            {
                _stopCause = StopCause.EofReached;
                _nextSongType = NextSongType.NextSong;
                _audioPlayerState = AudioPlayerState.Playing;
                switch (audioFile.Format.ToLower())
                {
                    case ".mp3":
                        _waveStream = new Mp3FileReader(audioFile.Path);
                        break;
                    case ".wav":
                        _waveStream = new WaveFileReader(audioFile.Path);
                        break;
                    case ".aiff":
                        _waveStream = new AiffFileReader(audioFile.Path);
                        break;
                    default:
                        _waveStream = new AudioFileReader(audioFile.Path);
                        break;
                }
                _directSoundOut = new DirectSoundOut(150);

                _waveChannel32 = new WaveChannel32(_waveStream, 5, 0)
                {
                    PadWithZeroes = false
                };
                _directSoundOut.Init(_waveChannel32);
                _directSoundOut.Play();
                _directSoundOut.PlaybackStopped += DirectSoundOutput_PlaybackStopped;
            }
            else if (_audioPlayerState == AudioPlayerState.Paused)
            {
                _audioPlayerState = AudioPlayerState.Playing;
                _directSoundOut.Play();
            }
            StartedPlayingNotification(new object(), new StartedPlayerEventArgs());
        }
        public delegate void StartedPlayingEventHandler(object sender, StartedPlayerEventArgs e);
        public event StartedPlayingEventHandler StartedPlayingNotification;

        //play NEXT song
        public void PlayNextSong()
        {
            _nextSongType = NextSongType.NextSong;
            _waveStream.CurrentTime = TimeSpan.FromSeconds(_waveStream.TotalTime.TotalSeconds-0.1);
        }

        //play PREV song
        public void PlayPreviousSong()
        {
            _nextSongType = NextSongType.PrevSong;
            _waveStream.CurrentTime = TimeSpan.FromSeconds(_waveStream.TotalTime.TotalSeconds-0.1);
        }
        

        //pausing the player
        public void PauseSong()
        {
            if (_audioPlayerState == AudioPlayerState.Playing)
            {
                _timer.Stop();
                _audioPlayerState = AudioPlayerState.Paused;
                _directSoundOut.Pause();
                PausedPlayerNotification(new object(), new PausedPlayerEventArgs());
            }
        }

        public delegate void PausedPlayerEventHandler(Object sender, PausedPlayerEventArgs e);
        public event PausedPlayerEventHandler PausedPlayerNotification;

        //stopping the player
        public void StopSong()
        {
            if (_audioPlayerState != AudioPlayerState.Stopped)
            {
                _timer.Stop();
                _stopCause = StopCause.UserTriggered;
                _directSoundOut.Stop();
                _audioPlayerState = AudioPlayerState.Stopped;
            }
        }

        private void DirectSoundOutput_PlaybackStopped(object sender, StoppedEventArgs sea)
        {
            StoppedPlayerEventArgs stoppedPlayerEventArgs = null;
            string nextSongTyp = "";
            if (_stopCause == StopCause.EofReached)
            {
                _audioPlayerState = AudioPlayerState.Stopped;
                if (_nextSongType == NextSongType.NextSong) nextSongTyp = "NEXT";
                else if (_nextSongType == NextSongType.PrevSong) nextSongTyp = "PREV";
                else nextSongTyp = "NONE";
                _nextSongType = NextSongType.NextSong;
                stoppedPlayerEventArgs = new StoppedPlayerEventArgs("EOF", nextSongTyp);
            }
            else if (_stopCause == StopCause.UserTriggered)
            {
                _audioPlayerState = AudioPlayerState.Stopped;
                stoppedPlayerEventArgs = new StoppedPlayerEventArgs("USR", "NONE");
            }
            //else stoppedPlayerEventArgs = new StoppedPlayerEventArgs("UNK");
            StoppedPlayerNotification(new object(), stoppedPlayerEventArgs);
        }
        public delegate void StoppedPlayerEventHandler(Object sender, StoppedPlayerEventArgs e);
        public event StoppedPlayerEventHandler StoppedPlayerNotification;
    }
}
