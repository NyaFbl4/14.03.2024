using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour, 
        IGameStartListener, IGameFinishListener, 
        IGamePauseListener, IGameResumeListener
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed = 5.0f;

        private void Start()
        {
            IGameListener.Register(this);
        }

        private void OnEnable()
        {
            //_inputManager.OnMove += OnMove;
        }

        private void OnDisable()
        {
            //_inputManager.OnMove -= OnMove;
        }
        
        public void OnStartGame()
        {
            this._inputManager.OnMove += this.OnMove;
            Debug.Log("OnStartGame");
        }

        public void OnFinishGame()
        {
            this._inputManager.OnMove -= this.OnMove;
            Debug.Log("OnFinishGame");
        }

        public void OnPauseGame()
        {
            //enabled = false;
        }

        public void OnResumeGame()
        {
            //enabled = true;
        }
        
        private void OnMove(float direction)
        {
            /*
            if (!enabled)
            {
                return;
            }
            */
            
            MoveByRigidbodyVelocity(new Vector2(direction, 0) * Time.fixedDeltaTime);
        }

        private void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = this._rigidbody2D.position + vector * this._speed;
            this._rigidbody2D.MovePosition(nextPosition);
        }
    }
}