using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Ui : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    void Start()
    {
        textMeshProUGUI = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textMeshProUGUI.text = GameManagerSpaceShooter.instance.score.ToString();
    }

}