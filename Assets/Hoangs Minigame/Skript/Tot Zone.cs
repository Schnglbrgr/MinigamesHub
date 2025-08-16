using UnityEngine;

public class TotZone : MonoBehaviour
{
    public GameManagerSpaceShooter GameManager;
    private void OnTriggerEnter(Collider collision)
    {
        if (GameManager.Leben > 0)
        {
            GameManager.LoseLife();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
