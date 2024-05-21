using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private LevelBounds levelBounds;

        private readonly HashSet<Bullet> m_activeBullets = new();

        public void AddBullet(Bullet bullet)
        {
            this.m_activeBullets.Add(bullet);
        }

        public void RemoveBullet(Bullet bullet)
        {
            this.m_activeBullets.Remove(bullet);
        }

        private void FixedUpdate()
        {
            foreach (var bullet in m_activeBullets)
            {
                if (!levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }
    }
}