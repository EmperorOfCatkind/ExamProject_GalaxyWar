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

    // Update is called once per frame
    void Update()
    {
        
    }
}
