using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSystem : MonoBehaviour
{
    private Vector2 direction;
    private Rigidbody2D rb;
    public InputActionReference movement;

    public float speed = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Movement()
    {
        direction = movement.action.ReadValue<Vector2>();

        rb.AddForce(direction * speed);

    }

    
}
