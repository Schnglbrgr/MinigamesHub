using UnityEngine;

public class PowerUpFireRate : MonoBehaviour
{
    private AttackSpaceBattle attackSpaceBattle;

    private void Awake()
    {
        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackSpaceBattle.fireRate = 0.2f;

            Destroy(gameObject);
        }
    }
}
