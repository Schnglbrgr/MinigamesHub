using UnityEngine;

public class MovementEnemyLight : MonoBehaviour
{
    [SerializeField] private Transform[] ways;
    [SerializeField] private float speed = 1f;

    private int selectWay;

    private void Start()
    {
        selectWay = Random.Range(0, ways.Length);
    }

    private void FixedUpdate()
    {
        //Movement();
    }

    private void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, ways[selectWay].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, ways[selectWay].position) < 0.02f)
        {
            selectWay = Random.Range(0, ways.Length);
        }
    }
}
