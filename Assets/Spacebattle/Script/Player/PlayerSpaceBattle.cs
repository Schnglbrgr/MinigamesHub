using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSpaceBattle : MonoBehaviour
{
    [SerializeField] private MovementSpacebattle movementSpacebattle;
    [SerializeField] private AttackSpaceBattle attackSpaceBattle;


    private void Awake()
    {


    }

    private void FixedUpdate()
    {
       movementSpacebattle.Movement();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            attackSpaceBattle.Attack();
        }
    }

}
