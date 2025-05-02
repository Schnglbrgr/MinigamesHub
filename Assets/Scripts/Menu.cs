using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Awake()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }


    public void QuitGame()
    {
        Debug.Log("Quit");
        //Application.Quit();
    }
}
