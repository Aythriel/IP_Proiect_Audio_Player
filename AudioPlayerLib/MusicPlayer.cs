using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AudioPlayerLib.AudioPlayer;

// File: MusicPlayer.cs 
// Author: Bogdan Vacariuc
// Done: May 2020
// Purpose: MusicPlayer facade that will be called from the interface.

//makes it so when building in debug, privates from this class are visible to unit test projects
#if DEBUG
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("UnitTesting")]
#endif
namespace AudioPlayerLib
{
    public class MusicPlayer
    {
        private Playlist _playList;
        private AudioPlayer _audioPlayer;
        private AudioVisualizer _audioVisualizer;
        private IPlayStrategy _playNext = new DefaultNextSong();
        private int _currentSong=0;

        public enum PlayMode { Default, Shuffle};

        public MusicPlayer(PlayModeEventHandler playModeNotification,
                            PausedPlayerNotificationEventHandler pausedPlayerEvent,
                            StartedPlayingNotificationEventHandler startedPlayingEvent,
                            StoppedPlayerEventHandlerNotificaiton stoppedPlayerNotification,
                            Timer timer,
                            ProgressBar progressBar,
                            PictureBox pictureBox,
                            ListBox listBoxSongs,
                            UpdateVisualizeEventHandler updateVisualizeNotification) {
            _playList = Playlist.Instance();
            _playList.setControl(listBoxSongs);
            _audioPlayer = new AudioPlayer(StartedPlayingEventHandler, PausedPlayerEventHandler, StoppedPlayerEventHandler);
            PlayModeNotification = playModeNotification;
            PausedPlayerEvent = pausedPlayerEvent;
            StartedPlayingEvent = startedPlayingEvent;
            StoppedPlayerNotification = stoppedPlayerNotification;
            UpdateVisualizeNotification += updateVisualizeNotification;
            _audioVisualizer = new AudioVisualizer(progressBar, pictureBox, timer);
            timer.Tick += UpdateVisualize;
        }

        // adds a new playlist from the given path
        public int LoadNewPlaylist(string path)
        {
            _playList.Clear();
            return _playList.AddSongs(path);
        }
        // adds the melodies from the given path to the existing playlist
        public int AddToPlaylist(string path)
        {
            return _playList.AddSongs(path);
        }

        public bool RemoveFromPlaylist(int songid)
        {
            return _playList.RemoveSong(songid);
        }

        // selects the song with the songid
        public void SelectSong(int songid)
        {
            _currentSong = songid;
            _audioVisualizer.SetSong(_playList.GetSong(_currentSong));
            Play();
        }

        // stops playing the current song
        public void Stop()
        {
            _audioPlayer.StopSong();
            _audioVisualizer.OnPauseEvent(this, null);
        }

        public void Play()
        {
            //_audioPlayer.StopSong();
            _audioPlayer.PlaySong(_playList.GetSong(_currentSong));
        }

        // switches to next song
        public void Next()
        {
            _audioVisualizer.SetSong(_playList.GetSong(_currentSong));
            _currentSong = _playNext.NextSong(_currentSong, _playList.Size);
            Play();
        }

        public void Pause()
        {
            _audioPlayer.PauseSong();
            _audioVisualizer.OnPauseEvent(this, null);
        }

        // switches to previous song
        public void Previous()
        {
            if(_currentSong > 0)
            {
                _audioPlayer.StopSong();
                _currentSong--; 
                _audioPlayer.PlayPreviousSong();
                _audioVisualizer.SetSong(_playList.GetSong(_currentSong));
                Play();
            }
        }

        // sends events when the play mode is changed
        public void SetPlayMode(PlayMode playMode)
        {
            if (playMode.Equals(PlayMode.Default)) _playNext = new DefaultNextSong();
            else _playNext = new ShuffleNextSong();
            PlayModeNotification(new object(), new PlayModeEventArgs(playMode));
        }
        public delegate void PlayModeEventHandler(Object sender, PlayModeEventArgs e);
        public event PlayModeEventHandler PlayModeNotification;

        // sends events when the melody is paused
        private void PausedPlayerEventHandler(Object sender, PausedPlayerEventArgs e)
        {
            _audioVisualizer.OnPauseEvent(sender, e);
            PausedPlayerEvent(new object(), new PausedPlayerEventArgs());
        }
        public delegate void PausedPlayerNotificationEventHandler(Object sender, PausedPlayerEventArgs e);
        public event PausedPlayerNotificationEventHandler PausedPlayerEvent;

        // sends events when the melody started to play
        private void StartedPlayingEventHandler(object sender, StartedPlayerEventArgs e)
        {
            _audioVisualizer.OnStartEvent(sender, e);
            StartedPlayingEvent(new object(), e);
        }
        public delegate void StartedPlayingNotificationEventHandler(Object sender, StartedPlayerEventArgs e);
        public event StartedPlayingNotificationEventHandler StartedPlayingEvent;

        // sends events when the melody is stopped
        private void StoppedPlayerEventHandler(Object sender, StoppedPlayerEventArgs e)
        {
            if (e.type.Equals("EOF"))
            {
                if (e.nextSongType.Equals("NEXT"))
                {
                    Next();
                }
                else if (e.nextSongType.Equals("PREV"))
                {
                    Previous();
                }
                else
                {
                    _currentSong = 0;
                    Play();
                }
            }
            else if (e.type.Equals("USR"))
            {
                Stop();
            }
            StoppedPlayerNotification(new object(), e);
        }
        public delegate void StoppedPlayerEventHandlerNotificaiton(Object sender, StoppedPlayerEventArgs e);
        public event StoppedPlayerEventHandlerNotificaiton StoppedPlayerNotification;

        // sends events when the visualizer is updated
        public void UpdateVisualize(Object sender, EventArgs e)
        {
            _audioVisualizer.VisualUpdateEvent(sender, e);
        }
        public delegate void UpdateVisualizeEventHandler(Object sender, EventArgs e);
        public event UpdateVisualizeEventHandler UpdateVisualizeNotification;
    }
}
