using UnityEngine;

public class BallController : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody2D rb;
    [SerializeField] float ballSpeed = 1f;
    [SerializeField] float directionY = 11f;
    float rRange = 2f;


    void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        rb = GetComponent<Rigidbody2D>();        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            gameManager.IncreaseScore();
            BallBounce();
            if (ballSpeed >= 10) {
                ballSpeed = 10;
                return; }
            ballSpeed++;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            gameManager.EndGame();
        }
    }


    void BallBounce()
    {
        float bounceDirection = Random.Range(-rRange, rRange);
        rb.linearVelocity = new Vector2(bounceDirection * ballSpeed, directionY);
    }
}
