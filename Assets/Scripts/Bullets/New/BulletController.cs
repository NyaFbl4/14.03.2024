using UnityEngine;

namespace ShootEmUp
{
    public class BulletController : MonoBehaviour
    {
        public void FlyBullet(Bullet bullet, Args args)
        {
            //bullet.transform.SetParent(args.worldTransform);
            bullet.SetPosition(args.position);
            bullet.SetColor(args.color);
            bullet.SetPhysicsLayer(args.physicsLayer);
            bullet.damage = args.damage;
            bullet.isPlayer = args.isPlayer;
            bullet.SetVelocity(args.velocity);
        }
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
