using UnityEngine;

public class Key : MonoBehaviour, IPickable
{
    private PlayerController player;
    private AudioControllerMazeRunner audioController;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerMazeRunner>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void TakeItem()
    {
        player.keyInventory += 1;

        player.CheckKeys();

        audioController.MakeSound(audioController.collectWeapon);

        Destroy(gameObject);
    }

}
