using UnityEngine;

public class AmmoPickUp : MonoBehaviour, IPickable
{
    private AttackSystem attackSystem;
    private PoolManager poolManager;
    private GameObject player;

    public int ammoReward;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        attackSystem = player.GetComponent<CollectWeapon>().currentWeapon.GetComponent<AttackSystem>();

        poolManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PoolManager>();
    }

    public void TakeItem()
    {
        if (attackSystem.currentAmmo > 0)
        {
            attackSystem.currentAmmo = Mathf.Max(attackSystem.currentAmmo + ammoReward, attackSystem.maxAmmo);

            poolManager.Return(player.GetComponent<CollectWeapon>().ammoPrefab, gameObject);
        }
    }
}
