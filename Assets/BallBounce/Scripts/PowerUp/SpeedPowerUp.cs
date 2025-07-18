using UnityEngine;


[CreateAssetMenu(menuName = "BallBounce/PowerUps/Speed")]

public class SpeedPowerUp : PowerUpEffect
{
    [SerializeField] float speed;

    public override void Apply(GameObject target)
    {
        target.GetComponent<PlatformController>().SetSpeed(speed);        
    }
}
