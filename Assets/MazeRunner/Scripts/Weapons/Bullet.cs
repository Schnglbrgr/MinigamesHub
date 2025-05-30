using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    public int damage;

    private Rigidbody2D rb;
    private EnemyController enemyController;
    private PoolManager poolManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();
    }

    void Update()
    {
        rb.linearVelocity = transform.right * speed;

        StartCoroutine(ReturnBullet());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyController = collision.gameObject.GetComponent<EnemyController>();

            enemyController.TakeDamageEnemy(damage);

            StopCoroutine(ReturnBullet());
        }

        poolManager.Return(GetComponent<AttackSystem>().bullet,gameObject);
    }

    IEnumerator ReturnBullet()
    {
        yield return new WaitForSeconds(3);
        poolManager.Return(GetComponent<AttackSystem>().bullet, gameObject);

    }
}
