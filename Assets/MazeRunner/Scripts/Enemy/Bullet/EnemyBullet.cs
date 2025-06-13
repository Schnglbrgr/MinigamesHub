using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 2f;
    public int damage;

    private void FixedUpdate()
    {
        transform.position += transform.up * speed * Time.deltaTime;

        Destroy(gameObject, 4);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable isDamageable = collision.gameObject.GetComponent<IDamageable>();

        if (isDamageable != null && collision.gameObject.tag != "Enemy")
        {
            isDamageable.TakeDamage(damage);
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
