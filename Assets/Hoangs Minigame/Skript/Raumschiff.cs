using UnityEngine;

public class Raumschiff : MonoBehaviour
{
    public float moveSpeed;
    public GameObject laserObject;
    public GameObject cannonLeft;
    public GameObject cannonRight;
    void Update()
    {
        Movement();
        Shooting();
    }


    void Movement()
    {
        Vector3 movement = Vector3.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -8, 8);
        transform.position = position;
    }


    void Shooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(laserObject, cannonRight.transform.position, laserObject.transform.rotation);
            Instantiate(laserObject, cannonLeft.transform.position, laserObject.transform.rotation);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        GameManagerSpaceShooter.instance.gameOver = true;
    }
}
