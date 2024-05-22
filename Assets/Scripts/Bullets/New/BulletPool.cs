using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private LevelBounds levelBounds;
        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;

        private readonly Queue<Bullet> pool = new();
        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cache = new();

        private void Awake()
        {

        }

        public BulletPool(Transform container, Bullet prefab)
        {
            this.container = container;
            this.prefab = prefab;
        }

        public void Initialize(int initialCount)
        {
            for (var i = 0; i < initialCount; i++)
            {
                var bullet = Instantiate(this.prefab, this.container);
                pool.Enqueue(bullet);
            }

            Debug.Log(pool.Count.ToString());
        }

        public Bullet GetBullet()
        {
            if (pool.TryDequeue(out var bullet))
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

        public void RemoveBullet(Bullet bullet)
        {
            if (this.activeBullets.Remove(bullet))
            {
                bullet.transform.SetParent(this.container);
                pool.Enqueue(bullet);
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
    }
}
