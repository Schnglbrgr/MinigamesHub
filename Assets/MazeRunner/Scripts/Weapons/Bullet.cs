using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    public int damage;

    private Rigidbody2D rb;
    private EnemyHealthSystem enemyHealthSystem;

    private void Awake()
    {
        enemyHealthSystem = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealthSystem>();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = Vector2.right * speed;

        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyHealthSystem.TakeDamageEnemy(damage);
        }

        Destroy(gameObject);
    }
}
