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
    private AudioControllerMazeRunner audioController;

    public float currentHealth;
    public int currrentShield;
    public int maxShield = 50;
    public int maxHealth = 100;
    private bool startHealing;

    private void Awake()
    {

        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerMazeRunner>();

        gameManagerMazeRunner = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerMazeRunner>();

        currentHealth = maxHealth;

        currrentShield = maxShield;

        startHealing = false;

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

        audioController.MakeSound(audioController.getHit);

        if (currrentShield <= 0)
        {
            currentHealth = Mathf.Max(currentHealth - damage, 0);
        }
        else
        {
            currrentShield = Mathf.Max(currrentShield - damage, 0);
        }

        GetComponent<PlayerController>().killsInRow = 0;

        if (gameManagerMazeRunner.bossActive)
        {
            startHealing = false;

            StopCoroutine(ReciveHealth());
        }
    }

    private void CheckHealth()
    {

        healthBar.value = currentHealth / maxHealth;

        shieldBar.value = currrentShield / maxShield;

        healthText.text = $"{currentHealth} / {maxHealth}";

        shieldText.text = $"{currrentShield} / {maxShield}";

        if (currentHealth <= 0)
        {
            gameManagerMazeRunner.Lose();            
        }
        else if (gameManagerMazeRunner.bossActive)
        {
            StartCoroutine(ReciveHealth());
        }
    }

    private IEnumerator ReciveHealth()
    {
        while (startHealing)
        {
            if (currentHealth < 100)
            {
                currentHealth += Time.deltaTime;
                yield return null;
            }
            yield return null;
        }
    }

    public void AddShield(int shieldBonus)
    {
        currrentShield += shieldBonus;
        audioController.MakeSound(audioController.getHealed);
    }

    public void AddHealth(int healthBonus)
    {
        currentHealth += healthBonus;

        audioController.MakeSound(audioController.getHealed);
    }
   
}
