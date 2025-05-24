using System.Collections;
using UnityEngine;

public class AttackSpaceBattle : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPosition;

    public float fireRate = 0.5f;
    private float currentFireRate;
    private int damage;
    public int currentDamage;
    private float timer;

    private void Awake()
    {
        damage = 1;

        currentDamage = damage;

        currentFireRate = fireRate;
    }

    public void Attack()
    {
        if (timer <= 0)
        {           
            Instantiate(bullet, spawnPosition.position, Quaternion.identity);

            timer = currentFireRate;
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    public void StopDamage(float timer)
    {
        StartCoroutine(StopDamageTimer(timer));
    }

    public void StopFireRate(float timer)
    {
        StartCoroutine(StopFireRateTimer(timer));
    }

    IEnumerator StopFireRateTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        currentFireRate = fireRate;
    }

    IEnumerator StopDamageTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        currentDamage = damage;
    }
}
