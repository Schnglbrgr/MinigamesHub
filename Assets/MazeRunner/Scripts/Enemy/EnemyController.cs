using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class EnemyController : MonoBehaviour, IDamageable
{
    public PickRandomItemSO dropRandomItem;
    public EnemyMazeRunnerSO enemy;
    public Slider hpBar;
    public TMP_Text hpText;
    public Transform spawnItem;
    public ManaSystem manaSystem;

    public float currentHealth;
    public int damage;
    public int manaReward;
    public float speed;

    public abstract void Movement();

    public abstract void Attack();

    public virtual void TakeDamage(int damage)
    {
    }

    public abstract void CheckHealth();

}
