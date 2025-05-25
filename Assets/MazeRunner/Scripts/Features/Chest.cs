using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public PickRandomItemSO pickRandomItem;

    [SerializeField] private Transform weaponsSpawn;
    [SerializeField] private GameObject holdText;

    private GameObject player;
    private bool isHolding;
    private bool chestIsUsed = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        CheckDistance();

        if (isHolding && !chestIsUsed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(pickRandomItem.SelectRandomWeapon(), weaponsSpawn.position, Quaternion.identity);
                chestIsUsed = true;
            }
        }
        else if (isHolding && chestIsUsed && Input.GetKeyDown(KeyCode.E))
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
            isHolding = true;
        }
        else
        {
            holdText.SetActive(false);
            isHolding = false;
        }
    }

    IEnumerator ReturnText()
    {
        yield return new WaitForSeconds(1f);
        holdText.GetComponentInChildren<TMP_Text>().text = "";
        holdText.GetComponentInChildren<TMP_Text>().color = Color.white;
    }

}
