using TMPro;
using UnityEngine;
using System.Collections;

public class ManaPickUp : MonoBehaviour, IPickable
{
    private ManaSystem manaSystem;
    private TMP_Text warningText;
    private AudioControllerMazeRunner audioController;


    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerMazeRunner>();

        manaSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();

        warningText = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerMazeRunner>().maxHealthShield;

    }

    public void TakeItem()
    {
        if (manaSystem.mana < 100)
        {
            manaSystem.mana += 10;
            audioController.MakeSound(audioController.collectWeapon);
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
