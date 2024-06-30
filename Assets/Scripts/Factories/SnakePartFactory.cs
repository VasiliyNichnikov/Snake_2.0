#nullable enable
using Data;
using Snake;
using UnityEngine;
using Weapons;

namespace Factories
{
    public class SnakePartFactory
    {
        private readonly SnakePartController _snakePartPrefab;
        private readonly Transform _snakePartParent;

        public SnakePartFactory(SnakePartController snakePartPrefab, Transform snakePartParent)
        {
            _snakePartPrefab = snakePartPrefab;
            _snakePartParent = snakePartParent;
        }
        
        public ISnakePartController Create(SnakePartData data, 
            IChoosingEnemyTarget choosingEnemyTarget, 
            IChoosingWeapon choosingWeapon,
            IChoosingAlly choosingAlly)
        {
            var view = Object.Instantiate(_snakePartPrefab, _snakePartParent , false);
            view.Init(data, choosingEnemyTarget, choosingWeapon, choosingAlly);
            return view;
        }
    }
}