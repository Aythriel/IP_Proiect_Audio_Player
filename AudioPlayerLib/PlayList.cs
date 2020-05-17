using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayerLib
{
    class PlayList
    {
        private static PlayList _instance;
        private static Object _lock;
        private List<AudioFile> _songs;
        
        private PlayList()
        {
            _songs = new List<AudioFile>();
        }


        public static PlayList getInstance()
        {
            lock (_lock)
            {
                if (null == _instance)
                {
                    _instance = new PlayList();
                }
            }
       
            return _instance;
        }
    }
}
