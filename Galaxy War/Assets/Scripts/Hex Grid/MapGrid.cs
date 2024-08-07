using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    public static MapGrid Instance {get; private set;}
    [SerializeField] private Transform coordinatesPrefab;
    private GridSystem gridSystem;

    void Awake()
    {
        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.DisplayCoordinates(coordinatesPrefab);

        if(Instance != null)
        {
            Debug.LogError("More than one MapGrid " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);
    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
    public int GetWidth() => gridSystem.GetWidth();
    public int GetHeight() => gridSystem.GetHeight();
}
