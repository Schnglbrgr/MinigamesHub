using UnityEngine;

public class AttackSpaceBattle : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPosition;

    public float coolDown = 0.5f;
    private float timer;

    public void Attack()
    {
        if (timer <= 0)
        {           
            Instantiate(bullet, spawnPosition.position, Quaternion.identity);

            timer = coolDown;
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}
