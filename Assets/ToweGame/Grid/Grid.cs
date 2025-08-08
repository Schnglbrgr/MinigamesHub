using UnityEngine;

public class Grid
{
    private int width;
    private int height = 0;
    private int length;
    private int[,] gridArray;

    public Grid(int width, int height, int length)
    {
        this.width = width;
        height = this.height;
        this.length = length;

        gridArray = new int[width, length];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                gridArray[x, z] = 0;
            }
        }
    }


    public int GetTileValue(int x, int z)
    {
        if (x > 0 && x < width && z > 0 && z < length)
            return gridArray[x, z];
        else
        {
            Debug.Log("Tile at " + x + " " + z + " is out of bounds");
            return 0;
        }
    }


    public void SetTileValue(int x, int z, int value)
    {
        if (x > 0 && x < width && z > 0 && z < length)
            gridArray[x, z] = value;
        else
        {
            Debug.Log("Tile at " + x + " " + z + " is out of bounds");
        }
    }
}
