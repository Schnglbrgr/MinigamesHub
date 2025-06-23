using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AttackSystem : MonoBehaviour
{
    public RotateWeapon rotateWeapon;
    public CollectWeapon collectWeapon; 
    public GameObject bullet;
    public GameObject ammoHUD;
    public Transform shootPoint;
    public PoolManager poolManager;
    public TMP_Text ammoText;
    public GameObject warningAmmo;
    public WeaponsSO myWeapon;
    public GameObject currentBullet;
    public AudioControllerMazeRunner audioController;
    public InputActionReference shoot;

    public int currentAmmo;
    public float timer;
    public int maxAmmo;


    public abstract void Shoot(InputAction.CallbackContext obj);

    public virtual void Rotation()
    {
        rotateWeapon.Rotate();
    }

    public abstract void ControlEnable(bool turnOff_On);

}
