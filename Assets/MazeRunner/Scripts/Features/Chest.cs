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

    private GameObject player;
    private bool chestIsUsed;
    private bool inRange;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        chestIsUsed = false;
    }

    private void Update()
    {
        CheckDistance();
    }

    public void OpenChest(InputAction.CallbackContext context)
    {       
        if (!chestIsUsed && context.performed && inRange)
        {
            Instantiate(pickRandomWeapon.SelectRandomObject(), weaponsSpawn.position, Quaternion.identity);
            Instantiate(pickRandomItem.SelectRandomObject(), itemSpawn.position, Quaternion.identity);
            chestIsUsed = true;
        }
        else if (chestIsUsed && context.performed && inRange)
        {
            holdText.GetComponentInChildren<TMP_Text>().text = "Chest is empty";
            holdText.GetComponentInChildren<TMP_Text>().color = Color.red;
            StartCoroutine(ReturnText());
        }

    }

    private void CheckDistance()
    {
        if (Vector2.Distance(gameObject.transform.position, player.transform.position) < 3f)
        {
            holdText.SetActive(true);
            inRange = true;
        }
        else
        {
            holdText.SetActive(false);
            inRange = false;
        }
    }

    IEnumerator ReturnText()
    {
        yield return new WaitForSeconds(1f);
        holdText.GetComponentInChildren<TMP_Text>().text = "";
        holdText.GetComponentInChildren<TMP_Text>().color = Color.white;
    }

}
