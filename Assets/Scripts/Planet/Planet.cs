using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private GridObject gridObject;
    private GridPosition gridPosition;
    
    [SerializeField] private int oreValue;
    [SerializeField] private int fuelValue;
    [SerializeField] private TextMeshPro oreText;
    [SerializeField] private TextMeshPro fuelText;

    [SerializeField] private SpaceDockWaypoint spaceDockWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        SetValues();
        gridPosition = gridObject.GetGridPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValues()
    {
        oreText.text = oreValue.ToString();
        fuelText.text = fuelValue.ToString();
    }

    public SpaceDockWaypoint GetSpaceDockWaypoint()
    {
        return spaceDockWaypoint;
    }

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
}
