using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UiGameOverText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        if (GameManager.instance.gameOver == true && textMesh.enabled == false)
        { 
            textMesh.enabled = true;
        }

    }           
}
