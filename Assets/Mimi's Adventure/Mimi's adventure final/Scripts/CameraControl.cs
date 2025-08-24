using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //Room camera
    [SerializeField] private float speed;
    private float currentPosx;
    private Vector3 velocity = Vector3.zero;

    //Follow the player camera
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraspeed;
    private float lookAhead;


    private void Update()
    {
        //Room camera (room camera turned out to be a bad experience)
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosx, transform.position.y, transform.position.z), ref velocity, speed);

        //follow the player camera
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraspeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosx = _newRoom.position.x; 
    }
}
