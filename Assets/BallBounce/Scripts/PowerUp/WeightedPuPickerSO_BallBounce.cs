using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeightedPuPickerSO_BallBounce", menuName = "BallBounce/WeightedPuPickerSO_BallBounce")]
public class WeightedPuPickerSO_BallBounce : ScriptableObject
{
    public List<PowerUpEffect> powerUps;

    public PowerUpEffect GetRandomPowerUp()
    {
        int totalWeight = 0;

        for (int i = 0; i < powerUps.Count; i++)
        {
            totalWeight += powerUps[i].weight;
        }

        int randomWeight = Random.Range(0, totalWeight);
        int cumulative = 0;

        for(int i = 0; i < powerUps.Count; i++)
        {
            cumulative += powerUps[i].weight;
            if(randomWeight < cumulative)
            {
                return powerUps[i];
            }
        }
        return null;
    }
}
