using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;
        [SerializeField] private LevelBounds levelBounds;

        private readonly Queue<Bullet> bulletPool = new();
        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cache = new();

        public void Initialize(int initialCount)
        {
            for (var i = 0; i < initialCount; i++)
            {
                var bullet = Instantiate(this.prefab, this.container);
                bulletPool.Enqueue(bullet);
            }

            Debug.Log(bulletPool.Count.ToString());
        }
        public void RemoveBullet(Bullet bullet)
        {
            if (this.activeBullets.Remove(bullet))
            {
                bullet.transform.SetParent(this.container);
                bulletPool.Enqueue(bullet);
            }
        }
        public void UpdatePool()
        {
            cache.Clear();
            cache.AddRange(activeBullets);

            foreach (var bullet in cache)
            {
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }
        public Bullet GetBullet()
        {
            if (bulletPool.TryDequeue(out var bullet))
            {
                bullet.gameObject.SetActive(true);
            }
            else
            {
                bullet = Instantiate(prefab, container);
            }

            activeBullets.Add(bullet);
            return bullet;
        }
    }
}
