using UnityEngine;

public class PowerUpFireRate : MonoBehaviour
{
    private AttackSpaceBattle attackSpaceBattle;

    private float timer = 3f;

    private void Awake()
    {
        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackSpaceBattle.fireRate -= 0.2f;
        attackSpaceBattle.StopFireRate(timer);
        Destroy(gameObject);
    }
}

