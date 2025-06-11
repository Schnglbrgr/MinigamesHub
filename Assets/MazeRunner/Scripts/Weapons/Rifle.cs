using TMPro;
using UnityEngine;

public class Rifle : AttackSystem, IPickable
{
    public WeaponsSO myWeapon;
    public GameObject bullet;
    public GameObject ammoHUD;
    public Transform spawnPoint;
    private PoolManager poolManager;
    private GameObject currentBullet;
    private TMP_Text ammoText;
    private GameObject warningAmmo;

    private int currentAmmo;
    private float timer;

    private void Awake()
    {
        bullet.GetComponent<Bullet>().damage = myWeapon.damage;

        currentAmmo = myWeapon.maxAmmo;

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();

        ammoText = ammoHUD.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TMP_Text>();

        warningAmmo = ammoHUD.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;

        spawnPoint = transform.GetChild(1).GetComponent<Transform>();

    }

    private void Update()
    {
        ammoText.text = $"{currentAmmo} / {myWeapon.maxAmmo}";

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (currentAmmo <= 0)
        {
            warningAmmo.SetActive(true);
        }
        else
        {
            warningAmmo.SetActive(false);
        }

        Rotation();
    }

    public override void Shoot()
    {
        if (Input.GetMouseButton(0) && timer <= 0 && currentAmmo > 0)
        {
            timer = myWeapon.fireRate;
            currentAmmo--;

            currentBullet = poolManager.PoolInstance(bullet);
        }
    }
    public void TakeItem()
    {
        collectWeapon.TakeWeapon(gameObject);
    }
}
