using System;
using System.Collections.Generic;
using System.Text;

namespace Twins.Utils
{
    public class AudioPlayer
    {
        public Plugin.SimpleAudioPlayer.ISimpleAudioPlayer Player { get; }
        public string CurrentSong { get; private set; } = "";

        private static double Volume = 1.0;

        //Arreglar bug que se stackean las canciones
        public AudioPlayer() { Player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer(); }

        public void ChangeVolume(double newVolume) 
        {
            Volume = newVolume / 100;
            Player.Volume = Volume;
        }

        public double GetVolume() { return Player.Volume; }

        public void LoadSong(string songName)
        {
            if (songName != CurrentSong)
            {
                Player.Load("Sounds\\" + songName);
                Player.Volume = Volume;
                Player.Loop = true;
                CurrentSong = songName;
       
                Player.Play();
            }
        }

        public void LoadEffect(string effectName)
        {
            Player.Load("Sounds\\" + effectName);
        }

        public void Play() { Player.Play(); }

        public void Pause() { Player.Pause(); }
    }
}

    
