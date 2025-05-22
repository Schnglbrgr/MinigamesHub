using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    private SpaceBattleManager spaceBattleManager;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();
    }
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        spaceBattleManager.SpawnEnemies();
        spaceBattleManager.EndGame();
    }
}
