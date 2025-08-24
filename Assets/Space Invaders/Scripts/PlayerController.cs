using UnityEngine;

public class SpaceInvadersPlayerController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float hInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");


        transform.Translate(Vector2.right * hInput * moveSpeed * Time.deltaTime);
    }
}
