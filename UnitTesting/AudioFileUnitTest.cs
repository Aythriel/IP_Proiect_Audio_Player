// ================================
// File: AudioFileUnitTest.cs
// Author: Teodorovici Silviu
// Purpose: A unit testing class for running tests on the AudioFile class
// ================================

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AudioPlayerLib;

namespace UnitTesting
{
    [TestClass]
    public class AudioFileUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NullPath()
        {
            AudioFile audioFile = new AudioFile(null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NonFilePath()
        {
            AudioFile audioFile = new AudioFile(@"IP_Proiect_Audio_Player\TestSamples");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BadFormat()
        {
            AudioFile audioFile = new AudioFile(@"IP_Proiect_Audio_Player\TestSamples\test.txt");
        }

        [TestMethod]
        public void Properties()
        {
            AudioFile audioFile = new AudioFile(@"IP_Proiect_Audio_Player\TestSamples\test.mp3");
            Assert.AreEqual(@"IP_Proiect_Audio_Player\TestSamples\test.mp3", audioFile.Path);
            Assert.AreEqual(".mp3", audioFile.Format);
            Assert.AreEqual("test", audioFile.Name);
        }
    }
}
