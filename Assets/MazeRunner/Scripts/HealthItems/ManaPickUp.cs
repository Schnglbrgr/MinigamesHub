using TMPro;
using UnityEngine;

public class ManaPickUp : MonoBehaviour, IPickable
{
    private ManaSystem manaSystem;

    private void Awake()
    {
        manaSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();
    }

    public void TakeItem()
    {
        if (manaSystem.mana < 100)
        {
            manaSystem.mana += 10;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Full");
        }
    }
}
