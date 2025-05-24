using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{

    private MovementSpacebattle movementSpacebattle;

    private float speedBoost;
    private float timer = 3f;
    private float currentSpeed;

    private void Awake()
    {
        movementSpacebattle = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementSpacebattle>();

        currentSpeed = movementSpacebattle.currentSpeed;

        speedBoost = currentSpeed + 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movementSpacebattle.currentSpeed = speedBoost;
            movementSpacebattle.StopBoost(timer);
            Destroy(gameObject);
        }
    }


}
