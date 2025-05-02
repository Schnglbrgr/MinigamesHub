using UnityEngine;

public class PlayerTetris : MonoBehaviour
{
    private GameManagerTetris gameManagerTetris;

    private float timer;
    private float previousTime;
    private float fallTime = 0.5f;
    private float coolDown = 2f;

    private void Awake()
    {
        gameManagerTetris = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerTetris>();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        Movement();
        Fall();
    }


    void Movement()
    {
        if (Input.GetKey(KeyCode.A) && timer < coolDown)
        {
            Move(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D) && timer < coolDown)
        {
            Move(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.W) && timer < coolDown)
        {
            transform.Rotate(0f,0f,90f);

            if (!gameManagerTetris.IsValidMove(this.transform))
            {
                transform.Rotate(0f, 0f, -90f);
            }

            timer = coolDown;

        }
        else if (Input.GetKey(KeyCode.S) && timer < coolDown)
        {
            Move(Vector3.down);
        }
    }

    void Move(Vector3 direction)
    {
        transform.position += direction;

        if (!gameManagerTetris.IsValidMove(this.transform))
        {
            transform.position -= direction;
        }

        timer = coolDown;
    }

    void Fall()
    {
        previousTime += Time.deltaTime;

        if (previousTime >= fallTime)
        {
            transform.position += Vector3.down;

            if (!gameManagerTetris.IsValidMove(this.transform))
            {
                transform.position += Vector3.up;
                gameManagerTetris.AddToGrid(this.transform);
                gameManagerTetris.isLand = true;
                this.enabled = false;
            }
            previousTime = 0f;
        }

    }


}
