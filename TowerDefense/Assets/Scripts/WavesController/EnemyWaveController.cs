using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private List<WaveSettings> waves;

    [SerializeField]
    [Min(1)]
    private int wavesDelay;

    [SerializeField]
    private bool isEndless = false;


    public delegate void OnStartWaveDelegate(WaveSettings waveSettings);
    public static OnStartWaveDelegate onStartWave;

    private void Awake()
    {
        StartCoroutine(NotifySpawners());
    }

    private IEnumerator NotifySpawners()
    {
        foreach(var wave in waves)
        {
            onStartWave(wave);
            yield return new WaitForSeconds(wavesDelay + wave.spawnDelay * wave.totalEnemyCount);
        }
        if (isEndless) 
            yield return NotifySpawners();

        yield return null;
    }
}
