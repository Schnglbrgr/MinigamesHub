using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UiGameOverText : MonoBehaviour
{
    public GameObject GameoverScene;
    private void Start()
    {
        GameoverScene.SetActive(false);
    }

    public void Loadscene(int scenenindex)
    {
        SceneManager.LoadScene(scenenindex);
    }
}
