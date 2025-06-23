using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class EnemyController : MonoBehaviour, IDamageable
{
    public PickRandomItemSO pickElementalWeapon;
    public PickRandomItemSO dropRandomItem;
    public EnemyMazeRunnerSO enemy;
    public Slider hpBar;
    public TMP_Text hpText;
    public Transform spawnItem;
    public ManaSystem manaSystem;
    public GameObject dropAmmo;
    public GameObject ammoPrefab;
    public GameObject dropItem;
    public PoolManager poolManager;
    public GameObject currentElemental;
    public Animator animationController;
    public AudioControllerMazeRunner audioController;

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
