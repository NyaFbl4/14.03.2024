using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ShootEmUp
{
    public enum GameState
    {
        Start,
        Finish,
        Pause,
        Resume,
        Off
    }
    
    public class GameManager : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private GameState gameState;
        
        private List<IGameListener> gameListeners = new();
        
        private void Awake()
        {
            gameState = GameState.Off;
            
            IGameListener.onRegister += AddListener;
        }

        private void OnDestroy()
        {
            gameState = GameState.Finish;
            IGameListener.onRegister -= AddListener;
        }

        private void AddListener(IGameListener gameListener)
        {
            gameListeners.Add(gameListener);
        }
        
        [Button]
        public void StartGame()
        {
            foreach (var gameListener in gameListeners)
            {
                if (gameListener is IGameStartListener gameStartListener)
                {
                    gameStartListener.OnStartGame();
                }
            }

            gameState = GameState.Start;
            Debug.Log("OnStartGame");
        }
        
        [Button]
        public void FinishGame()
        {
            foreach (var gameListener in gameListeners)
            {
                if (gameListener is IGameFinishListener gameFinishListener)
                {
                    gameFinishListener.OnFinishGame();
                }
            }
            
            gameState = GameState.Finish;
            Debug.Log("OnFinishGame");
        }
        
        [Button]
        public void PauseGame()
        {
            foreach (var gameListener in gameListeners)
            {
                if (gameListener is IGamePauseListener gamePauseListener)
                {
                    gamePauseListener.OnPauseGame();
                }
            }
            
            gameState = GameState.Pause;
            Debug.Log("OnPauseGame");
        }   
        
        [Button]
        public void ResumeGame()
        {
            foreach (var gameListener in gameListeners)
            {
                if (gameListener is IGameResumeListener gameResumeListener)
                {
                    gameResumeListener.OnResumeGame();
                }
            }
            gameState = GameState.Resume;
            Debug.Log("OnResumeGame");
        }
        
        public void FFinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}