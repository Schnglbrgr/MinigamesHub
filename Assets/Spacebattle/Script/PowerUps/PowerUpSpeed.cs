using UnityEngine;

public class PowerUpSpeed : PowerUpController, IPickableSpaceBattle
{
    private MovementSpacebattle movementSpacebattle;

    private float speedBoost;
    private float timer = 3f;
    private float currentSpeed;

    private void Awake()
    {
        movementSpacebattle = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementSpacebattle>();

        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();

        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerSpaceBattle>();

        currentSpeed = movementSpacebattle.currentSpeed;

        speedBoost = currentSpeed + 2;
    }

    public override void GivePowerUp()
    {
        movementSpacebattle.currentSpeed = speedBoost;
        movementSpacebattle.StopBoostTimer(timer);
        attackSpaceBattle.ReturnColor(timer);
        spaceBattleManager.poolManager.Return(GetComponent<MovementPowerUps>().prefab, gameObject);
    }

    public void PickItem()
    {
        GivePowerUp();
        audioController.MakeSound(audioController.pickPowerUp);
    }
}
