using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float fallingSpeed = 1.2f;
    [SerializeField] private ParticleSystem particleForPlatform;
    [SerializeField] private ParticleSystem particleForGround;
    public PowerUpEffect powerUpEffect;
    BallBouncePoolManager poolManager;


    private void Start()
    {
          poolManager = FindAnyObjectByType<BallBouncePoolManager>();
    }


    private void Update()
    {
        transform.Translate(fallingSpeed * Time.deltaTime * Vector2.down);    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Instantiate(particleForPlatform, transform.position, Quaternion.identity);
            powerUpEffect.Apply(collision.gameObject);            
            poolManager.Return(powerUpEffect.powerUpPrefab, gameObject);            
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(particleForGround, transform.position, Quaternion.identity);
            poolManager.Return(powerUpEffect.powerUpPrefab, gameObject);            
        }
    }
}
