using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendObject : MonoBehaviour
{
    [SerializeField]
    int gameOverEnemies;
    [SerializeField]
    int currentEnemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            currentEnemies++;
            Destroy(other);
        }
        if(currentEnemies >= gameOverEnemies)
        {
            Time.timeScale = 0;
        }
    }
}
