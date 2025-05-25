using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AttackEnemyMedium : MonoBehaviour
{
    private EnemyController enemyController;
    private HealthSystem playerHealthSystem;
    private GameObject player;
    private GameObject weaponPlayer;
    private Rigidbody2D rbPlayer;
    private Vector2 pushDirection;

    private int damage;
    private float pushForce = 3.5f;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealthSystem = player.GetComponent<HealthSystem>();
        damage = enemyController.damage;
        rbPlayer = player.GetComponent<Rigidbody2D>();
    }

    private void StunPlayer()
    {
        player.GetComponent<PlayerController>().enabled = false;

        if (player.GetComponent<CollectWeapon>().currentWeapon)
        {
            weaponPlayer = player.GetComponent<CollectWeapon>().currentWeapon;

            weaponPlayer.GetComponent<RotateWeapon>().enabled = false;

            weaponPlayer.GetComponent<AttackSystem>().enabled = false;
        }

        rbPlayer.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);

        StartCoroutine(StopStun());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealthSystem.TakeDamage(damage);

            pushDirection = collision.transform.position - transform.position;

            StunPlayer();
        }
    }

    IEnumerator StopStun()
    {
        yield return new WaitForSeconds(2);
        player.GetComponent<PlayerController>().enabled = true;

        if (player.GetComponent<CollectWeapon>().currentWeapon)
        {
            weaponPlayer = player.GetComponent<CollectWeapon>().currentWeapon;

            weaponPlayer.GetComponent<RotateWeapon>().enabled = true;

            weaponPlayer.GetComponent<AttackSystem>().enabled = true;
        }

    }
}
