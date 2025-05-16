using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float speed = 1.5f;
    [SerializeField] float powerUpTime = 3f;

    void Start()
    {

    }
    
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            StartCoroutine(PowerUpTimer());
            Time.timeScale = 1f;
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Ground"))
            Destroy(gameObject);
    }


    IEnumerator PowerUpTimer()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(powerUpTime);
    }
}
