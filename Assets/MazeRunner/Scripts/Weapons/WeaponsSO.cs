using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsSO", menuName = "Scriptable Objects/WeaponsSO")]
public class WeaponsSO : ScriptableObject
{
    public string nameWeapon;
    public int damage;
    public float fireRate;
    public int maxAmmo;
}
