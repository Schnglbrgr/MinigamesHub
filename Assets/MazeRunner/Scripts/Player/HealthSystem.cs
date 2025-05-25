using System.Collections;
using TMPro;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameObject[] shield;

    private GameManagerMazeRunner gameManagerMazeRunner;
    private Color currentColor;
    private Transform child;
    public int heartsLeft;
    public int shieldLeft;

    private void Awake()
    {
        gameManagerMazeRunner = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerMazeRunner>();

        heartsLeft = hearts.Length - 2;

        shieldLeft = shield.Length;

        child = transform.GetChild(1);

        currentColor = child.GetChild(1).GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        CheckHealth();
    }

    public void TakeDamage(int damage)
    {
        child = transform.GetChild(1);

        child.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;

        if (shieldLeft <= 0)
        {
            hearts[heartsLeft].SetActive(false);
            heartsLeft -= damage;
        }
        else
        {
            shield[shieldLeft].SetActive(false);
            shieldLeft -= damage;
        }

        StartCoroutine(ReturnColor());
    }

    private void CheckHealth()
    {
        if (heartsLeft <= 0)
        {
            gameManagerMazeRunner.Lose();
            Destroy(gameObject);
        }
    }

    public void AddShield()
    {
        shieldLeft++;

        shield[shieldLeft].SetActive(true);
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

            hearts[heartsLeft - 1].SetActive(true);

            Debug.Log("Chao");
        }
    }

    IEnumerator ReturnColor()
    {
        yield return new WaitForSeconds(0.2f);
        child.GetChild(1).GetComponent<SpriteRenderer>().color = currentColor;
    }
   
}
