using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;

    private GameManagerMazeRunner gameManagerMazeRunner;

    private int heartsLeft;

    private void Awake()
    {
        gameManagerMazeRunner = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerMazeRunner>();

        heartsLeft = hearts.Length - 2;
    }

    private void Update()
    {
        CheckHealth();
    }

    public void TakeDamage()
    {
        hearts[heartsLeft].SetActive(false);

        heartsLeft--;
    }

    private void CheckHealth()
    {
        if (heartsLeft <= 0)
        {
            gameManagerMazeRunner.Lose();
            Destroy(gameObject);
        }
    }

    public void AddHealth()
    {
        if (heartsLeft <= 2)
        {
            hearts[heartsLeft + 1].SetActive(true);

            Debug.Log("Hola");
        }
        else
        {
            heartsLeft++;

            hearts[heartsLeft].SetActive(true);

            Debug.Log("Chao");
        }
    }
   
}
