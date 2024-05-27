using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnHpChange;
        
        [SerializeField] private int _hitPoints;
        
        public bool IsHitPointsExists() 
        {
            return this._hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            this._hitPoints -= damage;
            if (this._hitPoints <= 0)
            {
                this.OnHpChange?.Invoke(this.gameObject);
            }
        }
    }
}