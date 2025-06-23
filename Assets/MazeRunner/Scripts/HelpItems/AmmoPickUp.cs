using UnityEngine;

public class AmmoPickUp : MonoBehaviour, IPickable
{
    private AttackSystem attackSystem;
    private PoolManager poolManager;
    private GameObject player;
    private AudioControllerMazeRunner audioController;

    public int ammoReward;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerMazeRunner>();

        player = GameObject.FindGameObjectWithTag("Player");

        attackSystem = player.GetComponent<CollectWeapon>().currentWeapon.GetComponent<AttackSystem>();

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();
    }

    public void TakeItem()
    {
        attackSystem.currentAmmo += ammoReward;

        audioController.MakeSound(audioController.collectWeapon);

        poolManager.Return(player.GetComponent<CollectWeapon>().ammoPrefab, gameObject);
    }
}
