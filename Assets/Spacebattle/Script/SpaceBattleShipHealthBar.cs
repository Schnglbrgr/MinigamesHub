using UnityEngine;
using UnityEngine.UI;

public class SpaceBattleShipHealthBar : MonoBehaviour
{
    private PlayerSpaceBattle playerSpaceBattle;
    private SpaceBattleManager spaceBattleManager;

    private int currentHealth;
    private int health;

    private void Awake()
    {
        playerSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSpaceBattle>();

        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        currentHealth = playerSpaceBattle.health;
    }

    private void Update()
    {
        currentHealth = playerSpaceBattle.currentHealth;

        HealthSystem();
    }

    void HealthSystem()
    {
        if (currentHealth <= 0)
        {
            spaceBattleManager.EndGame();
        }

        if (currentHealth < 100)
        {
            gameObject.SetActive(true);
        }

        GetComponent<Slider>().value = currentHealth / health;
    }
}
