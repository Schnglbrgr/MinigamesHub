using UnityEngine;
using System.Collections;

public class RangeAttack : MonoBehaviour
{
    private bool insideRange;
    public int damage;
    public float duratationAttack;

    private void Awake()
    {

        GetComponent<Animator>().SetBool("isHit", true);

        Invoke("StopAttack", duratationAttack);
    }

    IEnumerator MakeDamage(IDamageable isDamageable)
    {
        while (insideRange)
        {
            isDamageable.TakeDamage(damage);
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void StopAttack()
    {
        Destroy(gameObject);
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
