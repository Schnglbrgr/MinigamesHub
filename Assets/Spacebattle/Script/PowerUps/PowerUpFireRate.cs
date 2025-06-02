using UnityEngine;

public class PowerUpFireRate : MonoBehaviour
{
    private AttackSpaceBattle attackSpaceBattle;
    private SpaceBattleManager spaceBattleManager;
    private float timer = 3f;

    private void Awake()
    {
        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackSpaceBattle.fireRate -= 0.2f;
        attackSpaceBattle.StopFireRate(timer);
        attackSpaceBattle.ReturnColor(timer);
        spaceBattleManager.poolManager.Return(GetComponent<MovementPowerUps>().prefab, gameObject);
    }
}

