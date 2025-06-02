using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    private AttackSpaceBattle attackSpaceBattle;
    private MovementSpacebattle movementSpacebattle;
    private SpaceBattleManager spaceBattleManager;

    private float speedBoost;
    private float timer = 3f;
    private float currentSpeed;

    private void Awake()
    {
        movementSpacebattle = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementSpacebattle>();

        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();

        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        currentSpeed = movementSpacebattle.currentSpeed;

        speedBoost = currentSpeed + 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movementSpacebattle.currentSpeed = speedBoost;
            movementSpacebattle.StopBoost(timer);
            attackSpaceBattle.ReturnColor(timer);
            spaceBattleManager.poolManager.Return(GetComponent<MovementPowerUps>().prefab, gameObject);
        }
    }


}
