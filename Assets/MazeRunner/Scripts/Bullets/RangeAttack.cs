using UnityEngine;
using System.Collections;

public class RangeAttack : MonoBehaviour
{
    public Animator animationController;

    private bool insideRange;
    public int damage;
    public float duratationAttack;

    private void Awake()
    {
        MakeAttack();
    }

    IEnumerator MakeDamage(IDamageable isDamageable)
    {
        while (insideRange)
        {
            isDamageable.TakeDamage(damage);
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator MakeAttack()
    {
        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable isDamageable = collision.gameObject.GetComponent<IDamageable>();

        if (isDamageable != null && collision.gameObject.tag != "Player")
        {
            insideRange = true;

            StartCoroutine(MakeDamage(isDamageable));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IDamageable isDamageable = collision.gameObject.GetComponent<IDamageable>();

        if (isDamageable != null && collision.gameObject.tag != "Player")
        {
            insideRange = false;

            StopAllCoroutines();
        }
    }
}
