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

    public void TakeWeapon(GameObject grabWeapon)
    {  
        currentWeapon = Instantiate(grabWeapon, gunPosition.position, Quaternion.identity, player);

        currentWeaponInv = Instantiate(grabWeapon, currentWeaponInvPosition.position, Quaternion.identity, player);
    }

    private void DropWeapon()
    {
        if (currentWeapon != null && Input.GetMouseButton(1))
        {
            currentWeapon.transform.SetParent(weaponsInMap);

            currentWeapon = null;

            dropWeaponText.SetActive(false);

            timer = coolDown;

            Destroy(currentWeaponInv);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
             
    }

    IEnumerator StopWarning()
    {
        yield return new WaitForSeconds(1);
        inventoryFull.SetActive(false);
    }
}
