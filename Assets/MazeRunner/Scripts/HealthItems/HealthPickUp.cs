using TMPro;
using UnityEngine;
using System.Collections;

public class HealthPickUp : MonoBehaviour, IPickable
{
    private HealthSystem healthSystem;
    private TMP_Text warningText;

    private void Awake()
    {
        healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();

        warningText = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerMazeRunner>().maxHealthShield;
    }

    public void TakeItem()
    {
        if (healthSystem.currentHealth < healthSystem.maxHealth)
        {
            healthSystem.AddHealth(10);
            Destroy(gameObject);
        }
        else
        {
            warningText.text = "Max Health";
            StartCoroutine(ReturnText());
        }
    }

    IEnumerator ReturnText()
    {
        yield return new WaitForSeconds(0.2f);
        warningText.text = "";
    }
}
