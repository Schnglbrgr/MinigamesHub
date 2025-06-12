using System.Collections;
using UnityEngine;

public class AttackEnemyHigh : MonoBehaviour
{
    [SerializeField] private Transform[] shootPoints;
    [SerializeField] private GameObject bulletEnemy;
    [SerializeField] private float durationAttack;
    [SerializeField] private float coolDownAttack;

    private GameObject player;
    private Color currentColor;
    private Quaternion startRotation;
    private float timer;
    private float timerShoot;
    private float fireRate = 1f;
    private float rotationSpeed = 100f;
    private float rotationBullet;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startRotation = transform.rotation;
    
        for (int x = 0; x < 3; x++)
        {
            currentColor = transform.GetChild(x).GetComponent<SpriteRenderer>().color;
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            ChangeColor(Color.white);
        }

        if (timerShoot > 0)
        {
            timerShoot -= Time.deltaTime;
        }

        if (!CheckPositionPlayer())
        {
            ChangeColor(currentColor);
        }
    }

    public void AttackPlayer(int damage)
    {
        if (CheckPositionPlayer() && timer <= 0)
        {
            //RotateEnemy();

            if (timerShoot <= 0)
            {
                rotationBullet = 0f;

                for (int x = 0; x < shootPoints.Length; x++)
                {
                    Instantiate(bulletEnemy, shootPoints[x].position, Quaternion.Euler(0f,0f,rotationBullet));
                    bulletEnemy.GetComponent<EnemyBullet>().damage = damage;
                    rotationBullet += 90f;
                }

                timerShoot = fireRate;
            }

            ChangeColor(Color.red);

            StartCoroutine(StopAttack());
        }
    }

    private void RotateEnemy()
    {
        transform.Rotate(0f,0f,rotationSpeed * Time.deltaTime);
    }

    private bool CheckPositionPlayer()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 8f)
        {
            return true;
        }
        return false;
    }

    private void ChangeColor(Color color)
    {
        for (int x = 0; x < 3; x++)
        {
            transform.GetChild(x).GetComponent<SpriteRenderer>().color = color;
        }
    }

    IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(durationAttack);
        timer = coolDownAttack;
        transform.rotation = startRotation;
    }
}
