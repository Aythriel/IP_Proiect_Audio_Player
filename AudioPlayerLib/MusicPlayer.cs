using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        private Thread _audioThread;
        private Thread _visualThread;
        private IPlayStrategy _playNext = new DefaultNextSong();
        private int _currentSong=0;
        public enum PlayMode { Default, Shuffle};

        public MusicPlayer(PlayModeEventHandler playModeNotification,
                            PausedPlayerNotificationEventHandler pausedPlayerEvent,
                            StartedPlayingNotificationEventHandler startedPlayingEvent,
                            StoppedPlayerEventHandlerNotificaiton stoppedPlayerNotification,
                            VisualizationEventHandlerNotification sendVisualContext) {
            _playList = Playlist.getInstance();
            _audioPlayer = new AudioPlayer(StartedPlayingEventHandler, PausedPlayerEventHandler, StoppedPlayerEventHandler, VisualizationEventHandler);
            PlayModeNotification = playModeNotification;
            PausedPlayerEvent = pausedPlayerEvent;
            StartedPlayingEvent = startedPlayingEvent;
            StoppedPlayerNotification = stoppedPlayerNotification;
            SendVisualContext = sendVisualContext;
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
            play();
        }

        public void play()
        {
            _audioPlayer.StopSong();
            _audioPlayer.PlaySong(_playList.GetSong(_currentSong));
        }

        public void next()
        {
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
            PausedPlayerEvent(new object(), new PausedPlayerEventArgs());
        }
        public delegate void PausedPlayerNotificationEventHandler(Object sender, PausedPlayerEventArgs e);
        public event PausedPlayerNotificationEventHandler PausedPlayerEvent;

        private void StartedPlayingEventHandler(object sender, StartedPlayerEventArgs e)
        {
            StartedPlayingEvent(new object(), e);
        }
        public delegate void StartedPlayingNotificationEventHandler(Object sender, StartedPlayerEventArgs e);
        public event StartedPlayingNotificationEventHandler StartedPlayingEvent;

        private void StoppedPlayerEventHandler(Object sender, StoppedPlayerEventArgs e)
        {
            if(e.type == "EOF")
            {
                next();
            }
            StoppedPlayerNotification(new object(), e);
        }
        public delegate void StoppedPlayerEventHandlerNotificaiton(Object sender, StoppedPlayerEventArgs e);
        public event StoppedPlayerEventHandlerNotificaiton StoppedPlayerNotification;

        private void VisualizationEventHandler(object sender, VisualizationEventArgs e)
        {
            SendVisualContext(new object(), e);
        }
        public delegate void VisualizationEventHandlerNotification(object sender, VisualizationEventArgs e);
        public event VisualizationEventHandlerNotification SendVisualContext;



    }
}
