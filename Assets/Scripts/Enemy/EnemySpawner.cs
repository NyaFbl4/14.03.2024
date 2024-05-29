using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private EnemyPosition _enemyPosition;
        [SerializeField] private GameObject _character;
        [SerializeField] private Transform _worldTransform;

        [Header("Pool")]
        [SerializeField] private int _enemyCount;
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _prefab;

        private readonly Queue<GameObject> _enemyPool = new Queue<GameObject>();
        
        private void Awake()
        {
            for (var i = 0; i < _enemyCount; i++)
            {
                var enemy = Instantiate(this._prefab, this._container);
                this._enemyPool.Enqueue(enemy);
            }
        }

        public bool TrySpawnEnemy(out GameObject enemy)
        {
            if (!this._enemyPool.TryDequeue(out enemy))
            {
                return false;
            }

            enemy.transform.SetParent(this._worldTransform);

            var spawnPosition = this._enemyPosition.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
    
            var attackPosition = this._enemyPosition.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            enemy.GetComponent<EnemyAttackAgent>().SetTarget(this._character);
            return true;
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this._container);
            this._enemyPool.Enqueue(enemy);
        }
    }
}