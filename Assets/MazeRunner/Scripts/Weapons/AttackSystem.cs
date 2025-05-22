using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private int damage;
    [SerializeField] private float coolDown;
    [SerializeField] private int maxAmmo;

    private TMP_Text ammoText;
    private Transform spawnPoint;
    private GameObject warningAmmo;
    private GameObject player;
    public GameObject ammoHUD;

    private int currentAmmo;
    private float timer;

    private void Awake()
    {
        bullet.GetComponent<Bullet>().damage = damage;

        spawnPoint = transform.GetChild(1).GetComponent<Transform>();

        player = GameObject.FindGameObjectWithTag("Player");

        currentAmmo = maxAmmo;

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

        ammoText.text = $"{currentAmmo} / {maxAmmo}";

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
            timer = coolDown;

            currentAmmo--;

            Instantiate(bullet, spawnPoint.position, player.transform.rotation);
        }
    }
}
