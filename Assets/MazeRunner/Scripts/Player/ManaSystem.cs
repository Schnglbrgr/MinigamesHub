using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    [SerializeField] private Slider manaBar;
    [SerializeField] private TMP_Text manaText;

    public float mana;

    private void Start()
    {
        mana = 0;
    }

    private void Update()
    {
        CheckMana();

        manaText.text = $"{mana} / 100";
    }

    private void CheckMana()
    {
        manaBar.value = mana / 100;
    }
}
