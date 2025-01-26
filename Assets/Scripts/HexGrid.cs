using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CodeMonkey.Utils;

public class HexGrid : MonoBehaviour
{
    private GridSystem<MapGridObject> grid;
    private MapGridObject lastGridObject;
    [SerializeField] GameObject startTransform;
    [SerializeField] int width;
    [SerializeField] int height;

    [SerializeField] List<GameObject> wallList;
    [SerializeField] List<GameObject> emptyList;

    List<MazeNode> nodes;

    public int Width { get; set; }
    public int Height { get; set; }
    public float cellSize = 5f;

    private void Awake()
    {
        Width = width; //26
        Height = height; //18
        //float cellSize = 5f; //5

        grid = new GridSystem<MapGridObject>(Width, Height, cellSize, new Vector3((Width * cellSize)/-2f,0, ((Height*cellSize)*.75f/-2f)+1 ), (GridSystem<MapGridObject> g, int x, int y) => new MapGridObject(g, x, y));

    }
    private void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        GenHexMaze maze = new GenHexMaze();
        int startX = Width / 2;//startTile.x;
        int startY = Height / 2;//startTile.y;

        nodes = maze.GenerateMaze(this, new Vector2Int(startX, startY));

        int index = 0;
        for (int j = 0; j < Height; j++)
            for (int i = 0; i < Width; i++)
            {
                index = i + (j * Width);
                MapGridObject tempObject = grid.GetGridObject(i, j);
                MazeNode tempNode = nodes[index];

                switch (tempNode.State)
                {
                    case GroundState.Wall:
                        {
                            GameObject tempTransform = Instantiate(PickATile(wallList), grid.GetWorldPosition(i, j)+ new Vector3(0,0,0), Quaternion.identity);
                            grid.GetGridObject(i, j).visualTransform = tempTransform;                            
                            break;
                        }
                    case GroundState.Start:
                        {
                            GameObject tempTransform = Instantiate(startTransform, grid.GetWorldPosition(i, j) + new Vector3(0, 0,0), Quaternion.identity);
                            grid.GetGridObject(i, j).visualTransform = tempTransform;
                            break;
                        }
                    case GroundState.Empty:
                        {
                            GameObject tempTransform = Instantiate(PickATile(emptyList), grid.GetWorldPosition(i, j) + new Vector3(0, 0, 0), Quaternion.identity);
                            grid.GetGridObject(i, j).SetTransform( tempTransform);
                            break;
                        }
                }
            }

        // Make neighbours visible
        List<Vector3Int> neighbours = grid.GetNeighbours(startX, startY);

        foreach (var n in neighbours)
        {
            MapGridObject temp = grid.GetGridObject(n.x, n.y);
        }
    }



    private GameObject PickATile(List<GameObject> list)
    {
        int count = list.Count;

        return list[Random.Range(0, count)];
    }

    public List<Vector3Int> GetCellNeighbours(int x, int y)
    {
        return grid.GetNeighbours(x, y);
    }

    public MapGridObject GetObject(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    public void SetObject(int x, int y, MapGridObject item)
    {
        grid.SetGridObject(x, y, item);
    }

    public Vector3 GetCellCenter(Vector3 position)
    {
        int x, y;
        grid.GetXY(position, out x, out y);
        return grid.GetWorldPosition(x, y);
    }

}



public class MapGridObject
{
    GridSystem<MapGridObject> grid;
    int x, y;
    public GameObject visualTransform;
    
    public MapGridObject(GridSystem<MapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void SetTransform(GameObject transform)
    {
        this.visualTransform = transform;
        grid.TriggerGridObjectChanged(x, y);
    }

    public void ClearTransform()
    {
        visualTransform = null;
    }

    public bool CanBuild()
    {
        return visualTransform == null;
    }

}

