using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject enemyPrefab;
    [SerializeField]
    public GameObject aggressiveEnemyPrefab;

    private WaveSettings waveSettings;
    private Coroutine spawnEnemyCoroutine;
    
    private int currentEnemyCount = 0;

    // Start is called before the first frame update
    void Awake()
    {
        EnemyWaveController.onStartWave+=StartWave;
    }

    private void StartWave(WaveSettings waveSettings)
    {
        if(spawnEnemyCoroutine != null)
            StopCoroutine(spawnEnemyCoroutine);

        this.waveSettings = waveSettings;
        spawnEnemyCoroutine = StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while(currentEnemyCount < waveSettings.totalEnemyCount)
        {
            if (waveSettings.totalEnemyCount > 1)
            {
                switch (currentEnemyCount % waveSettings.aggressiveEnemyCount == 0 && waveSettings.aggressiveEnemyCount > 1)
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
            yield return new WaitForSeconds(waveSettings.spawnDelay);
        }
        yield return null;
    }
}
