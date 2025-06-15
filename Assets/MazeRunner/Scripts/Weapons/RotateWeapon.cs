using UnityEngine;

public class RotateWeapon : MonoBehaviour
{

    public void Rotate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 targetRotation = mousePos - transform.position;

        float angle = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0f,0f,angle));

        Vector3 currentScale = transform.localScale;

        if (mousePos.x < transform.position.x)
        {
            currentScale.x *= 1f;
        }

        transform.localScale = currentScale;
 
    }
}
