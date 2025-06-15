using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 8f;

    public int damage;

    private Rigidbody2D rb;
    private PoolManager poolManager;
    private CollectWeapon collectWeapon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();

        collectWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<CollectWeapon>();

        StartCoroutine(ReturnBullet());

    }

    private void OnDisable()
    {
        StopCoroutine(ReturnBullet());
    }


    void FixedUpdate()
    {
        rb.linearVelocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable isDamageable = collision.gameObject.GetComponent<IDamageable>();

        if (isDamageable != null && collision.gameObject.tag != "Player")
        {
            isDamageable.TakeDamage(damage);

            poolManager.Return(collectWeapon.bulletPrefab,gameObject);
        }

        if (collision.gameObject.layer == 7)
        {
            poolManager.Return(collectWeapon.bulletPrefab, gameObject);
        }

    }

    IEnumerator ReturnBullet()
    {
        yield return new WaitForSeconds(3);
        poolManager.Return(collectWeapon.bulletPrefab, gameObject);
    }
}
