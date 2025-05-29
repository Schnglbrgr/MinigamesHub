using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformController : MonoBehaviour
{
    public float speed { get; private set; } = 8f;
    private float clampRange = 8.5f;

    private float minSpeed = 4f;
    private float maxSpeed = 14f;

    private float minScaleX = 1.5f;
    private float maxScaleX = 5.5f;

    [HideInInspector]
    public float scaleStat = 0;

    public bool isMoveable = true;
     
    void Update()
    {
        Movement();
    }    

    void Movement()
    {
        if (isMoveable)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            transform.Translate(speed * Time.unscaledDeltaTime * new Vector2(moveX, 0));

            float halfwidth = transform.localScale.x / 2f;

            float clampedX = Mathf.Clamp(transform.position.x, -clampRange + halfwidth, clampRange - halfwidth);
            transform.position = new Vector2(clampedX, transform.position.y);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed += newSpeed;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
    }

    public void SetScaleX(float newX)
    {
        Vector3 currentScale = transform.localScale;
        float newScaleX = currentScale.x + newX;
        newScaleX = Mathf.Clamp(newScaleX, minScaleX, maxScaleX);
        transform.localScale = new Vector3(newScaleX, currentScale.y, currentScale.z);
    }
}
