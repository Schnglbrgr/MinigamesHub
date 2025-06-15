using TMPro;
using UnityEngine;
using System.Collections;

public class ShieldPickUp : MonoBehaviour, IPickable
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
        if (healthSystem.currrentShield < healthSystem.maxShield)
        {
            healthSystem.AddShield();
            Destroy(gameObject);
        }
        else
        {
            warningText.text = "Max Shield";
            StartCoroutine(ReturnText());
        }
    }
    IEnumerator ReturnText()
    {
        yield return new WaitForSeconds(0.2f);
        warningText.text = "";
    }
}
