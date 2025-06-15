using UnityEngine;

public class EnemyMedium : EnemyController
{
    private GameObject player;

    private void Awake()
    {
        currentHealth = enemy.health;

        damage = enemy.damage;

        manaReward = enemy.mana;

        speed = enemy.speed;

        player = GameObject.FindGameObjectWithTag("Player");

        manaSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ManaSystem>();

        hpBar.value = currentHealth / enemy.health;

        hpText.text = $"{currentHealth} / {enemy.health}";
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public override void Movement()
    {
        if (CheckPositionPlayer())
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    public override void Attack()
    {  
    }

    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;

        hpBar.value = currentHealth / enemy.health;

        hpText.text = $"{currentHealth} / {enemy.health}";

        CheckHealth();

        GetComponent<Animation>().Play();
    }

    public override void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            if (manaSystem.mana < 100)
            {
                manaSystem.mana += manaReward;
            }

            Instantiate(dropRandomItem.SelectRandomObject(), spawnItem.position, Quaternion.identity);

            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable isDamageable = collision.gameObject.GetComponent<IDamageable>();

        if (isDamageable != null && collision.gameObject.tag != "Enemy")
        {
            isDamageable.TakeDamage(damage);
        }
    }

    private bool CheckPositionPlayer()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 4.5f)
        {
            return true;
        }
        return false;
    }

}
