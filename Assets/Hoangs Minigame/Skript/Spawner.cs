using UnityEngine;

public class Spawner: MonoBehaviour
{
    public GameObject Asteorid;

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
            Vector3 spawnPosition = new Vector3(Random.Range(-8, 8), 6, 0);
            Instantiate(Asteorid, spawnPosition, Asteorid.transform.rotation);
        }
    }
}
