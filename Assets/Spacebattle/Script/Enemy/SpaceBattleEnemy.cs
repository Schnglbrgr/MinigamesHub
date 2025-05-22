using UnityEngine;

public class SpaceBattleEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int damage;

    private SpaceBattleManager spaceBattleManager;
    private HealthSpaceBattle healthSpaceBattle;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        healthSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSpaceBattle>();
    }

    private void FixedUpdate()
    {
        Movement();
        ReachedFloor();
    }

    void Movement()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    void ReachedFloor()
    {
        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
            spaceBattleManager.EndGame();           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        spaceBattleManager.score += 10;
        spaceBattleManager.SpawnEnemies();
        spaceBattleManager.LevelUp(speed);
        Destroy(gameObject);

        if (collision.gameObject.tag == "Player")
        {
            healthSpaceBattle.TakeDamage(damage);
        }
    }


}
