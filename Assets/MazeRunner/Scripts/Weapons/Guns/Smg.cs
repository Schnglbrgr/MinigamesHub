using TMPro;
using UnityEngine;

public class Smg : AttackSystem, IPickable
{
    private void Awake()
    {
        bullet.GetComponent<Bullet>().damage = myWeapon.damage;

        currentAmmo = myWeapon.maxAmmo;

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();

        ammoText = ammoHUD.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TMP_Text>();

        warningAmmo = ammoHUD.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;

        spawnPoint = transform.GetChild(1).GetComponent<Transform>();

        collectWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<CollectWeapon>();

        timer = 0f;

    }
    private void OnEnable()
    {
        ammoHUD.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        ammoHUD.transform.GetChild(0).gameObject.SetActive(true);
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

        Shoot();
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

    public  override void ControlEnable(bool turnOff_On)
    {
        this.enabled = turnOff_On;
    }
}
