using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    private Vector2 direction;
    private Rigidbody2D rb;

    private float movementX;
    private float movementY;
    public float speed = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void Movement()
    {
        movementX = Input.GetAxis("Horizontal");
        movementY = Input.GetAxis("Vertical");

        direction = new Vector2(movementX,movementY);

        rb.AddForce(direction * speed);

    }

    
}
