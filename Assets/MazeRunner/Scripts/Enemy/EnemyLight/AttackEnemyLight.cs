using UnityEngine;

public class AttackEnemyLight : MonoBehaviour
{
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform shootPoint;

    private GameObject currentBullet;
    private GameObject gameManagerMazeRunner;
    private GameObject player;
    private Vector3 targetRotation;
    private float angle;
    private float initialAngle = 90f;
    private float timer;
    private float fireRate = 2f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        gameManagerMazeRunner = GameObject.FindGameObjectWithTag("GameController");

    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    private bool CheckPositionPlayer()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 10f)
        {
            return true;
        }
        return false;
    }

    public void RotateToPlayer(int damage)
    {
        if (CheckPositionPlayer())
        {
            targetRotation = player.transform.position - transform.position;

            angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - initialAngle));

            AttackPlayer(damage);
        }
    }

    private void AttackPlayer(int damage)
    {
        if (timer <= 0)
        {
            currentBullet = gameManagerMazeRunner.GetComponent<PoolManager>().PoolInstance(enemyBullet);

            currentBullet.transform.position = shootPoint.position;

            currentBullet.GetComponent<EnemyBullet>().damage = damage;

            timer = fireRate;
        }
    }
}
