using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class MainBulletSystem : MonoBehaviour
    {
        [SerializeField] private BulletPool bulletPool;
        [SerializeField] private BulletManager bulletManager;
        [SerializeField] private CollisionHandler bulletCollisionHandler;
        [SerializeField] public Transform worldTransform;

        public void FlyBulletByArgs(Args args)
        {
            var bullet = this.bulletPool.GetBullet();
            // Initialize bullet properties
            bullet.transform.SetParent(this.worldTransform);
            bullet.SetPosition(args.position);
            bullet.SetColor(args.color);
            bullet.SetPhysicsLayer(args.physicsLayer);
            bullet.damage = args.damage;
            bullet.isPlayer = args.isPlayer;
            bullet.SetVelocity(args.velocity);

            this.bulletManager.AddBullet(bullet);
            bullet.OnCollisionEntered += this.bulletCollisionHandler.OnBulletCollision;
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
