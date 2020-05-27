using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AudioPlayerLib;
using System.Threading;

namespace UnitTesting
{
    [TestClass]
    public class AudioPlayerUnitTest
    {
        private AudioPlayer _audioPlayer;
        private AudioPlayer.AudioPlayerState _playerState;
        private AudioFile _audioFile_mp3;
        private AudioFile _audioFile_aac;
        private AudioFile _audioFile_flac;
        private string songStoppedArgs;
        private void StartingPlayingEventHandler(object sender, StartedPlayerEventArgs e)
        {
            _playerState = AudioPlayer.AudioPlayerState.Playing;
        }

        private void PausedPlayerEventHandler(object sender, PausedPlayerEventArgs e)
        {
            _playerState = AudioPlayer.AudioPlayerState.Paused;
        }

        private void StoppedPlayerEventHandler(object sender, StoppedPlayerEventArgs e)
        {
            _playerState = AudioPlayer.AudioPlayerState.Stopped;
            if (e.type.Equals("EOF"))
            {
                if (e.nextSongType.Equals("NEXT"))
                {
                    songStoppedArgs = "EOF_NEXT";
                }
                else if (e.nextSongType.Equals("PREV"))
                {
                    songStoppedArgs = "EOF_NEXT";
                }
                else
                {
                    songStoppedArgs = "EOF_PREV";
                }

            }
            else if (e.type.Equals("USR"))
            {
                songStoppedArgs = "USR";
            }
        }

        [TestInitialize]
        public void Init()
        {
            _audioPlayer = new AudioPlayer
            (
                StartingPlayingEventHandler,
                PausedPlayerEventHandler,
                StoppedPlayerEventHandler
            );
            _audioFile_mp3  = new AudioFile(@"TestData\Songs1\mp3Sample.mp3");
            _audioFile_aac  = new AudioFile(@"TestData\Songs1\aacSample.aac");
            _audioFile_flac = new AudioFile(@"TestData\Songs1\aacSample.flac");
        }

        [TestMethod]
        public void TestSequence()
        {
            Test_01_PlaySong_mp3();
            Test_02_StopSong_mp3();
            Test_03_PlaySong_aac();
            Test_04_StopSong_aac();
            Test_05_PlaySong_flac();
            Test_06_StopSong_flac();
            Test_07_PlaySongBeforePause_mp3();
            Test_08_PauseSong_mp3();
            Test_09_PlaySongAfterPause_mp3();
            Test_10_StopSong_mp3();
            Test_11_PlaySongBeforeNext_aac();
            Test_12_NextSong_aac();
            Test_13_PlaySongBeforePrev_flac();
            Test_14_StopSong_flac();
            Test_15_PlaySongBeforePrev_flac();
        }

        //[TestMethod]
        public void Test_01_PlaySong_mp3()
        {
            Thread.Sleep(1000);
            _audioPlayer.PlaySong(_audioFile_mp3);
            Thread.Sleep(200);
            Assert.AreEqual(_playerState,_audioPlayer._audioPlayerState);
        }

        //[TestMethod]
        public void Test_02_StopSong_mp3()
        {
            Thread.Sleep(1000);
            _audioPlayer.StopSong();
            Thread.Sleep(200);
            //Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
            Assert.AreEqual("USR", songStoppedArgs);
        }
        //[TestMethod]
        public void Test_03_PlaySong_aac()
        {
            Thread.Sleep(1000);
            _audioPlayer.PlaySong(_audioFile_aac);
            Thread.Sleep(200);
            Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
        }

        //[TestMethod]
        public void Test_04_StopSong_aac()
        {
            Thread.Sleep(1000);
            _audioPlayer.StopSong();
            Thread.Sleep(200);
            Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
            Assert.AreEqual("USR", songStoppedArgs);
        }

        //[TestMethod]
        public void Test_05_PlaySong_flac()
        {
            Thread.Sleep(1000);
            _audioPlayer.PlaySong(_audioFile_flac);
            Thread.Sleep(200);
            Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
        }

        //[TestMethod]
        public void Test_06_StopSong_flac()
        {
            Thread.Sleep(1000);
            _audioPlayer.StopSong();
            Thread.Sleep(200);
            Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
            Assert.AreEqual("USR", songStoppedArgs);
        }

        //[TestMethod]
        public void Test_07_PlaySongBeforePause_mp3()
        {
            Thread.Sleep(1000);
            _audioPlayer.PlaySong(_audioFile_mp3);
            Thread.Sleep(200);
            Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
        }

        //[TestMethod]
        public void Test_08_PauseSong_mp3()
        {
            Thread.Sleep(1000);
            _audioPlayer.PauseSong();
            Thread.Sleep(200);
            Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
        }

        //[TestMethod]
        public void Test_09_PlaySongAfterPause_mp3()
        {
            Thread.Sleep(1000);
            _audioPlayer.PlaySong(_audioFile_mp3);
            Thread.Sleep(200);
            Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
        }

        //[TestMethod]
        public void Test_10_StopSong_mp3()
        {
            Thread.Sleep(1000);
            _audioPlayer.StopSong();
            Thread.Sleep(200);
            Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
            Assert.AreEqual("USR", songStoppedArgs);
        }

        //[TestMethod]
        public void Test_11_PlaySongBeforeNext_aac()
        {
            Thread.Sleep(1000);
            _audioPlayer.PlaySong(_audioFile_aac);
            Thread.Sleep(200);
            Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
        }

        //[TestMethod]
        public void Test_12_NextSong_aac()
        {
            Thread.Sleep(1000);
            _audioPlayer.PlayNextSong();
            Thread.Sleep(200);
            Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
            Assert.AreEqual("EOF_NEXT", songStoppedArgs);
        }

        //[TestMethod]
        public void Test_13_PlaySongBeforePrev_flac()
        {
            Thread.Sleep(1000);
            _audioPlayer.PlaySong(_audioFile_flac);
            Thread.Sleep(200);
            Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
        }

        //[TestMethod]
        public void Test_14_StopSong_flac()
        {
            Thread.Sleep(1000);
            _audioPlayer.PlayPreviousSong();
            Thread.Sleep(200);
            Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
            Assert.AreEqual("EOF_PREV", songStoppedArgs);
        }

        //[TestMethod]
        public void Test_15_PlaySongBeforePrev_flac()
        {
            Thread.Sleep(1000);
            _audioPlayer.PlaySong(_audioFile_flac);
            Thread.Sleep(200);
            //Assert.AreEqual(_playerState, _audioPlayer._audioPlayerState);
            while (_audioPlayer._audioPlayerState == AudioPlayer.AudioPlayerState.Playing);
            Thread.Sleep(1000);
            Assert.AreEqual("EOF_NEXT", songStoppedArgs);
        }
    }
}
