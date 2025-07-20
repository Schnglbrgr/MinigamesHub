using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class ElementalWeaponController : MonoBehaviour
{
    [Header ("----Components----")]
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
    public GameObject crossHair;
    public AudioControllerMazeRunner audioController;

    [Header("----Variables----")]
    public float healthWeapon;
    public int damage;
    public float durationAttack;
    public float fireRate;

    public abstract void SpecialAttack(Collision2D enemyy);

    public abstract void Attack(InputAction.CallbackContext context);

    public virtual void Rotation()
    {
        rotateWeapon.Rotate();
    }

    public abstract void HealthWeapon();

    public abstract void ControlEnable(bool turnOff_On);
}
