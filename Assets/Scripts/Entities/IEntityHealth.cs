#nullable enable
using System;

namespace Entities
{
    public interface IEntityHealth
    {
        event Action? OnDied; 
        
        bool IsDied { get; }
        
        void TakeDamage(int damage);
    }
}