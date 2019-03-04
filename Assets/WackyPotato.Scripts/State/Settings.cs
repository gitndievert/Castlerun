using System;

namespace WackyPotato.Scripts.State
{
    [Serializable]
    public class Settings
    {
        public bool Written { get; set; }
        public bool Reviewed { get; set; }
        public int WinCount { get; set; }
        public float MusicVolume { get; set; }
        public float SoundVolume { get; set; }
    }
}
