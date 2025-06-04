using UnityEngine;

public class PlayerTetris : MonoBehaviour
{
    private GameManagerTetris gameManagerTetris;

    private float previousTime;
    public float fallTime;
    private float moveSpeed = 0.2f;
    private float timer = 0f;


    private void Awake()
    {
        gameManagerTetris = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerTetris>();
    }


    private void Update()
    {
        if (timer < 0.3f)
        {
            timer += Time.deltaTime;
        }

        Movement();
        Fall();
    }


    void Movement()
    {
        if (Input.GetKey(KeyCode.A) && timer >= moveSpeed )
        {
            Move(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D) && timer >= moveSpeed)
        {
            Move(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.W) && timer >= moveSpeed )
        {
            timer -= moveSpeed;
            transform.Rotate(0f,0f,90f);

            if (!gameManagerTetris.IsValidMove(transform))
            {
                transform.Rotate(0f, 0f, -90f);
            }

        }
        else if (Input.GetKey(KeyCode.S))
        {
            //
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
