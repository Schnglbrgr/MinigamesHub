using System.Collections;
using TMPro;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    private HealthSystem healthSystem;
    private TMP_Text warningText;

    private void Awake()
    {
        healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
        warningText = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerMazeRunner>().maxHealthShield;

    }

    void GiveHealth()
    {
        if (healthSystem.heartsLeft < 3)
        {
            healthSystem.heartsLeft++;
            Destroy(gameObject);
        }
        else
        {
            warningText.text = "Max Health";
            StartCoroutine(ReturnText());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GiveHealth();
        }
    }
    IEnumerator ReturnText()
    {
        yield return new WaitForSeconds(0.2f);
        warningText.text = "";
    }

}
