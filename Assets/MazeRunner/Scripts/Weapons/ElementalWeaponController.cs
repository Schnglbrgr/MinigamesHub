using UnityEngine;
using UnityEngine.UI;

public abstract class ElementalWeaponController : MonoBehaviour
{
    public RotateWeapon rotateWeapon;
    public ElementalsSO elementalWeapon;
    public GameObject elementalBullet;
    public GameObject currentBullet;
    public CollectWeapon collectWeapon;
    public PoolManager poolManager;
    public GameObject headWeapon;
    public Color colorBullet;
    public Slider hpBar;
    public Transform shootPoint;

    public float healthWeapon;
    public int damage;
    public float durationAttack;
    public float fireRate;

    public abstract void SpecialAttack(Collision2D enemyy);

    public abstract void Attack();

    public virtual void Rotation()
    {
        rotateWeapon.Rotate();
    }

    public abstract void HealthWeapon();

    public abstract void ControlEnable(bool turnOff_On);
}
