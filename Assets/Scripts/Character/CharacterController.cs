using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour 
                        ,IGameFinishListener ,IGameStartListener
                        ,IGamePauseListener  ,IGameResumeListener
    {
        [SerializeField] private GameObject _character; 
        [SerializeField] private GameManager _gameManager;

        private void OnEnable()
        {
            this._character.GetComponent<HitPointsComponent>().OnHpChange += this.OnCharacterDeath;
        }

        private void OnDisable()
        {
            this._character.GetComponent<HitPointsComponent>().OnHpChange -= this.OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _) => this._gameManager.FinishGame();

        public void OnStartGame()
        {
            
        }

        public void OnFinishGame()
        {
            
        }

        public void OnPauseGame()
        {
            
        }

        public void OnResumeGame()
        {
            
        }
        
    }
}