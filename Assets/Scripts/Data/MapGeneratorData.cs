#nullable enable
using System.Collections.ObjectModel;
using UnityEngine;

namespace Data
{
    public readonly struct MapGeneratorData
    {
        public readonly ReadOnlyCollection<Transform> AlliesTransforms;
        public readonly ReadOnlyCollection<Transform> EnemiesTransforms;
        public readonly ReadOnlyCollection<Transform> WeaponsTransforms;
        public readonly ReadOnlyCollection<WaveData> Waves;

        public MapGeneratorData(ReadOnlyCollection<Transform> alliesTransforms, 
            ReadOnlyCollection<Transform> enemiesTransforms, 
            ReadOnlyCollection<Transform> weaponsTransforms,
            ReadOnlyCollection<WaveData> waves)
        {
            AlliesTransforms = alliesTransforms;
            EnemiesTransforms = enemiesTransforms;
            WeaponsTransforms = weaponsTransforms;
            Waves = waves;
        }
    }
}