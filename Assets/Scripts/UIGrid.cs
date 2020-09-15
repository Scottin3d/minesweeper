
using UnityEngine;
using CodeMonkey.Utils;


public class UIGrid {

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;

    private int[,] gridArray;
    private TextMesh[,] debugTextArray;

    public UIGrid(int _width, int _height, float _celZise, Vector3 _originPosition) {
        this.width = _width;
        this.height = _height;
        this.cellSize = _celZise;
        this.originPosition = _originPosition;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        // GetLength return the length of the dimension given
        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {
                debugTextArray[x,y] = UtilsClass.CreateWorldText(gridArray[x,y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }

        // draw the end lines
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        SetValue(2,1,56);
    }

    private Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y) {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetValue(Vector3 worldPosition, int value) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public void SetValue(int x, int y, int targetValue) {
        // grid validation
        if (x >= 0 && y >= 0 && x < width && y < height ) {
            gridArray[x, y] = targetValue;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }

    public int GetValue(int x, int y) {
        // grid validation
        if (x >= 0 && y >= 0 && x < width && y < height) {
            return gridArray[x, y];
        } else {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition) {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
}
