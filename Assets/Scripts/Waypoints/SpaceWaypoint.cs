using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWaypoint : MonoBehaviour
{
    private GridPosition gridPosition;
    private GridObject gridObject;
    public bool hasShip;

    void Awake()
    {
        hasShip = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        gridPosition = gridObject.GetGridPosition();
        //Debug.Log(gridObject.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
}
