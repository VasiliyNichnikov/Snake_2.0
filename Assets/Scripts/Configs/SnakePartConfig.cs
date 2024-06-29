#nullable enable
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "SnakePartConfig", menuName = "SNT/SnakePartConfig", order = 0)]
    public class SnakePartConfig : ScriptableObject
    {
        [Header("Скорость части змеи")]
        public float Speed;

        [Header("Скорость на поворотах")] 
        public float AngularSpeed;

        [Header("Дистанция остановки")] 
        public float StoppingDistance;

        [Header("Максимальное ускорение")] 
        public float Acceleration;
    }
}