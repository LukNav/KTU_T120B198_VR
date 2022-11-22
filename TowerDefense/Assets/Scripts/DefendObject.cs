using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DefendObject : MonoBehaviour
{
    [SerializeField]
    private TMP_Text gameOverText;

    [SerializeField]
    private TMP_Text healthText;

    [SerializeField]
    [Min(0)]
    private int health;

    private void UpdateHealth()
    {
        healthText.text = "Bomb health: " + health;
    }
    private void Awake()
    {
        gameOverText.enabled = false;
        UpdateHealth();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            health--;
            UpdateHealth();
            Destroy(other.gameObject);
        }
        if(health <= 0)
        {
            gameOverText.enabled = true;
            Time.timeScale = 0;
        }
    }
}
