using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private TMP_Text gameOverText;
    
    [SerializeField]
    private Button mainMenuButton;

    [SerializeField]
    private TMP_Text playerHealthText;

    [SerializeField]
    private float playerHealth;

    public delegate void OnPlayersDeathDelegate();
    public static OnPlayersDeathDelegate onPlayersDeath;

    private void Awake()
    {
        updateHealth();
        mainMenuButton.gameObject.SetActive(false);
        gameOverText.enabled = false;
        onPlayersDeath+=GameOver;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && playerHealth > 0)//If player is hit by bullet and is not dead
        {
            playerHealth -= collision.gameObject.GetComponent<Bullet>().Damage;
            updateHealth();
            if (playerHealth <= 0)
            {
                onPlayersDeath();
            }
        }
    }
    public void GameOver()
    {
        gameOverText.enabled = true;
        mainMenuButton.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    private void updateHealth()
    {
        playerHealthText.text = "Your health: " + playerHealth;
    }
}
