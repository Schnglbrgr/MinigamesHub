using System.Collections;
using UnityEngine;

public class CollectWeapon : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Transform gunPosition;
    [SerializeField] private Transform currentWeaponInvPosition;
    [SerializeField] private GameObject dropWeaponText;
    [SerializeField] private Transform weaponsInMap;
    [SerializeField] private GameObject inventoryFull;

    private int layerWeapon = 6;
    private float timer = 0;
    private float coolDown = 0.5f;

    private Transform player;
    private GameObject grabWeapon;
    public GameObject currentWeapon;
    private GameObject currentWeaponInv;

    private string weaponName;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        DropWeapon();

        if (currentWeapon != null)
        {
            dropWeaponText.SetActive(true);
        }

    }

    private void TakeWeapon()
    {
        for (int x = 0; x < weapons.Length; x++)
        {
            if (weaponName == weapons[x].GetComponent<AttackSystem>().weapon.nameWeapon)
            {
                grabWeapon = weapons[x];
            }     
        }       

        currentWeapon = Instantiate(grabWeapon, gunPosition.position, Quaternion.identity, player);

        currentWeapon.GetComponent<RotateWeapon>().enabled = true;
        currentWeapon.GetComponent<AttackSystem>().enabled = true;

        currentWeapon.GetComponent<AttackSystem>().ammoHUD.transform.GetChild(0).gameObject.SetActive(true);

        currentWeaponInv = Instantiate(grabWeapon, currentWeaponInvPosition.position, Quaternion.identity, player);

    }

    private void DropWeapon()
    {
        if (currentWeapon != null && Input.GetMouseButton(1))
        {
            currentWeapon.transform.SetParent(weaponsInMap);

            currentWeapon.GetComponent<AttackSystem>().ammoHUD.transform.GetChild(0).gameObject.SetActive(false);

            currentWeapon.GetComponent<RotateWeapon>().enabled = false;

            currentWeapon.GetComponent<AttackSystem>().enabled = false;

            currentWeapon = null;

            dropWeaponText.SetActive(false);

            timer = coolDown;

            Destroy(currentWeaponInv);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layerWeapon && timer <= 0 && currentWeapon == null)
        {
            Destroy(collision.gameObject);

            weaponName = collision.GetComponent<AttackSystem>().weapon.name;

            TakeWeapon();
        }
        else if (collision.gameObject.layer == layerWeapon && currentWeapon != null)
        {
            inventoryFull.SetActive(true);

            StartCoroutine(StopWarning());
        }

        
    }

    IEnumerator StopWarning()
    {
        yield return new WaitForSeconds(1);
        inventoryFull.SetActive(false);
    }
}
