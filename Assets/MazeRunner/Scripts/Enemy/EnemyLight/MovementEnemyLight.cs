using UnityEngine;

public class MovementEnemyLight : MonoBehaviour
{
    [SerializeField] private Transform[] patrolsPoints;

    private Transform wayParent;
    private float speed = 1f;
    private int randomNum;
    private void Awake()
    {
        wayParent = GameObject.FindGameObjectWithTag("Ways").GetComponent<Transform>();
    }

    private void Start()
    {
        for (int x = 0; x < patrolsPoints.Length; x++)
        {
            patrolsPoints[x].SetParent(wayParent);
        }

        randomNum = Random.Range(0, patrolsPoints.Length);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolsPoints[randomNum].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolsPoints[randomNum].position) < 0.1f)
        {
            randomNum = Random.Range(0, patrolsPoints.Length);
        }
    }
}
