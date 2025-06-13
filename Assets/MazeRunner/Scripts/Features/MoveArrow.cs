using UnityEngine;

public class MoveArrow : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Animation>().Play();
    }
}
