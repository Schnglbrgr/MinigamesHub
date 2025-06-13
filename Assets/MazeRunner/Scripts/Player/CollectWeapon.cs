using System.Collections;
using UnityEngine;

public class CollectWeapon : MonoBehaviour
{
    [SerializeField] private Transform gunPosition;
    [SerializeField] private Transform currentWeaponInvPosition;
    [SerializeField] private GameObject dropWeaponText;
    [SerializeField] private Transform weaponsInMap;
    [SerializeField] private GameObject inventoryFull;

    private float timer;
    private float coolDown = 0.5f;

    private Transform player;
    public GameObject currentWeapon;
    private GameObject currentWeaponInv;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        timer = 0f;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (currentWeapon != null)
        {
            DropWeapon();
        }

    }

    public void TakeWeapon(GameObject grabWeapon)
    {
        if (currentWeapon == null && timer <= 0)
        {
            currentWeapon = Instantiate(grabWeapon, gunPosition.position, Quaternion.identity, player);

            currentWeaponInv = Instantiate(grabWeapon, currentWeaponInvPosition.position, Quaternion.identity, player);

            currentWeapon.GetComponent<AttackSystem>().ControlEnable(true);

            dropWeaponText.SetActive(true);

            Destroy(grabWeapon);

        }
        else
        {
            inventoryFull.SetActive(true);

            StartCoroutine(StopWarning());
        }
    }

    private void DropWeapon()
    {
        if (Input.GetMouseButton(1))
        {
            currentWeapon.transform.SetParent(weaponsInMap);

            currentWeapon.GetComponent<AttackSystem>().ControlEnable(false);

            currentWeapon = null;

            dropWeaponText.SetActive(false);           

            timer = coolDown;

            Destroy(currentWeaponInv);

        }
    }

    IEnumerator StopWarning()
    {
        yield return new WaitForSeconds(1);
        inventoryFull.SetActive(false);
    }
}
