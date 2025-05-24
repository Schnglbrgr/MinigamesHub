using UnityEngine;

public class PowerUpDamage : MonoBehaviour
{
    private AttackSpaceBattle attackSpaceBattle;

    private float timer = 3;

    private void Awake()
    {
        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackSpaceBattle.currentDamage++;
        attackSpaceBattle.StopDamage(timer);
        Destroy(gameObject);
    }
}
