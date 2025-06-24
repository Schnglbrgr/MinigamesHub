using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BossController : MonoBehaviour, IDamageable
{
    [Header("----Components----")]
    public BossMazeRunnerSO boss;
    public Transform spawnPoint;
    public Transform shootPoint;
    public Transform[] childSpawns;
    public GameObject gameManagerMazeRunner;
    public Animator animationController;
    public Slider healthBar;
    public TMP_Text healthText;
    public GameObject attackUI;

    [Header("----Variables----")]
    public int health;
    public int damage;
    public float speed;
    public int manaReward;
    public float fireRate;
    public float spawnRate;
    public float timer;
    public float timerSpawn;
    public float timerAttack;

    public abstract void Movement();

    public abstract void Attack();

    public virtual void TakeDamage(int damage)
    {
    }

    public abstract void CheckHealth();
}
