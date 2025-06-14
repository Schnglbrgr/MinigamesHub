using System.Collections;
using UnityEngine;

public class MovementSpacebattle : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    public float currentSpeed;

    private SpaceBattleManager spaceBattleManager;

    private void Awake()
    {
        spaceBattleManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpaceBattleManager>();

        currentSpeed = speed;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
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
        transform.position += direction * currentSpeed * Time.deltaTime;

        if (!spaceBattleManager.InsideMap(transform))
        {
            transform.position -= direction;
        }

    }

    public void StopBoost(float timer)
    {
        StartCoroutine(StopBoostTimer(timer));
    }

    IEnumerator StopBoostTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        currentSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickable isPickAble = collision.gameObject.GetComponent<IPickable>();

        if (isPickAble != null)
        {
            isPickAble.PickItem();
        }
    }
}
