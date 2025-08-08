using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "BallBounce/PowerUps/SlowMotion")]
public class SlowMotionPowerUp : PowerUpEffect
{
    [SerializeField] private float powerUpDuration = 3f;

    public override void Apply(GameObject target)
    {
        CoroutineRunner.Instance.StartCoroutine(SlowMotion());
    }

            
    private IEnumerator SlowMotion()
    {        
        BallBounceGameManager gameManager = FindAnyObjectByType<BallBounceGameManager>();
        Image slowMoIcon = GameObject.Find("UI")?.transform.Find("SlowMoIcon")?.GetComponent<Image>();        

        slowMoIcon.gameObject.SetActive(true);
        slowMoIcon.fillAmount = 1f;

        float startTime = Time.realtimeSinceStartup;
        float totalPausedTime = 0f;
        float pauseStartTime = 0f;


        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        while (true)
        {
            if (gameManager.isPaused)
            {

                pauseStartTime = Time.realtimeSinceStartup;

                while (gameManager.isPaused)
                {
                    yield return null;
                }

                totalPausedTime += Time.realtimeSinceStartup - pauseStartTime;

            }

            float currentTime = Time.realtimeSinceStartup;
            float adjustedElapsed = currentTime - startTime - totalPausedTime;

            float remaining = powerUpDuration - adjustedElapsed;
            if(slowMoIcon != null)
            {
                slowMoIcon.fillAmount = remaining / powerUpDuration;
            }

            if (adjustedElapsed >= powerUpDuration)
            {
                break;
            }
            yield return null;
        }

        slowMoIcon.gameObject.SetActive(false);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;        
        }
}
