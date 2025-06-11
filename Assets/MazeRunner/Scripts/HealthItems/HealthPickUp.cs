using TMPro;
using UnityEngine;

public class HealthPickUp : MonoBehaviour, IPickable
{
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
    }

    public void TakeItem()
    {
        if (healthSystem.currentHealth < healthSystem.maxHealth)
        {
            healthSystem.AddHealth();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Full");
        }
    }
}
