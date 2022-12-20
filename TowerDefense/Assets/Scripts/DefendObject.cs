using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DefendObject : MonoBehaviour
{
    [SerializeField]
    private TMP_Text healthText;

    [SerializeField]
    [Min(0)]
    private int health;

    Player playerScript;

    private void UpdateHealth()
    {
        healthText.text = "CPU health: " + health;
    }
    private void Awake()
    {
        UpdateHealth();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            playerScript.GameOver();
        }
    }
}
