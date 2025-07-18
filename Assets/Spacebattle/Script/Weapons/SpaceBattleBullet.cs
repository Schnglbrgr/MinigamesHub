using System.Collections;
using UnityEngine;

public class SpaceBattleBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private AttackSpaceBattle attackSpaceBattle;

    public int damage;

    private void Awake()
    {
        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();

        damage = attackSpaceBattle.currentDamage;

        transform.position = attackSpaceBattle.spawnPosition.position;
    }

    private void OnEnable()
    {
        transform.position = attackSpaceBattle.spawnPosition.position;

        damage = attackSpaceBattle.currentDamage;
    }

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageableSpaceBattle isDamageable = collision.gameObject.GetComponent<IDamageableSpaceBattle>();

        if (isDamageable != null)
        {
            isDamageable.TakeDamage(damage);
            attackSpaceBattle.bulletPool.Return(attackSpaceBattle.bullet, gameObject);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(4);
        attackSpaceBattle.bulletPool.Return(attackSpaceBattle.bullet, gameObject);
    }
}
