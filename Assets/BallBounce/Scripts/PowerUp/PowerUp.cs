using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect powerUpEffect;
    [SerializeField] private float fallingSpeed = 1.2f;

    private void Update()
    {
        transform.Translate(fallingSpeed * Time.deltaTime * Vector2.down);    
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            powerUpEffect.Apply(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
            Destroy(gameObject);
    }
}
