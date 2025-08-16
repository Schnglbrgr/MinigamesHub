using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
