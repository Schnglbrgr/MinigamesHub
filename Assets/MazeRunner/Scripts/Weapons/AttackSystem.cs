using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    public WeaponsSO weapon;
    private TMP_Text ammoText;
    private Transform spawnPoint;
    private GameObject warningAmmo;
    private GameObject player;
    public GameObject ammoHUD;

    private int currentAmmo;
    private float timer;

    private void Awake()
    {
        bullet.GetComponent<Bullet>().damage = weapon.damage;

        spawnPoint = transform.GetChild(1).GetComponent<Transform>();

        player = GameObject.FindGameObjectWithTag("Player");

        currentAmmo = weapon.maxAmmo;

        ammoText = ammoHUD.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TMP_Text>();

        warningAmmo = ammoHUD.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;

    }

    private void Update()
    {

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        Shoot();

        ammoText.text = $"{currentAmmo} / {weapon.maxAmmo}";

        if (currentAmmo <= 0)
        {
            warningAmmo.SetActive(true);
        }
        else
        {
            warningAmmo.SetActive(false);
        }

    }


    private void Shoot()
    {
        if (Input.GetMouseButton(0) && timer <= 0 && currentAmmo > 0)
        {
            timer = weapon.fireRate;

            currentAmmo--;

            Instantiate(bullet, spawnPoint.position, player.transform.rotation);
        }
    }
}
