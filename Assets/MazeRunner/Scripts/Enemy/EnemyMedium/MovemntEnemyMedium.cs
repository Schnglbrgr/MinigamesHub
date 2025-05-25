using UnityEngine;

public class MovemntEnemyMedium : MonoBehaviour
{
    private GameObject player;

    private float speed = 1.5f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private bool CheckPositionPlayer()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 4.5f)
        {
            return true;
        }
        return false;
    }

    private void MoveTowardsPlayer()
    {
        if (CheckPositionPlayer())
        {
            transform.position = Vector2.MoveTowards(transform.position,player.transform.position, speed * Time.deltaTime);
        }
    }
}
