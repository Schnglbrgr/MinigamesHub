using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    public PickRandomItemSO pickRandomWeapon;
    public PickRandomItemSO pickRandomItem;

    [SerializeField] private Transform weaponsSpawn;
    [SerializeField] private Transform itemSpawn;
    [SerializeField] private GameObject holdText;
    [SerializeField] private InputActionReference openChest;

    private GameObject player;
    public bool chestIsUsed;
    private bool inRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        chestIsUsed = false;

        inRange = false;

        openChest.action.started += OpenChest;
    }

    private void Update()
    {
        CheckDistance();
    }

    public void OpenChest(InputAction.CallbackContext context)
    {       
        if (!chestIsUsed && inRange)
        {
            Instantiate(pickRandomWeapon.SelectRandomObject(), weaponsSpawn.position, Quaternion.identity);
            Instantiate(pickRandomItem.SelectRandomObject(), itemSpawn.position, Quaternion.identity);
            chestIsUsed = true;
        }

    }

    private void CheckDistance()
    {
        if (Vector2.Distance(gameObject.transform.position, player.transform.position) < 3f)
        {
            holdText.SetActive(true);

            holdText.GetComponentInChildren<TMP_Text>().text = "Hold E";

            holdText.GetComponentInChildren<TMP_Text>().color = Color.white;

            inRange = true;

            if (chestIsUsed)
            {
                holdText.GetComponentInChildren<TMP_Text>().text = "Chest is empty";
                holdText.GetComponentInChildren<TMP_Text>().color = Color.red;
            }
        }
        else
        {
            holdText.SetActive(false);

            inRange = false;

        }
    }

}
