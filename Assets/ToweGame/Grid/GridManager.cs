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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            int tileValue = grid.GetTileValue(10, 67);
            Debug.Log(tileValue);
        }
    }
}
