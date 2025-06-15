using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private Slider shieldBar;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text shieldText;

    private GameManagerMazeRunner gameManagerMazeRunner;
    public int currentHealth;
    public int currrentShield;
    public int maxShield = 50;
    public int maxHealth = 100;

    private void Awake()
    {
        gameManagerMazeRunner = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerMazeRunner>();

        currentHealth = maxHealth;

        currrentShield = maxShield;

        healthText.text = $"{currentHealth} / {maxHealth}";

        shieldText.text = $"{currrentShield} / {maxShield}";
    }

    private void Update()
    {
        CheckHealth();
    }

    public void TakeDamage(int damage)
    {
        GetComponent<Animation>().Play();

        if (currrentShield <= 0)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0);
        }
        else
        {
            currrentShield = Mathf.Max(currrentShield - damage, 0);
        }

        healthText.text = $"{currentHealth} / {maxHealth}";

        shieldText.text = $"{currrentShield} / {maxShield}";
    }

    private void CheckHealth()
    {
        healthBar.value = currentHealth / maxHealth;

        shieldBar.value = currrentShield / maxShield;

        if (currentHealth <= 0)
        {
            gameManagerMazeRunner.Lose();
            Destroy(gameObject);
        }
    }

    public void AddShield()
    {
        currrentShield += 10;
    }

    public void AddHealth(int healthBonus)
    {
        currentHealth += healthBonus;
    }
   
}
