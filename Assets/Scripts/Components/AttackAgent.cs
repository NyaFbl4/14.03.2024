using UnityEngine;

namespace ShootEmUp
{
    public class AttackAgent : MonoBehaviour
    {
        [SerializeField] private GameObject _character;
        [SerializeField] private MainBulletSystem _bulletMainSystem;
        [SerializeField] private BulletConfig _bulletConfig;

        private bool _fireRequired;

        public void Attack()
        {
            var weapon = this._character.GetComponent<WeaponComponent>();
            _fireRequired = true;

            if (_fireRequired)
            {
                _bulletMainSystem.FlyBulletByArgs(new MainBulletSystem.Args
                {
                    position = weapon.Position,
                    velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed,
                    color = this._bulletConfig.color,
                    physicsLayer = (int)this._bulletConfig.physicsLayer,
                    damage = this._bulletConfig.damage,
                    isPlayer = true
                });
                
                _fireRequired = false;
            }
        }
    }
}
