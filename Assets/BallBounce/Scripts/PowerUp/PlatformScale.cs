using UnityEngine;



[CreateAssetMenu(menuName = "BallBounce/PowerUps/Platformscale")]
public class PlatformScale : PowerUpEffect
{
    [SerializeField] private float xScale;


    public override void Apply(GameObject target)
    {        
        target.GetComponent<PlatformController>().SetScaleX(xScale);
    }          
}
