using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 2f;
    public int damage;
    public GameObject prefab;

    private AudioControllerMazeRunner audioController;
    private PoolManager poolManager;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerMazeRunner>();

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();

        audioController.MakeSound(audioController.shootEnemy);
    }

    private void OnEnable()
    {
        audioController.MakeSound(audioController.shootEnemy);
    }

    private void FixedUpdate()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable isDamageable = collision.gameObject.GetComponent<IDamageable>();

        if (isDamageable != null && collision.gameObject.tag != "Enemy")
        {
            isDamageable.TakeDamage(damage);

            poolManager.Return(prefab, gameObject);
        }

        if (collision.gameObject.layer == 7)
        {
            poolManager.Return(prefab, gameObject);
        }       
    }
}
