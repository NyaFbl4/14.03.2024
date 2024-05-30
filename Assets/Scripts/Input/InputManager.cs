using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public event Action<float> OnMove;
        public event Action OnAttack;

        private void Update()
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