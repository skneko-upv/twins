using System;
using System.Collections.Generic;
using System.Text;

namespace Twins.Utils
{
    public class AudioPlayer
    {
        public Plugin.SimpleAudioPlayer.ISimpleAudioPlayer Player { get; }

        public AudioPlayer() { Player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current; }

        public void LoadSong(String songName)
        {
            Player.Load("Sounds\\" + songName);
            Player.Play();
        }
    }
}

    
