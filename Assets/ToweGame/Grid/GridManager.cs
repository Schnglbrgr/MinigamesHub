using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridWidth = 5;
    [SerializeField] private int gridLength = 5;

    private Grid grid;

    private void Start()
    {
        grid = new Grid(gridWidth, 0, gridLength);
    }
}
