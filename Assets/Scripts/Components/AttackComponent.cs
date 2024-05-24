using UnityEngine;

namespace ShootEmUp
{
    public class AttackComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _character;
        [SerializeField] private BulletManager _bulletManager;
        [SerializeField] private BulletConfig _bulletConfig;

        private bool _fireRequired;

        public void Attack()
        {
            var weapon = this._character.GetComponent<WeaponComponent>();
            _fireRequired = true;

            if (_fireRequired)
            {
                _bulletManager.FlyBulletByArgs(new BulletManager.Args
                {
                    position = weapon.Position,
                    velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed,
                    color = this._bulletConfig.color,
                    physicsLayer = (int)PhysicsLayer.CHARACTER,
                    damage = this._bulletConfig.damage,
                    isPlayer = true
                });

                _fireRequired = false;
            }
        }
    }
}
