using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WaveSettings", menuName = "ScriptableObjects/WaveSettings", order = 1)]
public class WaveSettings : ScriptableObject
{
    [SerializeField]
    public int totalEnemyCount;
    [SerializeField]
    public int aggressiveEnemyCount;
    [SerializeField]
    public float spawnDelay;
}
