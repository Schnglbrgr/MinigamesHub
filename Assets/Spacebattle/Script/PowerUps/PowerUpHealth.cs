using UnityEngine;

public class PowerUpHealth : MonoBehaviour
{
    private HealthSpaceBattle healthSpaceBattle;
    private AttackSpaceBattle attackSpaceBattle;
    private SpaceBattleManager spaceBattleManager;

    private void Awake()
    {
        healthSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSpaceBattle>();
        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (healthSpaceBattle.currentHealth < 100)
            {
                healthSpaceBattle.currentHealth += 30;                
            }
            attackSpaceBattle.ReturnColor(0.1f);
            spaceBattleManager.poolManager.Return(GetComponent<MovementPowerUps>().prefab, gameObject);
        }
    }
}
