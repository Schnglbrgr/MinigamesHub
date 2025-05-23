using UnityEngine;

public class SpaceBattleBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private void FixedUpdate()
    {
        Movement();

        Destroy(gameObject, 4);
    }

    void Movement()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
