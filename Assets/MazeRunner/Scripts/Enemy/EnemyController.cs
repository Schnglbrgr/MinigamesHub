using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class EnemyController : MonoBehaviour, IDamageable
{
    public PickRandomItemSO dropRandomItem;
    public Slider hpBar;
    public TMP_Text hpText;

    public abstract void Movement();

    public abstract void Attack();

    public virtual void TakeDamage(int damage)
    {
    }

    public abstract void CheckHealth();

}
