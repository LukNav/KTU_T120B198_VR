using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    [Min(1)]
    int maxEnemyCount;
    int currentEnemyCount;
    [SerializeField]
    [Min(1)]
    int enemyInterval;

    // Start is called before the first frame update
    void Start()
    {
        if(maxEnemyCount > 0)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        var newEnemy = Instantiate(enemyPrefab, gameObject.transform);
        currentEnemyCount++;
        if(currentEnemyCount < maxEnemyCount)
            Invoke("SpawnEnemy", enemyInterval);
    }
}
