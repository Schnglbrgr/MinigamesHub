using UnityEngine;

public class AttackSpaceBattle : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPosition;

    public float fireRate = 0.5f;
    private float timer;

    public int damage = 1;

    public void Attack()
    {
        if (timer <= 0)
        {           
            Instantiate(bullet, spawnPosition.position, Quaternion.identity);

            timer = fireRate;
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            Attack();
        }
    }
}
