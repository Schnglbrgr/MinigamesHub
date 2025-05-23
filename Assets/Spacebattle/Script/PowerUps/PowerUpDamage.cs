using UnityEngine;

public class PowerUpDamage : MonoBehaviour
{
    private AttackSpaceBattle attackSpaceBattle;

    private int currentDamage;
    private int boostDamage;

    private void Awake()
    {
        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();

        currentDamage = attackSpaceBattle.damage;

        boostDamage = currentDamage * 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackSpaceBattle.damage = boostDamage;
            Destroy(gameObject);
        }
    }
}
