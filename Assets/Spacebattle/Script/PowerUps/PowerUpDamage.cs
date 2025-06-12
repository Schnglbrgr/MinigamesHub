using UnityEngine;

public class PowerUpDamage : PowerUpController, IPickable
{
    private AttackSpaceBattle attackSpaceBattle;
    private SpaceBattleManager spaceBattleManager;
    private float timer = 3;

    private void Awake()
    {
        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();
    }

    public override void GivePowerUp()
    {
        attackSpaceBattle.currentDamage++;
        attackSpaceBattle.StopDamage(timer);
        attackSpaceBattle.ReturnColor(timer);
        spaceBattleManager.poolManager.Return(GetComponent<MovementPowerUps>().prefab, gameObject);
    }

    public void PickItem()
    {
        GivePowerUp();
    }
}
