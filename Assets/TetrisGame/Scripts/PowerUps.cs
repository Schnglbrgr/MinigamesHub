using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUps : MonoBehaviour
{
    private GameManagerTetris gameManagerTetris;

    private bool isPowerUpActive = false;
    private int inventoryChangePiece = 1;


    private void Awake()
    {
        gameManagerTetris = GetComponent<GameManagerTetris>();
    }

    public void ChangePiece(InputAction.CallbackContext context)
    {

        if (!isPowerUpActive && invertoryChangePiece >= 1 && context.performed)
        {
            ChangeColorButton(0, Color.red);

            Destroy(currentPrefab);

            Destroy(nextPrefab);

            SpawnNewBlock();

            NextPrefab();

            powerUpActive = true;

            invertoryChangePiece--;
        }

    }
}
