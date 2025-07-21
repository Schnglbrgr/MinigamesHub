using UnityEngine;

public class Asteorid : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Rotate(new Vector3(180, 180, 0) * Time.deltaTime);
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Laser")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.score += 1;
    }
}
