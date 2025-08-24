using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]private Transform previousRoom;
    [SerializeField]private Transform nextRoom;
    [SerializeField] private CameraControl cam;


    //determines which room the character wants to go to and changes from the previous room to the current one
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
                cam.MoveToNewRoom(nextRoom);
            else
                cam.MoveToNewRoom(previousRoom);
        }
    }

   

}
