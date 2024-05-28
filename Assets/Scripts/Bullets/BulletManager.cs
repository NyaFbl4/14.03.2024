using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletManager : MonoBehaviour
    {
        [SerializeField] private int _initialCount = 50;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private BulletPool _bulletPool;

        private void Awake()
        {
            _bulletPool.Initialize(_initialCount);
        }

        private void FixedUpdate()
        {
            _bulletPool.UpdatePool();
        }

        public void FlyBulletByArgs(BulletManager.Args args)
        {
            var bullet = _bulletPool.GetBullet();

            bullet.transform.SetParent(_worldTransform);
            bullet.SetPosition(args.position);
            bullet.SetColor(args.color);
            bullet.SetPhysicsLayer(args.physicsLayer);
            bullet.damage = args.damage;
            bullet.isPlayer = args.isPlayer;
            bullet.SetVelocity(args.velocity);

            bullet.OnCollisionEntered += OnBulletCollision;
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            bullet.OnCollisionEntered -= OnBulletCollision;
            _bulletPool.RemoveBullet(bullet);
        }

        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }
    }
}
