using System.Collections;
using UnityEngine;

public class SpaceBattleBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private SpaceBattleEnemy enemy;
    private AttackSpaceBattle attackSpaceBattle;

    private int damage;

    private void Awake()
    {
        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();

        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<SpaceBattleEnemy>();

        damage = attackSpaceBattle.currentDamage;

        transform.position = attackSpaceBattle.spawnPosition.position;
    }

    private void OnEnable()
    {
        transform.position = attackSpaceBattle.spawnPosition.position;
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
        if (collision.gameObject.tag == "Enemy")
        {
            enemy.TakeDamage(damage);
            attackSpaceBattle.bulletPool.Return(attackSpaceBattle.bullet, gameObject);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(4);
        attackSpaceBattle.bulletPool.Return(attackSpaceBattle.bullet, gameObject);
    }
}
