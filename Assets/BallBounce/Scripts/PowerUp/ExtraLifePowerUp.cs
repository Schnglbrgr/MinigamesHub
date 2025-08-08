using UnityEngine;


[CreateAssetMenu(menuName = "BallBounce/PowerUps/ExtraLife")]
public class ExtraLifePowerUp : PowerUpEffect
{
    [SerializeField] private int maxLives = 3;    


    private BallBounceUiManager uiManager;
    private BallBounceGameManager gameManager;

    
    public override void Apply(GameObject target)
    {
        uiManager = FindAnyObjectByType<BallBounceUiManager>();
        gameManager = FindAnyObjectByType<BallBounceGameManager>();
        
        GameObject ground = GameObject.FindGameObjectWithTag("Ground");
        Color lastColor = ground.GetComponent<SpriteRenderer>().color;
        if(lastColor != Color.green)
        ground.GetComponent<SpriteRenderer>().color = Color.green;

        gameManager.lives++;
        gameManager.lives = Mathf.Clamp(gameManager.lives, 0, maxLives);  

        uiManager.UpdateScoreText();
    }
}
