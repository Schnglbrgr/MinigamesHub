using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    private SpaceBattleManager spaceBattleManager;
    public WeightedEntrySpaceBattleSO bombEntry;
    private GameObject prefab;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();
        prefab = bombEntry.prefab;
        transform.position = spaceBattleManager.poolManager.PickRandomSpawn();
    }
    private void OnDisable()
    {
        transform.position = spaceBattleManager.poolManager.PickRandomSpawn();
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y <= 0)
        {
            spaceBattleManager.poolManager.Return(prefab,gameObject);
            spaceBattleManager.SpawnEnemies();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        spaceBattleManager.poolManager.Return(prefab, gameObject);
        spaceBattleManager.EndGame();
    }
}
