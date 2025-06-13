using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 8f;

    public int damage;

    private Rigidbody2D rb;
    private PoolManager poolManager;
    private AttackSystem currentWeapon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();

        StartCoroutine(ReturnBullet());

        currentWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<CollectWeapon>().currentWeapon.GetComponent<AttackSystem>();

        transform.rotation = currentWeapon.transform.rotation;

        transform.position = currentWeapon.spawnPoint.position;
    }

    private void OnDisable()
    {
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
        IDamageable isDamageable = collision.gameObject.GetComponent<IDamageable>();

        if (isDamageable != null)
        {
            isDamageable.TakeDamage(damage);
            poolManager.Return(currentWeapon.bullet,gameObject);
        }

        if (collision.gameObject.layer == 7)
        {
            poolManager.Return(currentWeapon.bullet, gameObject);
        }

    }

    IEnumerator ReturnBullet()
    {
        yield return new WaitForSeconds(3);
        poolManager.Return(currentWeapon.bullet, gameObject);
    }
}
