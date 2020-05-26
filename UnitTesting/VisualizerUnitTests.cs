using System;
using System.Windows.Forms;
using AudioPlayerLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;


// File: VisualizerUnitTests.cs 
// Author: Cana Andrei - https://github.com/Aythriel
// Done: May 2020
// Purpose: An MSUnit testing class designed to help assure the AudioVisualizer module of the music player works correctly.

namespace UnitTesting
{
    [TestClass]
    public class VisualizerUnitTests
    {
        [TestMethod]
        public void AudioVisualizerConstructorGoodRefs()
        {
            ProgressBar progBar = new ProgressBar();
            PictureBox picBox = new PictureBox();
            Timer timer = new Timer();

            AudioVisualizer audioVisualizer = new AudioVisualizer(progBar, picBox, timer);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AudioVisualizerConstructorBadRefs()
        {
            AudioVisualizer audioVisualizer = new AudioVisualizer(null, null, null);
        }

        [TestMethod]
        public void SetSongGoodReference()
        {
            AudioFile audioFile = new AudioFile(".//TestResources//test2.mp3");
            ProgressBar progBar = new ProgressBar();
            PictureBox picBox = new PictureBox();
            Timer timer = new Timer();

            AudioVisualizer audioVisualizer = new AudioVisualizer(progBar, picBox, timer);
            audioVisualizer.SetSong(audioFile);
            Assert.IsTrue(timer.Enabled, "Timer should be enabled when a good audio file is sent.");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SetSongBadReference()
        {
            AudioFile audioFile = new AudioFile(".//TestResources//test2.mp3");
            ProgressBar progBar = new ProgressBar();
            PictureBox picBox = new PictureBox();
            Timer timer = new Timer();

            AudioVisualizer audioVisualizer = new AudioVisualizer(progBar, picBox, timer);
            audioVisualizer.SetSong(null);
        }


        [TestMethod]
        public void SetSongGeneratedBitmap()
        {
            AudioFile audioFile = new AudioFile(".//TestResources//test2.mp3");
            ProgressBar progBar = new ProgressBar();
            PictureBox picBox = new PictureBox();
            Timer timer = new Timer();

            AudioVisualizer audioVisualizer = new AudioVisualizer(progBar, picBox, timer);
            audioVisualizer.SetSong(audioFile);
            Assert.IsNotNull(picBox.Image, "Image should be generated and applied to picture box.");
        }

        [TestMethod]
        public void checkPause()
        {
            ProgressBar progBar = new ProgressBar();
            PictureBox picBox = new PictureBox();
            Timer timer = new Timer();

            AudioVisualizer audioVisualizer = new AudioVisualizer(progBar, picBox, timer);
            audioVisualizer.OnPauseEvent(null, null); // event args aren't relevant here
            Assert.IsFalse(timer.Enabled, "Timer should be disabled by onPause() events");
        }

        [TestMethod]
        public void checkPlay()
        {
            ProgressBar progBar = new ProgressBar();
            PictureBox picBox = new PictureBox();
            Timer timer = new Timer();

            AudioVisualizer audioVisualizer = new AudioVisualizer(progBar, picBox, timer);
            audioVisualizer.OnPauseEvent(null, null); // event args aren't relevant here
            Assert.IsTrue(timer.Enabled, "Timer should be enabled by onPlay() events");
        }

    }
}
