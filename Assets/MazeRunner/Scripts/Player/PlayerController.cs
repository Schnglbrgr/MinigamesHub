using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MovementSystem movementSystem;
    [SerializeField] private CollectWeapon collectWeapon;
    [SerializeField] private TMP_Text keysText;
    [SerializeField] private PowerUps powerUps;
    [SerializeField] private Teleport teleport;

    private GameManagerMazeRunner gameManagerMazeRunner;

    public int keyInventory;

    private void Awake()
    {
        gameManagerMazeRunner = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerMazeRunner>();
    }

    private void Start()
    {
        keyInventory = 0;

        keysText.text = $"{keyInventory} / 10";

        powerUps.ActivatePowerUps();

    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.F))
        {
            powerUps.PowerUpsScreen();
        }

        if (teleport.standingTime >= 3)
        {
            teleport.TeleportPlayer();
        }

    }

    private void FixedUpdate()
    {
        movementSystem.Movement();
    }

    public void CheckKeys()
    {
        keysText.text = $"{keyInventory} / 10";
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPickable isPickable = collision.gameObject.GetComponent<IPickable>();

        if (isPickable != null)
        {
            isPickable.TakeItem();
        }

        if (collision.gameObject.tag == "Door")
        {
            gameManagerMazeRunner.Win(keyInventory);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            gameManagerMazeRunner.warningMessage.SetActive(false);
        }
    }

}
