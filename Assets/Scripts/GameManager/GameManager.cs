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
        
        private readonly List<IGameListener> _gameListeners = new();
        private readonly List<IGameUpdateListener> _gameUpdateListeners = new();
        private readonly List<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();
        private readonly List<IGameLateUpdateListener> _gameLateUpdateListeners = new();
        
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

        private void Update()
        {
            if (gameState != GameState.Start)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _gameUpdateListeners.Count; i++)
            {
                _gameUpdateListeners[i].OnUpdate(deltaTime);
            }
        }
        private void FixedUpdate()
        {
            if (gameState != GameState.Start)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _gameFixedUpdateListeners.Count; i++)
            {
                _gameFixedUpdateListeners[i].OnFixedUpdate(deltaTime);
            }
        }
        private void LateUpdate()
        {
            if (gameState != GameState.Start)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _gameLateUpdateListeners.Count; i++)
            {
                _gameLateUpdateListeners[i].OnLateUpdate(deltaTime);
            }
        }

        private void AddListener(IGameListener gameListener)
        {
            _gameListeners.Add(gameListener);
            
            if (gameListener is IGameUpdateListener gameUpdateListener)
            {
                _gameUpdateListeners.Add(gameUpdateListener);
            }   
            
            if (gameListener is IGameFixedUpdateListener gameFixedUpdateListener)
            {
                _gameFixedUpdateListeners.Add(gameFixedUpdateListener);
            }
            
            if (gameListener is IGameLateUpdateListener gameLateUpdateListener)
            {
                _gameLateUpdateListeners.Add(gameLateUpdateListener);
            }
        }
        
        [Button]
        public void StartGame()
        {
            foreach (var gameListener in _gameListeners)
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
            foreach (var gameListener in _gameListeners)
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
            foreach (var gameListener in _gameListeners)
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
            foreach (var gameListener in _gameListeners)
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