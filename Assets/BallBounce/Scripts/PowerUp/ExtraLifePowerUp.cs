using UnityEngine;


[CreateAssetMenu(menuName = "BallBounce/PowerUps/ExtraLife")]
public class ExtraLifePowerUp : PowerUpEffect
{
    [SerializeField] private int maxLives = 3;    

    [SerializeField] private PhysicsMaterial2D bouncyWallMaterial;


    public override void Apply(GameObject target)
    {
        GameManagerBallBounce gameManager = FindAnyObjectByType<GameManagerBallBounce>();
        GameObject ground = GameObject.FindGameObjectWithTag("Ground");
        ground.GetComponent<SpriteRenderer>().color = Color.green;
        ground.GetComponent<BoxCollider2D>().sharedMaterial = bouncyWallMaterial;
        gameManager.lives++;
        gameManager.lives = Mathf.Clamp(gameManager.lives, 0, maxLives);  
        gameManager.UpdateScoreText();
    }
}
