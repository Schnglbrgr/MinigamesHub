using UnityEngine;

public class SpaceBattleEnemyBullet : MonoBehaviour
{
    private GameObject boss;
    private SpaceBattleManager spaceBattleManager;

    private float speed = 15f;
    public int damage = 50;

    private void Awake()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");

        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        transform.position = boss.GetComponent<SpaceBattleBoss>().spawnPointBullet.position;
    }

    private void OnEnable()
    {
        transform.position = boss.GetComponent<SpaceBattleBoss>().spawnPointBullet.position;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageableSpaceBattle isDamageable = collision.gameObject.GetComponent<IDamageableSpaceBattle>();

        if (isDamageable != null && collision.gameObject.tag != "Enemy")
        {
            isDamageable.TakeDamage(damage);
            spaceBattleManager.poolManager.Return(boss.GetComponent<SpaceBattleBoss>().bulletEnemy, gameObject);
        }
        else if (collision.gameObject.tag == "Floor")
        {
            spaceBattleManager.poolManager.Return(boss.GetComponent<SpaceBattleBoss>().bulletEnemy, gameObject);
        }

    }
}
