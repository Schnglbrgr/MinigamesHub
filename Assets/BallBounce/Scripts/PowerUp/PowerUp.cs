using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("PowerUp Settings")]
    [SerializeField] private float fallingSpeed = 1.2f;
    public PowerUpEffect powerUpEffect;
    [Space(5)]

    [Header("Particles")]
    [SerializeField] private GameObject particleForPlatform;
    [SerializeField] private GameObject particleForGround;

    BallBouncePoolManager poolManager;
    BallBounceGameManager gameManager;


    private void Start()
    {
        poolManager = FindAnyObjectByType<BallBouncePoolManager>();
        gameManager = FindAnyObjectByType<BallBounceGameManager>();
    }


    private void Update()
    {
        if(!gameManager.isPaused)
        transform.Translate(fallingSpeed * Time.unscaledDeltaTime * Vector2.down);    
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
