using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "SnakeData", menuName = "SNT/SnakeData", order = 0)]
    public class SnakeConfig : ScriptableObject
    {
        public int MaximumAttackDistance;
    }
}