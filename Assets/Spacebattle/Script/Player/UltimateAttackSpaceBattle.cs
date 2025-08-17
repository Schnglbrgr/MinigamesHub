using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UltimateAttackSpaceBattle : MonoBehaviour
{
    [SerializeField] private Slider ultimateBar;
    [SerializeField] private TMP_Text ultimateText;

    private AttackSpaceBattle attackSpaceBattle;
    private MovementSpacebattle movementSpacebattle;
    private GameObject player;
    private Color currentColor;

    public int ultimateCharge;
    public bool ultimateActive;
    private int bonusDamage = 3;
    private float bonusFireRate = 0.5f;
    private float bonusSpeed = 5f;

    private void Awake()
    {
        attackSpaceBattle = GetComponent<AttackSpaceBattle>();

        movementSpacebattle = GetComponent<MovementSpacebattle>();

        player = GameObject.FindGameObjectWithTag("Player");

        currentColor = player.GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        ultimateBar.value = ultimateCharge / 100;

        ultimateText.text = $"{ultimateCharge} / 100";
    }

    public void UltimateAttack(InputAction.CallbackContext context)
    {
        if (ultimateCharge >= 100 && context.performed)
        {
            ultimateActive = true;

            ultimateCharge = 0;

            attackSpaceBattle.currentDamage = bonusDamage;

            attackSpaceBattle.currentFireRate = bonusFireRate;

            movementSpacebattle.currentSpeed = bonusSpeed;

            player.GetComponent<SpriteRenderer>().color = Color.blue;

            StartCoroutine(ReturnValues());
        }
    }

    IEnumerator ReturnValues()
    {
        yield return new WaitForSeconds(5f);

        ultimateActive = false;

        attackSpaceBattle.currentDamage = attackSpaceBattle.damage;

        attackSpaceBattle.currentFireRate = attackSpaceBattle.fireRate;

        movementSpacebattle.currentSpeed = movementSpacebattle.speed;

        player.GetComponent<SpriteRenderer>().color = currentColor;

    }
}
