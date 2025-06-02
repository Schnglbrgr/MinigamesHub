using System.Collections;
using TMPro;
using UnityEngine;

public class ShieldPickUp : MonoBehaviour
{
    private HealthSystem healthSystem;
    private TMP_Text warningText;

    private void Awake()
    {
        healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
        warningText = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerMazeRunner>().maxHealthShield;

    }

    void GiveShield()
    {
        if (healthSystem.shieldLeft < 3)
        {
            healthSystem.shieldLeft++;
            Destroy(gameObject);
        }
        else
        {
            warningText.text = "Max Shield";
            StartCoroutine(ReturnText());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GiveShield();
        }
    }
    IEnumerator ReturnText()
    {
        yield return new WaitForSeconds(0.2f);
        warningText.text = "";
    }
}
