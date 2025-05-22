using UnityEngine;

public class BallController : MonoBehaviour
{
    GameManagerBallBounce gameManager;
    Rigidbody2D rb;
    [SerializeField] float speed = 0.5f;
    [SerializeField] float maxSpeed = 8f;
    private float rangeX = 3f;
    private float directionY = 11.5f;


    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManagerBallBounce>();
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
            
            speed += 0.5f;
            speed = Mathf.Clamp(speed, 0f, maxSpeed);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            gameManager.EndGame();
        }
    }


    void BallBounce()
    {
        float bounceDirection = Random.Range(-rangeX, rangeX);
        rb.linearVelocity = new Vector2(bounceDirection * speed, directionY);
    }
}
