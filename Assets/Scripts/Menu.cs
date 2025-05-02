using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject miniGamesHub;

    private void Start()
    {
        startMenu?.SetActive(true);
        miniGamesHub?.SetActive(false);
    }

    public void LoadMiniGame(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }


    public void QuitGame()
    {
        Debug.Log("Quit");
        //Application.Quit();
    }
}
