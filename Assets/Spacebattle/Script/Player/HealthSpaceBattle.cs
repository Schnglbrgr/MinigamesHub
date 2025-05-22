using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSpaceBattle : MonoBehaviour
{
    [SerializeField] private Slider hpBar;

    private TMP_Text hpText;
    private int health = 100;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = health;

        hpText = hpBar.GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        hpBar.value = currentHealth / health;

        hpText.text = $"{currentHealth} / {health}";    
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
