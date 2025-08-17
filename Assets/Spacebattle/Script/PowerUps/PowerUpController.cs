using UnityEngine;

public abstract class PowerUpController : MonoBehaviour
{
    public AttackSpaceBattle attackSpaceBattle;
    public SpaceBattleManager spaceBattleManager;
    public AudioControllerSpaceBattle audioController;

    public abstract void GivePowerUp();
}
