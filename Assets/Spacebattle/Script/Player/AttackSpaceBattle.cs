using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackSpaceBattle : MonoBehaviour
{
    public Transform spawnPosition;
    public GameObject bullet;
    public PoolManagerSO bulletPool;
    private GameObject currentBullet;
    public InputActionReference shoot;

    private float fireRate = 0.5f;
    public float currentFireRate;
    private int damage = 1;
    public int currentDamage;
    private float timer = 0f;
    private Color currentColor;

    private void Awake()
    {
        currentDamage = damage;

        currentFireRate = fireRate;

        currentColor = GetComponent<SpriteRenderer>().color;
    }

    private void OnEnable()
    {
        shoot.action.started += Attack;
    }

    private void OnDisable()
    {
        shoot.action.started -= Attack;
    }

    public void Attack(InputAction.CallbackContext obj)
    {
        if (timer <= 0)
        {
            currentBullet = bulletPool.CreateObject(bullet);
            timer = currentFireRate;
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
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

    public void ReturnColor(float timer)
    {
        StartCoroutine(ReturnColor1(timer));
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
    IEnumerator ReturnColor1(float powerUpDuration)
    {
        yield return new WaitForSeconds(powerUpDuration);
        GetComponent<SpriteRenderer>().color = currentColor;
    }
}
