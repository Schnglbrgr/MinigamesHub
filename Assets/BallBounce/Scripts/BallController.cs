using UnityEngine;

public class BallController : MonoBehaviour
{
    private BallBounceGameManager gameManager;
    private BallBounceUiManager uiManager;
    private Rigidbody2D rb;

    [Header("Ball Settings")]
    [SerializeField] private float currentSpeed = 0.25f;
    [SerializeField] private float maxSpeed = 8f;
    [Space(5)]

    [SerializeField] private ParticleSystem gameOverParticle;
    private float speedIncrement = 0.25f;
    private float rangeX = 3f;
    private float directionY = 11.5f;

    void Awake()
    {        
        gameManager = FindAnyObjectByType<BallBounceGameManager>();
        uiManager = FindAnyObjectByType<BallBounceUiManager>();    
        rb = GetComponent<Rigidbody2D>();        
    }

    private void Start()
    {
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), -1f).normalized;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            BallBounce();
            gameManager.IncreaseScore();            
            
            currentSpeed += speedIncrement;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
        }

        if (collision.gameObject.CompareTag("Ground") && gameManager.lives <= 0)
        {
            Instantiate(gameOverParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameManager.EndGame();
        }
        else if(collision.gameObject.CompareTag("Ground") && gameManager.lives > 0)
        {
            BallBounce();
            gameManager.lives--;
            uiManager.UpdateScoreText();

            if(gameManager.lives <= 0)
            {
                collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }


    void BallBounce()
    {
        float bounceDirection = Random.Range(-rangeX, rangeX);
        rb.linearVelocity = new Vector2(bounceDirection * currentSpeed, directionY);
    }
}
