using UnityEngine;
using System;

namespace ShootEmUp
{
    public interface IBullet
    {
        event Action<IBullet, Collision2D> OnCollisionEntered;
        void SetVelocity(Vector2 velocity);
        void SetPhysicsLayer(int physicsLayer);
        void SetPosition(Vector3 position);
        void SetColor(Color color);
    }
}
