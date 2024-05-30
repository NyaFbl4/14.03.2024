using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour 
                        ,IGameFinishListener ,IGameStartListener
                        ,IGamePauseListener  ,IGameResumeListener
    {
        [SerializeField] private GameObject _character; 
        [SerializeField] private GameManager _gameManager;

        private void Awake()
        {
            IGameListener.Register(this);
        }
        
        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void OnCharacterDeath(GameObject _) => this._gameManager.FinishGame();

        public void OnStartGame()
        {
            this._character.GetComponent<HitPointsComponent>().OnHpChange += this.OnCharacterDeath;
        }

        public void OnFinishGame()
        {
            this._character.GetComponent<HitPointsComponent>().OnHpChange -= this.OnCharacterDeath;
        }

        public void OnPauseGame()
        {
            
        }

        public void OnResumeGame()
        {
            
        }
        
    }
}