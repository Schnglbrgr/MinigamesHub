using UnityEngine;

public abstract class AttackSystem : MonoBehaviour
{
    public RotateWeapon rotateWeapon;
    public CollectWeapon collectWeapon;

    public abstract void Shoot();

    public virtual void Rotation()
    {
        rotateWeapon.Rotate();
    }

}
