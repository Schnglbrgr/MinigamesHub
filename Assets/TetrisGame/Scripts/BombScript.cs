using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private float previousTime;
    private float fallTime = 0.2f;

    private GameManagerTetris gameManagerTetris;

    private void Awake()
    {
        gameManagerTetris = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerTetris>();
    }

    private void Update()
    {
        Fall();
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

                for (int y = 0; y < gameManagerTetris.gridDimension.y; y++)
                {
                    for (int x = 0; x < gameManagerTetris.gridDimension.x; x++)
                    {
                        if (gameManagerTetris.grid[x, y] != null)
                        {
                            Destroy(gameManagerTetris.grid[x, y].gameObject);
                            gameManagerTetris.grid[x, y] = null;                            
                        }
                    }
                }
             
                Destroy(gameObject);

                gameManagerTetris.SpawnNewBlock();

                Destroy(gameManagerTetris.nextPrefab);

                gameManagerTetris.NextPrefab();

                gameManagerTetris.powerUpActive = false;

                gameManagerTetris.score += 20;

            }

            previousTime = 0f;
        }
    }

}

