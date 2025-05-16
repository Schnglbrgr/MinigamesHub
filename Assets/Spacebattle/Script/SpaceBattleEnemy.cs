using UnityEngine;

public class SpaceBattleEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private SpaceBattleManager spaceBattleManager;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();
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
        spaceBattleManager.SpawnEnemies();
        spaceBattleManager.score += 10;

        Destroy(gameObject);

    }


}
