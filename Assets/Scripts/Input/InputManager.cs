using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour, IGameUpdateListener
    {
        public event Action<float> OnMove;
        public event Action OnAttack;

        private void Start()
        {
            IGameListener.Register(this);
        }
        public void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnAttack();
            }

            float direction = 0;
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = 1;
            }
            else
            {
                direction = 0;
            }
            
            OnMove?.Invoke(direction);
        }
    }
}