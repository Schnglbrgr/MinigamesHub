using UnityEngine;

public class PowerUpFireRate : PowerUpController, IPickable
{

    private float timer = 3f;

    private void Awake()
    {
        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerSpaceBattle>();
    }

    public override void GivePowerUp()
    {
        attackSpaceBattle.currentFireRate -= 0.2f;
        attackSpaceBattle.StopFireRate(timer);
        attackSpaceBattle.ReturnColor(timer);
        spaceBattleManager.poolManager.Return(GetComponent<MovementPowerUps>().prefab, gameObject);
    }

    public void PickItem()
    {
        GivePowerUp();
        audioController.MakeSound(audioController.pickPowerUp);
    }

}

