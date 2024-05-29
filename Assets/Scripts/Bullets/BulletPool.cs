using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletPool : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private LevelBounds _levelBounds;

        private readonly Queue<Bullet> _bulletPool = new Queue<Bullet>();
        private readonly HashSet<Bullet> _activeBullets = new HashSet<Bullet>();
        private readonly List<Bullet> _cache = new List<Bullet>();

        public void Initialize(int initialCount)
        {
            for (var i = 0; i < initialCount; i++)
            {
                var bullet = Instantiate(this._prefab, this._container);
                _bulletPool.Enqueue(bullet);
            }

            Debug.Log(_bulletPool.Count.ToString());
        }
        public void RemoveBullet(Bullet bullet)
        {
            if (this._activeBullets.Remove(bullet))
            {
                bullet.transform.SetParent(this._container);
                _bulletPool.Enqueue(bullet);
            }
        }
        public void UpdatePool()
        {
            _cache.Clear();
            _cache.AddRange(_activeBullets);

            foreach (var bullet in _cache)
            {
                if (!this._levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }
        public Bullet GetBullet()
        {
            if (_bulletPool.TryDequeue(out var bullet))
            {
                bullet.gameObject.SetActive(true);
            }
            else
            {
                bullet = Instantiate(_prefab, _container);
            }

            _activeBullets.Add(bullet);
            return bullet;
        }
    }
}
