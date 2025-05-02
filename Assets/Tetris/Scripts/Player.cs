using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    private GameManager gameManager;

    private float width;
    private float height;
    private float fallTime = 0.5f;
    private float previousTime;
    private float timer;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    private void FixedUpdate()
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
        if (Input.GetKey(KeyCode.A))
        {
            MovePLayer(Vector3.left);                     
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MovePLayer(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.W) && timer < 1)
        {
            timer = 2f;
            transform.Rotate(0f, 0f, 90f);

            if (!gameManager.IsValid(transform))
            {
                transform.Rotate(0f,0f,-90f);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MovePLayer(Vector3.down);
        }
    }

    void MovePLayer(Vector3 direction)
    {
        transform.position += direction;

        if (!gameManager.IsValid(transform))
        {
            transform.position -= direction;
        }
    }

    void Fall()
    {
        if (Time.time - previousTime > fallTime)
        {
            transform.position += new Vector3(0f,-1f,0f);
            previousTime = Time.time;

            if (!gameManager.IsValid(transform))
            {
                transform.position += Vector3.up;
                gameManager.AddToGrid(transform);
                this.enabled = false;
                gameManager.IsLand = true;
            }
        }


    }







}
