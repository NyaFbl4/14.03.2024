using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject _character; 
        [SerializeField] private GameManager _gameManager;
        //[SerializeField] private BulletSystem _bulletSystem;
        //[SerializeField] private BulletConfig _bulletConfig;

        //public bool _fireRequired;

        private void Start()
        {

        }

        private void OnEnable()
        {
            this._character.GetComponent<HitPointsComponent>().hpEmpty += this.OnCharacterDeath;
        }

        private void OnDisable()
        {
            this._character.GetComponent<HitPointsComponent>().hpEmpty -= this.OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => this._gameManager.FinishGame();

        /*
        private void FixedUpdate()
        {
            
            if (this._fireRequired)
            {
                this.OnFlyBullet();
                this._fireRequired = false;
            }
            
        }

        private void OnFlyBullet()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed
            });
        }
        */
    }
}