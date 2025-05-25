using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private HealthSystem playerHealthSystem;

    private float speed = 2f;
    private int damage;

    private void Awake()
    {
        playerHealthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        Destroy(gameObject, 4);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealthSystem.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
