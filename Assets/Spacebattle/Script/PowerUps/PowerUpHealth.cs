using UnityEngine;

public class PowerUpHealth : PowerUpController, IPickableSpaceBattle
{
    private HealthSpaceBattle healthSpaceBattle;

    private void Awake()
    {
        healthSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSpaceBattle>();
        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerSpaceBattle>();
    }

    public override void GivePowerUp()
    {
        if (healthSpaceBattle.currentHealth < 100)
        {
            healthSpaceBattle.currentHealth += 30;
        }
        attackSpaceBattle.ReturnColor(0.1f);
        spaceBattleManager.poolManager.Return(GetComponent<MovementPowerUps>().prefab, gameObject);
    }

    public void PickItem()
    {
        GivePowerUp();
        audioController.MakeSound(audioController.pickPowerUp);
    }
}
