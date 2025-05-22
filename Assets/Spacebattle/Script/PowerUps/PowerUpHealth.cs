using UnityEngine;

public class PowerUpHealth : MonoBehaviour
{
    private HealthSpaceBattle healthSpaceBattle;

    private void Awake()
    {
        healthSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSpaceBattle>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (healthSpaceBattle.currentHealth < 100)
            {
                healthSpaceBattle.currentHealth += 30;
            }

            Destroy(gameObject);
        }     
    }
}
