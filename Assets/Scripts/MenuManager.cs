using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject levelSelectMenu;


    private void Start()
    {
        startMenu?.SetActive(true);
        levelSelectMenu?.SetActive(false);    
    }


    public void SelectLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        //Application.Quit();
    }
}
