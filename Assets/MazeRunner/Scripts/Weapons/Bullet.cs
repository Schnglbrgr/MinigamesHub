using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 8f;

    public int damage;

    private Rigidbody2D rb;
    private EnemyController enemyController;
    private PoolManager poolManager;
    private AttackSystem currentWeapon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();
        StartCoroutine(ReturnBullet());
        currentWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<CollectWeapon>().currentWeapon.GetComponent<AttackSystem>();
        transform.position = currentWeapon.spawnPoint.position;
        transform.rotation = currentWeapon.transform.rotation;
    }

    private void OnDisable()
    {
        transform.position = currentWeapon.spawnPoint.position;
        StopCoroutine(ReturnBullet());
    }

    private void OnEnable()
    {
        transform.position = currentWeapon.spawnPoint.position;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyController = collision.gameObject.GetComponent<EnemyController>();

            enemyController.TakeDamageEnemy(damage);

            enemyController.GetComponent<Animation>().Play();

        }

        poolManager.Return(currentWeapon.bullet,gameObject);
    }

    IEnumerator ReturnBullet()
    {
        yield return new WaitForSeconds(3);
        poolManager.Return(currentWeapon.GetComponent<AttackSystem>().bullet, gameObject);

    }
}
