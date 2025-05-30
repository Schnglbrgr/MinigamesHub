using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaPickUp : MonoBehaviour
{
    private TMP_Text warningText;
    private ManaSystem manaSystem;

    private void Awake()
    {
        manaSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();
        warningText = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerMazeRunner>().maxHealthShield;
    }

    void GiveMana()
    {
        if (manaSystem.mana < 100)
        {
            manaSystem.mana += 10;
            Destroy(gameObject);
        }
        else
        {
            warningText.text = "Max Mana";
            StartCoroutine(ReturnText());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GiveMana();
        }
    }

    IEnumerator ReturnText()
    {
        yield return new WaitForSeconds(0.2f);
        warningText.text = "";
    }
}
