using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject aggressiveEnemyPrefab;

    [SerializeField]
    [Min(1)]
    private int maxEnemyCount;

    private int currentEnemyCount;

    [SerializeField]
    [Min(1)]
    private int enemyInterval;

    [SerializeField]
    [Min(2)]
    private int aggressiveEnemyCount;

    // Start is called before the first frame update
    void Awake()
    {
        if(maxEnemyCount > 0)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if(maxEnemyCount > 1)
        {
            switch (currentEnemyCount % aggressiveEnemyCount == 0 && aggressiveEnemyCount > 1)
            {
                case false:
                    Instantiate(enemyPrefab, gameObject.transform);
                    break;
                case true:
                    Instantiate(aggressiveEnemyPrefab, gameObject.transform);
                    break;
            }
        }
        else
        {
            Instantiate(enemyPrefab, gameObject.transform);
        }
        currentEnemyCount++;
        if(currentEnemyCount < maxEnemyCount)
            Invoke("SpawnEnemy", enemyInterval);
    }
}
