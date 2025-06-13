using UnityEngine;

public class Key : MonoBehaviour, IPickable
{
    private PlayerController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void TakeItem()
    {
        player.keyInventory += 1;

        player.CheckKeys();

        Destroy(gameObject);
    }

}
