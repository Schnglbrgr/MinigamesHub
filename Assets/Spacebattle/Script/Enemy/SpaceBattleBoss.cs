using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpaceBattleBoss : MonoBehaviour, IDamageableSpaceBattle
{
    [SerializeField] private Transform[] ways;
    [SerializeField] private Slider hpBar;
    [SerializeField] private GameObject attackLine;
    [SerializeField] private TMP_Text hpText;

    public GameObject bulletEnemy;
    public Transform spawnPointBullet;
    private SpaceBattleManager spaceBattleManager;
    private AudioControllerSpaceBattle audioController;
    private UltimateAttackSpaceBattle ultimateAttack;

    private int currentHealth;
    private float speed = 2f;
    private int randomWay;
    private float coolDown;
    private float fireRate;
    private float timerAttack;
    private bool isAttacking;
    private int ultimateReward = 20;
    private int hp = 15;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerSpaceBattle>();

        ultimateAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<UltimateAttackSpaceBattle>();

        currentHealth = hp;

        transform.position = spaceBattleManager.poolManager.PickRandomSpawn();

        coolDown = 3f;

        isAttacking = false;

        fireRate = 0f;
    }

    private void OnEnable()
    {
        currentHealth = hp;

        transform.position = spaceBattleManager.poolManager.PickRandomSpawn();

        randomWay = Random.Range(0, ways.Length);

        audioController.musicSource.clip = audioController.bossFight;

        audioController.musicSource.Play();
    }

    private void OnDisable()
    {
        hpBar.gameObject.SetActive(false);

        audioController.musicSource.clip = audioController.bg;

        audioController.musicSource.Play();

        hp += 5;
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

        hpBar.value = currentHealth / hp;

        hpText.text = $"{currentHealth} / {hp}";


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
        if (currentHealth <= 0)
        {
            spaceBattleManager.bossActive = false;

            spaceBattleManager.SpawnEnemies();

            hpBar.gameObject.SetActive(false);

            coolDown = Mathf.Max(coolDown -= 0.5f, 0.5f);

            gameObject.SetActive(false);

            if (ultimateAttack.ultimateCharge < 100)
            {
                ultimateAttack.ultimateCharge += ultimateReward;
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageableSpaceBattle isDamageable = collision.gameObject.GetComponent<IDamageableSpaceBattle>();

        if (isDamageable != null)
        {
            isDamageable.TakeDamage(bulletEnemy.GetComponent<SpaceBattleEnemyBullet>().damage);
        }
    }
}
