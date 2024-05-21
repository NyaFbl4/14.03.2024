using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField]
        private int initialCount = 50;

        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;

        private readonly Queue<Bullet> m_bulletPool = new();

        private void Awake()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var bullet = Instantiate(this.prefab, this.container);
                this.m_bulletPool.Enqueue(bullet);
            }
        }

        public Bullet GetBullet()
        {
            return this.m_bulletPool.Dequeue();
        }

        public void ReturnBullet(Bullet bullet)
        {
            bullet.transform.SetParent(this.container);
            this.m_bulletPool.Enqueue(bullet);
        }
    }
}
