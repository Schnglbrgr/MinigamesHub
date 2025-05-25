using UnityEngine;

public class PowerUpDamage : MonoBehaviour
{
    private AttackSpaceBattle attackSpaceBattle;
    private SpaceBattleManager spaceBattleManager;
    private float timer = 3;

    private void Awake()
    {
        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackSpaceBattle.currentDamage++;
        attackSpaceBattle.StopDamage(timer);
        attackSpaceBattle.ReturnColor(timer);
        spaceBattleManager.powerUpPool.Return(GetComponent<MovementPowerUps>().prefab, gameObject);
    }
}
