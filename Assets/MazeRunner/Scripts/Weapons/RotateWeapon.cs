using UnityEngine;
using UnityEngine.InputSystem;

public class RotateWeapon : MonoBehaviour
{
    [SerializeField] private InputActionReference rotationInput;

    private PlayerInput playerInput;
    private Vector3 pointerPosition;
    private Vector3 targetRotation;

    private float angle;

    private void Awake()
    {
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    public void Rotate()
    {

        if (playerInput.currentControlScheme == "GamePad")
        {
            pointerPosition = rotationInput.action.ReadValue<Vector3>();

            angle = Mathf.Atan2(pointerPosition.y, pointerPosition.x) * Mathf.Rad2Deg;
        }
        else
        {
            pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            targetRotation = pointerPosition - transform.position;

            angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;

        }

        transform.rotation = Quaternion.Euler(new Vector3(0f,0f,angle));
 
    }
}
