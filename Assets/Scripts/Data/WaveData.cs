using System.Collections.ObjectModel;

namespace Data
{
    public readonly struct WaveData
    {
        public readonly int NumberEnemies;
        public readonly int NumberAllies;
        public readonly int NumberOfEnemiesAtSameTime;
        public readonly int NumberOfAlliesAtSameTime;
        public readonly int NumberOfWeaponsAtSameTime;
        
        public readonly ReadOnlyCollection<EnemyData> EnemiesData;

        public WaveData(
            int numberEnemies, 
            int numberAllies,
            int numberOfEnemiesAtSameTime,
            int numberOfAlliesAtSameTime,
            int numberOfWeaponsAtSameTime,
            
            ReadOnlyCollection<EnemyData> enemiesData)
        {
            NumberEnemies = numberEnemies;
            NumberAllies = numberAllies;
            EnemiesData = enemiesData;

            NumberOfEnemiesAtSameTime = numberOfEnemiesAtSameTime;
            NumberOfAlliesAtSameTime = numberOfAlliesAtSameTime;
            NumberOfWeaponsAtSameTime = numberOfWeaponsAtSameTime;
        }
    }
}