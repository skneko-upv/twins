using System;
using System.Collections.Generic;
using System.Text;

namespace Twins.Utils
{
    public class AudioPlayer
    {
        public Plugin.SimpleAudioPlayer.ISimpleAudioPlayer Player { get; }

        private static double Volume = 100.0;

        public AudioPlayer() { Player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current; }

        public void ChangeVolume(double newVolume) 
        {
            Volume = newVolume / 100;
            Player.Volume = Volume;
        }

        public double GetVolume() { return Player.Volume; }

        public void LoadSong(String songName)
        {
            Player.Load("Sounds\\" + songName);
            Player.Volume = Volume;
            Player.Loop = true;
            Player.Play();
        }
    }
}

    
