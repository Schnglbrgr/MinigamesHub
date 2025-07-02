using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollectWeapon : MonoBehaviour
{
    [SerializeField] private Transform gunPosition;
    [SerializeField] private Transform currentWeaponInvPosition;   
    [SerializeField] private Transform weaponsInMap;
    [SerializeField] private GameObject inventoryFull;
    [SerializeField] private InputActionReference shoot;

    private float timer;
    private float coolDown = 0.5f;

    private Transform player;
    public GameObject currentWeapon;
    private GameObject currentWeaponInv;
    public GameObject bulletPrefab;
    public GameObject ammoPrefab;
    public GameObject dropWeaponText;

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

    }

    public void TakeWeapon(GameObject grabWeapon)
    {
        if (currentWeapon == null && timer <= 0)
        {
            currentWeapon = Instantiate(grabWeapon, gunPosition.position, Quaternion.identity, player);

            currentWeaponInv = Instantiate(grabWeapon, currentWeaponInvPosition.position, Quaternion.identity, player);

            if (currentWeapon.GetComponent<AttackSystem>() != null)
            {
                currentWeapon.GetComponent<AttackSystem>().ControlEnable(true);

                shoot.action.performed += currentWeapon.GetComponent<AttackSystem>().Shoot;
            }
            else if (currentWeapon.GetComponent<ElementalWeaponController>() != null)
            {
                currentWeapon.GetComponent<ElementalWeaponController>().ControlEnable(true);

                shoot.action.performed += currentWeapon.GetComponent<ElementalWeaponController>().Attack;
            }

            dropWeaponText.SetActive(true);

            Destroy(grabWeapon);

        }
        else
        {
            inventoryFull.SetActive(true);

            StartCoroutine(StopWarning());
        }
    }

    public void DropWeapon(InputAction.CallbackContext context)
    {
        if (currentWeapon != null && context.performed)
        {
            currentWeapon.transform.SetParent(weaponsInMap);

            if (currentWeapon.GetComponent<AttackSystem>() != null)
            {
                currentWeapon.GetComponent<AttackSystem>().ControlEnable(false);

                shoot.action.performed -= currentWeapon.GetComponent<AttackSystem>().Shoot;

            }
            else if (currentWeapon.GetComponent<ElementalWeaponController>() != null)
            {
                currentWeapon.GetComponent<ElementalWeaponController>().ControlEnable(false);

                shoot.action.performed -= currentWeapon.GetComponent<ElementalWeaponController>().Attack;

            }

            currentWeapon = null;

            dropWeaponText.SetActive(false);

            timer = coolDown;

            DestroyWeaponInv();
        }
    }

    public void DestroyWeaponInv()
    {
        Destroy(currentWeaponInv);
    }

    IEnumerator StopWarning()
    {
        yield return new WaitForSeconds(1);
        inventoryFull.SetActive(false);
    }
}
