using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSpacebattle : MonoBehaviour
{
    public float speed = 3f;

    public float currentSpeed;

    private SpaceBattleManager spaceBattleManager;

    public InputActionReference movement;

    private Vector2 direction;

    private Rigidbody2D rb;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        currentSpeed = speed;

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        direction = movement.action.ReadValue<Vector2>();

        MovePlayer();
    }

    void MovePlayer()
    {
        rb.linearVelocity = new Vector2(direction.x * speed, direction.y * speed);
    }

    public IEnumerator StopBoostTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        currentSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickableSpaceBattle isPickAble = collision.gameObject.GetComponent<IPickableSpaceBattle>();

        if (isPickAble != null)
        {
            isPickAble.PickItem();
        }
    }
}
