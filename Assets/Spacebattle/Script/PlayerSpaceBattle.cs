using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSpaceBattle : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform canonPosition;

    private SpaceBattleManager spaceBattleManager;

    private float delay = 0.5f;
    private float timer;
    public int health = 100;
    public int currentHealth;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        currentHealth = health;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        Attack();

    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MovePlayer(Vector3.up);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MovePlayer(Vector3.down);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(Vector3.left);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(Vector3.right);
        }
    }

    void MovePlayer(Vector3 direction)
    {       
        transform.position += direction * speed * Time.deltaTime;

        if (!spaceBattleManager.InsideMap(transform))
        {
            transform.position -= direction;
        }

    }

    void Attack()
    {
        if (Input.GetMouseButton(0) && timer <= 0)
        {
            Shooting();
        }
    }

    void Shooting()
    {
        timer = delay;
        Instantiate(bullet, canonPosition.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentHealth -= 20;
    }


}
