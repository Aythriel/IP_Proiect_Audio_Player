using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AudioPlayerLib.AudioPlayer;

//makes it so when building in debug, privates from this class are visible to unit test projects
#if DEBUG
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("UnitTesting")]
#endif
namespace AudioPlayerLib
{
    class MusicPlayer
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
                            UpdateVisualizeEventHandler updateVisualizeNotification) {
            _playList = Playlist.getInstance();
            _audioPlayer = new AudioPlayer(StartedPlayingEventHandler, PausedPlayerEventHandler, StoppedPlayerEventHandler);
            PlayModeNotification = playModeNotification;
            PausedPlayerEvent = pausedPlayerEvent;
            StartedPlayingEvent = startedPlayingEvent;
            StoppedPlayerNotification = stoppedPlayerNotification;
            UpdateVisualizeNotification += updateVisualizeNotification;
            _audioVisualizer = new AudioVisualizer(progressBar, pictureBox, timer);
        }

        public int loadNewPlaylist(string path)
        {
            _playList.Clear();
            return _playList.AddSongs(path);
        }

        public int addToPlaylist(string path)
        {
            return _playList.AddSongs(path);
        }

        public bool removeFromPlaylist(int songid)
        {
            return _playList.RemoveSong(songid);
        }

        public void selectSong(int songid)
        {
            _currentSong = songid;
            _audioVisualizer.SetSong(_playList.GetSong(_currentSong));
            play();
        }

        public void stop()
        {
            _audioPlayer.StopSong();
        }

        public void play()
        {
            //_audioPlayer.StopSong();
            _audioPlayer.PlaySong(_playList.GetSong(_currentSong));
        }

        public void next()
        {
            _audioVisualizer.SetSong(_playList.GetSong(_currentSong));
            _currentSong = _playNext.NextSong(_currentSong, _playList.Size);
            play();
        }

        public void pause()
        {
            _audioPlayer.PauseSong();
        }

        public void previous()
        {
            if(_currentSong > 0)
            {
                _audioPlayer.StopSong();
                _currentSong--; 
                _audioPlayer.PlayPreviousSong();
                _audioVisualizer.SetSong(_playList.GetSong(_currentSong));
                play();
            }
        }

        public void setPlayMode(PlayMode playMode)
        {
            if (playMode.Equals(PlayMode.Default)) _playNext = new DefaultNextSong();
            else _playNext = new ShuffleNextSong();
            PlayModeNotification(new object(), new PlayModeEventArgs(playMode));
        }
        public delegate void PlayModeEventHandler(Object sender, PlayModeEventArgs e);
        public event PlayModeEventHandler PlayModeNotification;

        private void PausedPlayerEventHandler(Object sender, PausedPlayerEventArgs e)
        {
            _audioVisualizer.OnPauseEvent(sender, e);
            PausedPlayerEvent(new object(), new PausedPlayerEventArgs());
        }
        public delegate void PausedPlayerNotificationEventHandler(Object sender, PausedPlayerEventArgs e);
        public event PausedPlayerNotificationEventHandler PausedPlayerEvent;

        private void StartedPlayingEventHandler(object sender, StartedPlayerEventArgs e)
        {
            _audioVisualizer.OnStartEvent(sender, e);
            StartedPlayingEvent(new object(), e);
        }
        public delegate void StartedPlayingNotificationEventHandler(Object sender, StartedPlayerEventArgs e);
        public event StartedPlayingNotificationEventHandler StartedPlayingEvent;

        private void StoppedPlayerEventHandler(Object sender, StoppedPlayerEventArgs e)
        {
            if (e.type.Equals("EOF"))
            {
                if (e.nextSongType.Equals("NEXT"))
                {
                    next();
                }
                else if (e.nextSongType.Equals("PREV"))
                {
                    previous();
                }
                else
                {
                    _currentSong = 0;
                    play();
                }
            }
            else if (e.type.Equals("USR"))
            {
                stop();
            }
            StoppedPlayerNotification(new object(), e);
        }
        public delegate void StoppedPlayerEventHandlerNotificaiton(Object sender, StoppedPlayerEventArgs e);
        public event StoppedPlayerEventHandlerNotificaiton StoppedPlayerNotification;

        public void UpdateVisualize(Object sender, EventArgs e)
        {
            _audioVisualizer.VisualUpdateEvent(sender, e);
        }
        public delegate void UpdateVisualizeEventHandler(Object sender, EventArgs e);
        public event UpdateVisualizeEventHandler UpdateVisualizeNotification;
    }
}
