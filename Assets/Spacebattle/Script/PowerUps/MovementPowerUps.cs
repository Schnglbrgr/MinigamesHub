using UnityEngine;

public class MovementPowerUps : MonoBehaviour
{
    public WeightedEntrySpaceBattleSO powerUpEntry;
    public float speed = 3f;
    private GameObject player;
    public GameObject prefab;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        prefab = powerUpEntry.prefab;

        transform.position = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>().poolManager.PickRandomSpawn();

    }

    private void OnDisable()
    {
        transform.position = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>().poolManager.PickRandomSpawn();
    }

    private void FixedUpdate()
    {
        Movement();

        if (transform.position.y <= 0)
        {
            //GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>().poolManager.Return(GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>().currentPrefabPowerUp, gameObject);
        }
    }

    void Movement()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

}
