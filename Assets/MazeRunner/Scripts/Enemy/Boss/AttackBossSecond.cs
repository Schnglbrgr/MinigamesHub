using UnityEngine;

public class AttackBossSecond : MonoBehaviour
{
    [SerializeField] private GameObject boss;

    private BossSecond bossSecond;

    private void Awake()
    {
        bossSecond = boss.GetComponent<BossSecond>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable isDamageable = collision.gameObject.GetComponent<IDamageable>();

        if (isDamageable != null && collision.gameObject.tag != "Enemy")
        {
            bossSecond.insideRange = true;

            StartCoroutine(bossSecond.MakeDamage(isDamageable));

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IDamageable isDamageable = collision.gameObject.GetComponent<IDamageable>();

        if (isDamageable != null && collision.gameObject.tag != "Enemy")
        {
            bossSecond.insideRange = false;

            StopAllCoroutines();
        }
    }   
}
