using UnityEngine;

public class PowerUpDamage : MonoBehaviour
{
    private AttackSpaceBattle attackSpaceBattle;

    private void Awake()
    {
        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackSpaceBattle.damage++;
        Destroy(gameObject);
    }
}
