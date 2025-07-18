using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Smg : AttackSystem, IPickable
{
    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerMazeRunner>();

        maxAmmo = myWeapon.maxAmmo;

        currentAmmo = maxAmmo;

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();

        ammoText = ammoHUD.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TMP_Text>();

        warningAmmo = ammoHUD.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;

        collectWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<CollectWeapon>();

        timer = 0f;

    }

    private void OnEnable()
    {
        ammoHUD.transform.GetChild(0).gameObject.SetActive(true);

        crossHair.SetActive(true);
    }

    private void OnDisable()
    {
        ammoHUD.transform.GetChild(0).gameObject.SetActive(false);

        crossHair.SetActive(false);
    }

    private void Update()
    {
        ammoText.text = $"{currentAmmo} / {maxAmmo}";

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

    public override void Shoot(InputAction.CallbackContext obj)
    {
        if (timer <= 0 && currentAmmo > 0)
        {
            audioController.MakeSound(audioController.shootPlayer);

            timer = myWeapon.fireRate;

            currentAmmo--;

            currentBullet = poolManager.PoolInstance(bullet);

            currentBullet.transform.position = shootPoint.position;

            currentBullet.transform.rotation = gameObject.transform.rotation;

            currentBullet.GetComponent<Bullet>().damage = myWeapon.damage;

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
