using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{

    private ManaSystem manaSystem;  

    [SerializeField] private int health;
    [SerializeField] private int manaReward;

    private void Awake()
    {
        manaSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();
    }

    private void Update()
    {
        CheckHealth();
    }

    public void TakeDamageEnemy(int damage)
    {
        health -= damage;       
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            manaSystem.mana = Mathf.Max(manaSystem.mana + manaReward, 100);

            gameObject.SetActive(false);
        }
    }

}
