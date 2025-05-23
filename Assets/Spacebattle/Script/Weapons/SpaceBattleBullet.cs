using UnityEngine;

public class SpaceBattleBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private SpaceBattleEnemy spaceBattleEnemy;
    private AttackSpaceBattle attackSpaceBattle;
    private SpaceBattleManager spaceBattleManager;

    public int damage;

    private void Awake()
    {
        spaceBattleEnemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<SpaceBattleEnemy>();

        attackSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackSpaceBattle>();
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
            Destroy(gameObject);
            spaceBattleEnemy.TakeDamage(damage);            
        }
    }
}
