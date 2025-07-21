using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject asteoridPrefab;
    private float timer = 2;

    bool TimerFinished()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            timer = 2;
            
            return true;
        }
        else
        {
            return false;
        }

    }
    void Update()
    {
       if (TimerFinished())
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8, 8), 8, 0);
            Instantiate(asteoridPrefab, spawnPosition, asteoridPrefab.transform.rotation);
        }
        
    }
}
