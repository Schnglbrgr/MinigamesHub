using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound; //the sound for moving the arrow up and down
    [SerializeField] private AudioClip interactSound;//interacting with an option (selecting an option)
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        //just like regular transform, rect transform helps adjust ui or objects. In this case we wish to make the arrow marker select options. 
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //change position of the selection arrow 
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        //Interact with options
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change != 0)
            SoundManager.instance.PlaySound(changeSound);

        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;
        //moving the arrow up and down
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        //we have to have access to the option buttons
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
