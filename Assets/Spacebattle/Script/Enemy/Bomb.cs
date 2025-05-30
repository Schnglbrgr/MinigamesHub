using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    private SpaceBattleManager spaceBattleManager;
    public WeightedEntrySO bombEntry;

    private GameObject prefab;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();
        prefab = bombEntry.prefab;
    }
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y <= 0)
        {
            spaceBattleManager.poolManager.Return(prefab,gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        spaceBattleManager.poolManager.Return(prefab, gameObject);
        spaceBattleManager.SpawnEnemies();
        spaceBattleManager.EndGame();
    }
}
