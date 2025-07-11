using UnityEngine;

public class ElementalBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private PoolManager poolManager;
    private Rigidbody2D rb;
    public ElementalWeaponController elementalWeapon;
    public GameObject currentPrefab;
    public GameObject rangeAttack;

    public int damage;

    private void Awake()
    {
        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();

        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        rb.linearVelocity= transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable isDamageable = collision.gameObject.GetComponent<IDamageable>();

        if (isDamageable != null && collision.gameObject.tag != "Player")
        {
            isDamageable.TakeDamage(damage);

            elementalWeapon.SpecialAttack(collision);

            poolManager.Return(currentPrefab,gameObject);
        }

        if (collision.gameObject.layer == 7)
        {
            poolManager.Return(currentPrefab, gameObject);
        }
    }
}
