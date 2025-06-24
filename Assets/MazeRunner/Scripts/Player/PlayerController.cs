using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MovementSystem movementSystem;
    [SerializeField] private CollectWeapon collectWeapon;
    [SerializeField] private TMP_Text keysText;
    [SerializeField] private PowerUps powerUps;
    [SerializeField] private TMP_Text killsInRowText;
    public PlayerInput playerInput;

    public int keyInventory;

    public int killsInRow = 0;

    private void Start()
    {
        keyInventory = 0;

        keysText.text = $"{keyInventory} / 10";

        powerUps.ActivatePowerUps();

        killsInRowText.text = $"KillStreak: {killsInRow}";

    }

    private void Update()
    {
        killsInRowText.text = $"KillStreak: {killsInRow}";
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

        IInteractive isInteractive = collision.gameObject.GetComponent<IInteractive>();

        if (isPickable != null)
        {
            isPickable.TakeItem();
        }
        else if (isInteractive != null)
        {
            isInteractive.StartInteraction();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractive isInteractive = collision.gameObject.GetComponent<IInteractive>();

        if (isInteractive != null)
        {
            isInteractive.ExitInteraction();
        }
    }

}
