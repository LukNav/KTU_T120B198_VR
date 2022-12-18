using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private TMP_Text gameOverText;

    [SerializeField]
    private TMP_Text playerHealthText;

    [SerializeField]
    private float playerHealth;

    private void Awake()
    {
        updateHealth();
        gameOverText.enabled = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            playerHealth -= collision.gameObject.GetComponent<Bullet>().Damage;
            updateHealth();
            if (playerHealth <= 0)
            {
                GameOver();
            }
        }
    }
    public void GameOver()
    {
        gameOverText.enabled = true;
        Time.timeScale = 0;
    }
    private void updateHealth()
    {
        playerHealthText.text = "Your health: " + playerHealth;
    }
}
