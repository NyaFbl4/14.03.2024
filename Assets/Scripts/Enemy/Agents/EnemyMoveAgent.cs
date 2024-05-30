using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached
        {
            get { return this._isReached; }
        }
        
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed = 5.0f;

        private Vector2 _destination;
        private float _distance = 0.25f;

        private bool _isReached;

        public void SetDestination(Vector2 endPoint)
        {
            this._destination = endPoint;
            this._isReached = false;
        }

        private void FixedUpdate()
        {
            if (this._isReached)
            {
                return;
            }
            
            var vector = this._destination - (Vector2) this.transform.position;
            if (vector.magnitude <= _distance)
            {
                this._isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            this.MoveByRigidbodyVelocity(direction);
        }
        
        private void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = this._rigidbody2D.position + vector * this._speed;
            this._rigidbody2D.MovePosition(nextPosition);
        }
    }
}