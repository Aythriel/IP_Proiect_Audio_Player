﻿using NAudio.Wave;
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
        private Timer timer;

        private AudioFile currentFile;

        

        public AudioPlayer
            (
                StartingPlayingEventHandler startingPlayingNotification,
                PausedPlayerEventHandler pausedPlayerNotification,
                StoppedPlayerEventHandler stoppedPlayerNotification,
                VisualizationEventHandler sendVisualContext
            )
        {
            _stopCause = StopCause.EofReached;
            _audioPlayerState = AudioPlayerState.Stopped;
            _nextSongType = NextSongType.NextSong;
            StartingPlayingNotification = startingPlayingNotification;
            PausedPlayerNotification = pausedPlayerNotification;
            StoppedPlayerNotification = stoppedPlayerNotification;
            SendVisualContext = sendVisualContext;
        }

        //starting playing the ...player
        public void PlaySong(AudioFile audioFile)
        {
            timer = new Timer(100);
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;
            timer.Start();
            currentFile = audioFile;
            if (_audioPlayerState == AudioPlayerState.Stopped)
            {
                _stopCause = StopCause.EofReached;
                _nextSongType = NextSongType.NextSong;
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
            StartingPlayingNotification(new object(), new StartedPlayerEventArgs());
        }
        public delegate void StartingPlayingEventHandler(object sender, StartedPlayerEventArgs e);
        public event StartingPlayingEventHandler StartingPlayingNotification;

        //play NEXT song
        public void PlayNextSong()
        {
            _nextSongType = NextSongType.NextSong;
            _waveStream.CurrentTime = TimeSpan.FromSeconds(_waveStream.TotalTime.TotalSeconds);
        }

        //play PREV song
        public void PlayPreviousSong()
        {
            _nextSongType = NextSongType.PrevSong;
            _waveStream.CurrentTime = TimeSpan.FromSeconds(_waveStream.TotalTime.TotalSeconds);
        }

        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            SendVisualContext(
                new object(),
                new VisualizationEventArgs
                (
                    currentFile.Name,
                    (int)_waveStream.TotalTime.TotalMilliseconds,
                    (int)_waveStream.CurrentTime.TotalMilliseconds)
                );
        }

        public delegate void VisualizationEventHandler(object sender, VisualizationEventArgs e);
        public event VisualizationEventHandler SendVisualContext;

        //pausing the player
        public void PauseSong()
        {
            if (_audioPlayerState == AudioPlayerState.Playing)
            {
                timer.Stop();
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
                timer.Stop();
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
