using UnityEngine;

public abstract class PowerUpEffect : ScriptableObject
{
    public int weight = 1;

    public GameObject powerUpPrefab;

    public abstract void Apply(GameObject target);   
}
