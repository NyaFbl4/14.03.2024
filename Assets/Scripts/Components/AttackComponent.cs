using UnityEngine;

namespace ShootEmUp
{
    public sealed class AttackComponent : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private WeaponComponent _weapon;
        [SerializeField] private BulletManager _bulletManager;
        [SerializeField] private BulletConfig _bulletConfig;

        private bool _fireRequired;

        private void Start()
        {
            _inputManager.OnAttack += Attack;
        }

        public void Attack()
        {
            _fireRequired = true;

            if (_fireRequired)
            {
                _bulletManager.FlyBulletByArgs(new BulletManager.Args
                {
                    position = _weapon.Position,
                    velocity = _weapon.Rotation * Vector3.up * this._bulletConfig.speed,
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
