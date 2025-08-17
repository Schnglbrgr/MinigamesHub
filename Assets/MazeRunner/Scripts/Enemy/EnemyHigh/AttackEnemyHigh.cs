using System.Collections;
using UnityEngine;

public class AttackEnemyHigh : MonoBehaviour
{
    [SerializeField] private Transform[] shootPoints;
    [SerializeField] private GameObject bulletEnemy;
    [SerializeField] private float durationAttack;
    [SerializeField] private float coolDownAttack;

    private GameObject player;
    private GameObject currentBullet;
    private GameObject gameManagerMazeRunner;
    private Quaternion startRotation;
    private float timer;
    private float timerShoot;
    private float fireRate = 1f;
    private float rotationBullet;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        startRotation = transform.rotation;

        gameManagerMazeRunner = GameObject.FindGameObjectWithTag("GameController");

    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timerShoot > 0)
        {
            timerShoot -= Time.deltaTime;
        }

        if (!CheckPositionPlayer())
        {
        }
    }

    public void AttackPlayer(int damage)
    {
        if (CheckPositionPlayer() && timer <= 0)
        {
            if (timerShoot <= 0)
            {
                rotationBullet = 0f;

                for (int x = 0; x < shootPoints.Length; x++)
                {
                    currentBullet = gameManagerMazeRunner.GetComponent<PoolManager>().PoolInstance(bulletEnemy);

                    currentBullet.transform.position = shootPoints[x].position;

                    currentBullet.GetComponent<EnemyBullet>().damage = damage;

                    rotationBullet -= 90f;

                    currentBullet.transform.rotation = Quaternion.Euler(0f, 0f, rotationBullet);
                }

                timerShoot = fireRate;
            }

            StartCoroutine(StopAttack());
        }
    }


    private bool CheckPositionPlayer()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 8f)
        {
            return true;
        }
        return false;
    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(durationAttack);
        timer = coolDownAttack;
        transform.rotation = startRotation;
    }
}
