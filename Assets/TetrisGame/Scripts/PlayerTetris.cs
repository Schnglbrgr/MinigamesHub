using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTetris : MonoBehaviour
{
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference rotation;

    private GameManagerTetris gameManagerTetris;

    private float previousTime;
    public float fallTime;
    private float moveSpeed = 0.1f;
    private float timer = 0f;

    private void Awake()
    {
        gameManagerTetris = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerTetris>();
    }

    private void OnEnable()
    {
        rotation.action.performed += Rotation;
    }

    private void OnDisable()
    {
        rotation.action.performed -= Rotation;
    }

    private void Update()
    {
        if (timer < 0.3f)
        {
            timer += Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        if (timer >= moveSpeed)
        {
            Move(movement.action.ReadValue<Vector2>());
        }

        Fall();
    }

    private void Rotation(InputAction.CallbackContext context)
    {
        transform.Rotate(0f, 0f, 90f);

        if (!gameManagerTetris.IsValidMove(transform))
        {
            transform.Rotate(0f, 0f, -90f);
        }
    }

    void Move(Vector3 direction)
    {
        timer -= moveSpeed;

        transform.position += direction;

        if (!gameManagerTetris.IsValidMove(transform))
        {
            transform.position -= direction;
        }

    }

    void Fall()
    {
        previousTime += Time.deltaTime;

        if (previousTime >= fallTime)
        {
            transform.position += Vector3.down;

            if (!gameManagerTetris.IsValidMove(transform))
            {
                transform.position += Vector3.up;

                gameManagerTetris.AddToGrid(transform);

                gameManagerTetris.SpawnNewBlock();

                this.enabled = false;

                Destroy(gameManagerTetris.nextPrefab);

                gameManagerTetris.NextPrefab();
            }
            previousTime = 0f;
        }
    }

}
