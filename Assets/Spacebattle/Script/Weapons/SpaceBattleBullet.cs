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

        damage = attackSpaceBattle.damage;
    }

    private void FixedUpdate()
    {
        Movement();

        Destroy(gameObject, 4);
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
            Destroy(gameObject);
        }
    }
}
