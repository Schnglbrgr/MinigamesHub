using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed;

    private void OnEnable()
    {
        Destroy(gameObject, 2f); 
    }
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
