using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

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
