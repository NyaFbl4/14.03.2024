using UnityEngine;

namespace ShootEmUp
{
    public sealed class MainBulletSystem : MonoBehaviour
    {
        [SerializeField] private int initialCount = 50;

        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;
        [SerializeField] private Transform worldTransform;

        [SerializeField] private BulletPool bulletPool;

        private void Awake()
        {
            bulletPool.Initialize(initialCount);
        }

        private void FixedUpdate()
        {
            bulletPool.UpdatePool();
        }

        public void FlyBulletByArgs(MainBulletSystem.Args args)
        {
            var bullet = bulletPool.GetBullet();

            bullet.transform.SetParent(worldTransform);
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
            bulletPool.RemoveBullet(bullet);
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
