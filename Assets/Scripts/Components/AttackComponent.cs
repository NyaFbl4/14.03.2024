using UnityEngine;

namespace ShootEmUp
{
    public class AttackComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _character;
        [SerializeField] private MainBulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;

        private bool _fireRequired;

        public void Attack()
        {
            var weapon = this._character.GetComponent<WeaponComponent>();
            _fireRequired = true;

            if (_fireRequired)
            {
                _bulletSystem.FlyBulletByArgs(new MainBulletSystem.Args
                {
                    isPlayer = true,
                    //physicsLayer = (int)this._bulletConfig.physicsLayer,
                    physicsLayer = (int)PhysicsLayer.CHARACTER,
                    color = this._bulletConfig.color,
                    damage = this._bulletConfig.damage,
                    position = weapon.Position,
                    velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed
                });
                /*
                _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
                {
                    isPlayer = true,
                    //physicsLayer = (int)this._bulletConfig.physicsLayer,
                    physicsLayer = (int)PhysicsLayer.CHARACTER,
                    color = this._bulletConfig.color,
                    damage = this._bulletConfig.damage,
                    position = weapon.Position,
                    velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed
                });
                */
                _fireRequired = false;
            }
        }
    }
}
