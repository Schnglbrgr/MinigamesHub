using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private Button speed;
    [SerializeField] private Button health;
    [SerializeField] private Button shield;
    [SerializeField] private Button exit;
    [SerializeField] private GameObject powerUpsHUD;
    [SerializeField] private TMP_Text warningMana;

    private ManaSystem manaSystem;
    private MovementSystem movementSystem;
    private HealthSystem healthSystem;
    private AudioControllerMazeRunner audioController;
    public PlayerInput playerInput;

    private float speedCost = 30f;
    private float healthCost = 50f;
    private float shieldCost = 70f;

    private float currentLevelSpeed = 0;
    private float currentLevelHealth = 0;
    private float currentLevelShield = 0;

    private float currentMana;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioControllerMazeRunner>();

        manaSystem = GetComponent<ManaSystem>();

        movementSystem = GetComponent<MovementSystem>();

        healthSystem = GetComponent<HealthSystem>();

        speed.transform.GetChild(2).GetComponent<TMP_Text>().text = $"Level {currentLevelSpeed}";

        health.transform.GetChild(2).GetComponent<TMP_Text>().text = $"Level {currentLevelHealth}";

        shield.transform.GetChild(2).GetComponent<TMP_Text>().text = $"Level {currentLevelShield}";
    }

    private void Update()
    {
        currentMana = manaSystem.mana;
    }

    public void PowerUpsScreen(InputAction.CallbackContext obj)
    {
        powerUpsHUD.SetActive(true);

        playerInput.SwitchCurrentActionMap("PowerUpsScreen");

        EventSystem.current.SetSelectedGameObject(speed.gameObject);

        Time.timeScale = 0f;
    }

    public void ActivatePowerUps()
    {
        speed.onClick.AddListener(SpeedPowerUp);

        health.onClick.AddListener(HealthPowerUp);

        shield.onClick.AddListener(ShieldPowerUp);
    }

    private void SpeedPowerUp()
    {
        if (currentMana >= speedCost && currentLevelSpeed <= 3)
        {
            manaSystem.mana = Mathf.Max(manaSystem.mana - speedCost, 0);

            audioController.MakeSound(audioController.levelUp);

            currentLevelSpeed++;

            movementSystem.speed += 0.5f;

            speed.transform.GetChild(2).GetComponent<TMP_Text>().text = $"Level {currentLevelSpeed}";
        }
        else if (currentLevelSpeed > 3)
        {
            warningMana.text = "Level Max";
        }
        else
        {
            warningMana.text = "Insufficient Mana";
        }

    }

    private void HealthPowerUp()
    {
        if (currentMana >= healthCost && currentLevelHealth <= 2 && healthSystem.currentHealth < 100)
        {
            manaSystem.mana = Mathf.Max(manaSystem.mana - healthCost, 0);

            audioController.MakeSound(audioController.levelUp);

            currentLevelHealth++;

            healthSystem.AddHealth(50);

            health.transform.GetChild(2).GetComponent<TMP_Text>().text = $"Level {currentLevelHealth}";
        }
        else if (currentLevelHealth > 2)
        {
            warningMana.text = "Level Max";
        }
        else if (currentMana < healthCost)
        {
            warningMana.text = "Insufficient Mana";
        }
        else
        {
            warningMana.text = "Max Health";
        }

    }

    private void ShieldPowerUp()
    {
        if (currentMana >= shieldCost && currentLevelShield <= 3 && healthSystem.currrentShield < healthSystem.maxShield)
        {
            manaSystem.mana = Mathf.Max(manaSystem.mana - shieldCost, 0);

            audioController.MakeSound(audioController.levelUp);

            currentLevelShield++;

            healthSystem.AddShield(50);

            shield.transform.GetChild(2).GetComponent<TMP_Text>().text = $"Level {currentLevelShield}";
        }
        else if (currentLevelShield > 3)
        {
            warningMana.text = "Level Max";
        }
        else if (currentMana < shieldCost)
        {
            warningMana.text = "Insufficient Mana";
        }
        else
        {
            warningMana.text = "Max Shield";
        }

    }

    public void Exit(InputAction.CallbackContext obj)
    {
        powerUpsHUD.SetActive(false);

        playerInput.SwitchCurrentActionMap("GamePlay");

        Time.timeScale = 1f;
    }

}
