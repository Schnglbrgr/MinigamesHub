using TMPro;
using UnityEngine;

public class ShieldPickUp : MonoBehaviour, IPickable
{
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
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
            Debug.Log("Full");
        }
    }

}
