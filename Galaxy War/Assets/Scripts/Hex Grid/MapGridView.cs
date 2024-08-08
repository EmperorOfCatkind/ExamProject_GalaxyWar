using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridView : MonoBehaviour
{
    public static MapGridView Instance {get; private set;}
    [SerializeField] private Transform hexTilePrefab;

    private MapGridViewSingle[,] mapGridViewSingleArray;

    void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("More than one MapGridView " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeGridView();
        HideAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeGridView()
    {
        int x = MapGrid.Instance.GetWidth();
        int z = MapGrid.Instance.GetHeight();
        mapGridViewSingleArray = new MapGridViewSingle[x, z];
        for(int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++){
                GridPosition gridPosition = new GridPosition(i, j);

                Transform mapGridViewTransform = Instantiate(hexTilePrefab, MapGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                mapGridViewSingleArray[i,j] = mapGridViewTransform.GetComponent<MapGridViewSingle>();
            }
        }
    }

    public void HideAll()
    {
        int x = MapGrid.Instance.GetWidth();
        int z = MapGrid.Instance.GetHeight();
        for(int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++){
                mapGridViewSingleArray[i,j].Hide();
            }
        }
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => MapGrid.Instance.GetGridPosition(worldPosition);
    public GridPosition GetHexGridposition(Vector3 worldPosition) => MapGrid.Instance.GetHexGridPosition(worldPosition);
    public int GetWidth() => MapGrid.Instance.GetWidth();
    public int GetHeight() => MapGrid.Instance.GetHeight();
    public bool IsInBounds(GridPosition gridPosition) => MapGrid.Instance.IsInBounds(gridPosition);


    public MapGridViewSingle GetMapGridViewSingle(GridPosition gridPosition)
    {
        int width = GetWidth();
        int height = GetHeight();
        if(IsInBounds(gridPosition))
        {
            return mapGridViewSingleArray[gridPosition.x, gridPosition.z];
        }
        return null;
    }
}
