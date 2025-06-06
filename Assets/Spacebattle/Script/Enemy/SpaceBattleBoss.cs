using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceBattleBoss : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform[] ways;
    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject attackLine;
    [SerializeField] private TMP_Text hpText;

    public GameObject bulletEnemy;
    public Transform spawnPointBullet;
    private SpaceBattleManager spaceBattleManager;
    private HealthSpaceBattle healthSpaceBattle;

    private int currentHealth;
    private float speed = 2f;
    private int randomWay;
    private float coolDown;
    private float fireRate;
    private float timerAttack;
    private bool isAttacking;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();
        healthSpaceBattle = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSpaceBattle>();

        currentHealth = 15;

        transform.position = spaceBattleManager.poolManager.PickRandomSpawn();

        hpBar.value = currentHealth / 15;

        coolDown = 3f;

        isAttacking = false;

        fireRate = 0f;

        hpText.text = $"{currentHealth} / 15";
    }

    private void OnEnable()
    {
        currentHealth = 15;
        transform.position = spaceBattleManager.poolManager.PickRandomSpawn();
        randomWay = Random.Range(0, ways.Length);
    }

    private void OnDisable()
    {
        hpBar.gameObject.SetActive(false);
    }

    private void Start()
    {
        randomWay = Random.Range(0, ways.Length);

        timerAttack = 4.5f;
    }

    private void Update()
    {
        if (timerAttack > 0)
        {
            timerAttack -= Time.deltaTime;
            fireRate -= Time.deltaTime;
        }

        Attack();
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            Movement();
        }
    }

    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, ways[randomWay].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, ways[randomWay].position) < 0.2f)
        {
            randomWay = Random.Range(0, ways.Length);
        }

    }

    void Attack()
    {
        if (timerAttack <= 0)
        {
            isAttacking = true;

            attackLine.SetActive(true);

            StartCoroutine(AttackPlayer());
        }
    }

    void CheckHealth()
    {
        hpBar.value = currentHealth / 15;
        hpText.text = $"{currentHealth} / 15";

        if (currentHealth <= 0)
        {
            spaceBattleManager.bossActive = false;
            spaceBattleManager.SpawnEnemies();
            hpBar.gameObject.SetActive(false);
            coolDown = Mathf.Max(coolDown -= 0.5f, 0.5f);
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        hpBar.gameObject.SetActive(true);

        CheckHealth();

        GetComponent<Animation>().Play();
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(0.45f);

        if (fireRate <= 0)
        {
            spaceBattleManager.poolManager.CreateObject(bulletEnemy);
        }

        attackLine.SetActive(false);

        timerAttack = coolDown;

        isAttacking = false;

        fireRate = 1f;
    }
}
