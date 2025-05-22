using UnityEngine;

public class KeysCollector : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Keys")
        {
            playerController.keyInventory += 1;

            playerController.CheckKeys();

            Destroy(collision.gameObject);
        }
    }


}
