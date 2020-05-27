// ================================
// File: PlaylistUnitTest.cs
// Author: Teodorovici Silviu
// Purpose: A unit testing class for running tests on the Playlist class
// ================================

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AudioPlayerLib;

namespace UnitTesting
{
    [TestClass]
    public class PlaylistUnitTest
    {
        [TestMethod]
        public void Singleton()
        {
            Playlist playlist = Playlist.Instance();
            playlist.AddSongs(@"..\..\TestData\Songs1\");
            Playlist ndPlaylist = Playlist.Instance();
            Assert.AreEqual(playlist.Size, ndPlaylist.Size);
            for(int i=0; i<playlist.Size; ++i)
            {
                Assert.AreEqual(playlist.GetSong(i).Path, ndPlaylist.GetSong(i).Path);
            }
            playlist.Clear();
            Assert.AreEqual(0, ndPlaylist.Size);
        }

        [TestMethod]
        public void Clearing()
        {
            Playlist playlist = Playlist.Instance();
            if(playlist.Size < 1)
                playlist.AddSongs(@"..\..\TestData\Songs1\");
            playlist.Clear();
            Assert.AreEqual(0, playlist.Size);
        }

        [TestMethod]
        public void AddingSongs()
        {
            Playlist playlist = Playlist.Instance();
            playlist.Clear();
            int noSongs = playlist.AddSongs(@"..\..\TestData\Songs1\");
            Assert.AreEqual(5, noSongs);
            Assert.AreEqual(5, playlist.Size);
        }

        [TestMethod]
        public void AddingSong()
        {
            Playlist playlist = Playlist.Instance();
            playlist.Clear();
            int noSongs = playlist.AddSongs(@"..\..\TestData\Songs1\mp3Sample.mp3");
            Assert.AreEqual(1, noSongs);
            Assert.AreEqual(1, playlist.Size);
        }

        [TestMethod]
        public void BadFormat()
        {
            Playlist playlist = Playlist.Instance();
            playlist.Clear();
            int noSongs = playlist.AddSongs(@"..\..\TestData\Songs1\info.txt");
            Assert.AreEqual(0, noSongs);
            Assert.AreEqual(0, playlist.Size);
        }

        [TestMethod]
        public void NullPath()
        {
            Playlist playlist = Playlist.Instance();
            playlist.Clear();
            int noSongs = playlist.AddSongs("");
            Assert.AreEqual(0, noSongs);
            Assert.AreEqual(0, playlist.Size);
        }

        [TestMethod]
        public void RemoveSong()
        {
            Playlist playlist = Playlist.Instance();
            playlist.Clear();
            int noSongs = playlist.AddSongs(@"..\..\TestData\Songs1\");
            Assert.AreEqual(true, playlist.RemoveSong(0));
            Assert.AreEqual(noSongs - 1, playlist.Size);
        }

        [TestMethod]
        public void OutOfRangeRemoval()
        {
            Playlist playlist = Playlist.Instance();
            playlist.Clear();
            playlist.AddSongs(@"..\..\TestData\Songs1\");
            Assert.AreEqual(false, playlist.RemoveSong(-1));
            Assert.AreEqual(false, playlist.RemoveSong(playlist.Size));
        }

        [TestMethod]
        public void GettingSong()
        {
            Playlist playlist = Playlist.Instance();
            playlist.Clear();
            playlist.AddSongs(@"..\..\TestData\Songs1\");
            for(int i=0; i<playlist.Size; ++i)
            {
                Assert.IsNotNull(playlist.GetSong(i));
                Assert.AreEqual(playlist.GetSong(i).GetType(), typeof(AudioFile));
            }
        }

        [TestMethod]
        public void OutOfRangeGet()
        {
            Playlist playlist = Playlist.Instance();
            playlist.Clear();
            playlist.AddSongs(@"..\..\TestData\Songs1\");

            Assert.AreEqual(null, playlist.GetSong(-1));
            Assert.AreEqual(null, playlist.GetSong(playlist.Size));
        }
    }
}
