using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlantmanagerScript : MonoBehaviour
{
    [SerializeField] int PlantState = 0;
    [SerializeField] Button water;
    [SerializeField] Button sun;
    [SerializeField] Button reset;
    public Text message;
    public Text plantstate;
    bool Water = false;
    bool Sun = false;
    
    void Start()
    {

        Button Sun = sun.GetComponent<Button>();
        Sun.onClick.AddListener(OnClickSun);
        sun.interactable = true;
        Button Water = water.GetComponent<Button>();
        Water.onClick.AddListener(OnClickWater);
        water.interactable = true;
        Button Reset = reset.GetComponent<Button>();
        Reset.onClick.AddListener(OnClickReset);
    }
   
    // Update is called once per frame
    void Update()
    {
        plantstate.text = "Your Plantstate is: " + PlantState;
       
    }
    void OnClickWater()
    {
        if (Sun == false)
        {
            Water = true;
            Sun = true;
            PlantState += 1;

            message.text = "Your plant needs sunshine!";

        }
        Win();
        Lose();
    }
    void OnClickSun()
    {
        
        if (Water == true && Sun == true )
        {
            PlantState += 1;
            message.text = "Your plant is healthy!";
            Water = false;
            Sun = false;
        }
        else
        {
            PlantState -= 1;
            message.text = "Your plant needs water!";
            Sun = false;
        }
        Win();
        Lose();
    }


    void Win()
    {
        if (PlantState == 5)
        {
            message.text = "Congrets, your plant is fully grown!";
            water.interactable = false;
            sun.interactable = false;

        }
       
    }
    void Lose()
    {
        if (PlantState == -5)
        {
            message.text = "Your plant is dead.Take better care next time!";
            water.interactable = false;
            sun.interactable = false;
        }
    }
    void OnClickReset() 
    {
        PlantState = 0;
        message.text = "You have a new plant!";
        sun.interactable = true;
        water.interactable = true;

    }
}

