using UnityEngine;

public class MovementPowerUps : MonoBehaviour
{
    public float speed = 3f;

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
}
