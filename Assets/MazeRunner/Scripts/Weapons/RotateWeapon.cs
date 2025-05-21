using UnityEngine;

public class RotateWeapon : MonoBehaviour
{

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector3 targetRotation = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

        float angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;

        angle = Mathf.Clamp(angle,-50f,50f);

        transform.rotation = Quaternion.Euler(new Vector3(0f,0f,angle));
    }
}
