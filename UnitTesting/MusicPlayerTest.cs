using System;
using AudioPlayerLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTesting
{
    [TestClass]
    public class MusicPlayerTest
    {
        private MusicPlayer _musicPlayer;

        [TestInitialize]
        public void init()
        {
            _musicPlayer = new MusicPlayer(null, null, null, null, new System.Windows.Forms.Timer(), new System.Windows.Forms.ProgressBar(), new System.Windows.Forms.PictureBox(), null);
        }

        [TestMethod]
        public void LoadNewPlaylistShouldWork()
        {
            int count = _musicPlayer.LoadNewPlaylist("");
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void AddNewMelodyShouldWork()
        {
            int count = _musicPlayer.AddToPlaylist(".//TestResources//test2.mp3");
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void RemoveMelodyShouldWork()
        {
            int count = _musicPlayer.AddToPlaylist(".//TestResources//test2.mp3");
            Assert.AreEqual(1, count);
            bool isRemoved = _musicPlayer.RemoveFromPlaylist(0);
            Assert.AreEqual(true, isRemoved);
        }

        [TestMethod]
        public void RemoveMelodyShouldReturnFalse()
        {
            int count = _musicPlayer.AddToPlaylist(".//TestResources//test2.mp3");
            Assert.AreEqual(1, count);
            bool isRemoved = _musicPlayer.RemoveFromPlaylist(0);
            Assert.AreEqual(true, isRemoved);
        }

    }
}
