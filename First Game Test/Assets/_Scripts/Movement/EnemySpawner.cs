using UnityEngine;

namespace TopDown.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Prefab del Enemigo")]
        [SerializeField] private GameObject enemyPrefab;

        [Header("Área de Spawn")]
        [SerializeField] private Rect spawnArea;

        [Header("Spawn Settings")]
        [SerializeField] private float spawnInterval = 3f;
        [SerializeField] private int maxEnemies = 10;

        private int currentEnemies = 0;

        private void Start()
        {
            InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
        }

        private void SpawnEnemy()
        {
            if (currentEnemies >= maxEnemies) return;

            Vector3 spawnPos = new Vector3(
                Random.Range(spawnArea.xMin, spawnArea.xMax),
                Random.Range(spawnArea.yMin, spawnArea.yMax),
                0);

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            currentEnemies++;
        }
    }
}
