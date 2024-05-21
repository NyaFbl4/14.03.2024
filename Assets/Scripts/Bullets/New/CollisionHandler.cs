using UnityEngine;

namespace ShootEmUp
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] private LevelBounds levelBounds;

        public void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            //this.RemoveBullet(bullet);

            BulletUtils.DealDamage(bullet, collision.gameObject);
            // Remove bullet from BulletManager
            var bulletManager = GetComponent<BulletManager>();
            bulletManager.RemoveBullet(bullet);
        }
    }
}
