using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSpaceBattle : MonoBehaviour, IDamageableSpaceBattle
{
    [SerializeField] private Slider hpBar;

    private TMP_Text hpText;
    private AudioControllerSpaceBattle audioController;
    private UltimateAttackSpaceBattle ultimate;

    private int health = 100;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = health;

        hpText = hpBar.GetComponentInChildren<TMP_Text>();

        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerSpaceBattle>();

        ultimate = GetComponent<UltimateAttackSpaceBattle>();
    }

    private void OnDisable()
    {
        currentHealth = health;
    }

    private void Update()
    {
        hpBar.value = currentHealth / health;

        hpText.text = $"{currentHealth} / {health}";    
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>().EndGame();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        CheckHealth();

        GetComponent<Animation>().Play();

        audioController.MakeSound(audioController.getHit);

        if (ultimate.ultimateCharge > 0)
        {
            ultimate.ultimateCharge -= 10;
        }
    }

}
