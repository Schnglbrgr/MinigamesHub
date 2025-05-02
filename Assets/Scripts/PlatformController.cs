using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformController : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    float clampRange = 6.75f;
     
    void Update()
    {
        Movement();
    }    

    void Movement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        transform.Translate(speed * Time.deltaTime * new Vector2(moveX, 0));

        float clampedX = Mathf.Clamp(transform.position.x, -clampRange, clampRange);
        transform.position = new Vector2(clampedX, transform.position.y);      
    }
}
