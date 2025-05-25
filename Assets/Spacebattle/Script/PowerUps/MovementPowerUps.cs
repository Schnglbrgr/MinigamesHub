using System.Collections;
using UnityEngine;

public class MovementPowerUps : MonoBehaviour
{
    public float speed = 3f;

    private GameObject player;
    public GameObject prefab;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void FixedUpdate()
    {
        Movement();

        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

}
