using CustomTrial.Classes;
using System;
using System.Collections.Generic;

namespace CustomTrial
{
    [Serializable]
    public class GlobalSettings
    {
        private List<Wave> _waves = new();
        private int _geoReward;

        public List<Wave> Waves => _waves;
        public int GeoReward => _geoReward;

        public void SetGeoReward(int geo)
        {
            _geoReward = geo;
        }

        public void AddWave(Wave wave)
        {
            _waves.Add(wave);
        }
    }
}
