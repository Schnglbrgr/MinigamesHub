using UnityEngine;

public abstract class EnemyController : MonoBehaviour, IDamageable
{
    public abstract void Movement();

    public abstract void Attack();

    public virtual void TakeDamage(int damage)
    {
    }

    public abstract void CheckHealth();

}
