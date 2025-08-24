using UnityEngine;

public class Gate : MonoBehaviour, IInteractive
{
    private GameManagerMazeRunner gameManagerMazeRunner;

    private void Awake()
    {
        gameManagerMazeRunner = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerMazeRunner>();
    }

    public void StartInteraction()
    {
        gameManagerMazeRunner.StartBoss();
    }

    public void ExitInteraction()
    {
        gameManagerMazeRunner.warningMessage.SetActive(false);
    }
}
