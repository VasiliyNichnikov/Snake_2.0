#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Data;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MapGeneratorContext", menuName = "SNT/MapGeneratorContext", order = 0)]
    public class MapGeneratorContext : ScriptableObject
    {
        [Serializable]
        private struct Wave
        {
            [Header("Кол-во врагов в волне")] 
            public int NumberEnemies;

            [Header("Кол-во союзников в волне")] 
            public int NumberAllies;

            [Header("Одновременное кол-во врагов в волне")]
            public int NumberOfEnemiesAtSameTime;

            [Header("Одновременное кол-во союзников в волне")]
            public int NumberOfAlliesAtSameTime;

            [Header("Одноврменное кол-во оружия в волне")]
            public int NumberOfWeaponsAtSameTime;
            
            [Header("Характеристики для зомби в волне")]
            public ReadOnlyCollection<ZombiesConfig.ZombieData> ZombiesData;
        }
        
        [SerializeField]
        private List<Transform> _alliesTransforms = null!;

        [SerializeField] 
        private List<Transform> _enemiesTransforms = null!;

        [SerializeField] 
        private List<Transform> _weaponsTransforms = null!;

        [SerializeField] 
        private Wave[] _waves = null!;
        
        public MapGeneratorData GetData()
        {
            var waves = new List<WaveData>();
            foreach (var wave in _waves)
            {
                var enemiesData = wave.ZombiesData.Select(ZombiesConfig.Convert).ToList();
                var data = new WaveData(wave.NumberEnemies, wave.NumberAllies, wave.NumberOfEnemiesAtSameTime,
                    wave.NumberOfAlliesAtSameTime, wave.NumberOfWeaponsAtSameTime, enemiesData.AsReadOnly());
                waves.Add(data);
            }

            return new MapGeneratorData(
                _alliesTransforms.AsReadOnly(), 
                _enemiesTransforms.AsReadOnly(), 
                _weaponsTransforms.AsReadOnly(), 
                waves.AsReadOnly());
        }
    }
}