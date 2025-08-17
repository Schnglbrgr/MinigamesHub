using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class  Highestscore : MonoBehaviour
{
    public Text HighestScore;

    void Start()
    {
        HighestScore.text = PlayerPrefs.GetInt("highestscore").ToString();
    }
}


