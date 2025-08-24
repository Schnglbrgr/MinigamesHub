using UnityEngine;

public class PlayerRsespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; //sound player while reaching checkpoint
    private Transform currentCheckpoint; //the last checkpoint is going to be stored here
    private Health playerHealth;
    private UIManager uiManager;


    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindAnyObjectByType<UIManager>();
    }

    public void CheckRespawn()
    {
        //check if checkpoint is available
        if (currentCheckpoint == null)
        {
            //show that the game is over
            uiManager.GameOver();

            return;
        }


        transform.position = currentCheckpoint.position; //move the player to ckeckpoint position
        playerHealth.Respawn();//next step restoring player health and reseting animation

        //the camera also needs to move back to the location of the last checkpoint !! the checkpoint must be placed as a child of the room object !!
        Camera.main.GetComponent<CameraControl>().MoveToNewRoom(currentCheckpoint.parent);
    }
    //Activate checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; //stores the ckeckpoint activated and the current one
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear"); //Triggers the animation of checkpoint
        }
    }
}
