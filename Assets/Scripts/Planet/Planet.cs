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

    [SerializeField] private PlayerType playerType;

    [SerializeField] private SpaceDockWaypoint spaceDockWaypoint;
    [SerializeField] private GroundForceWaypoint[] groundForceWaypoints;
    
    void Awake()
    {
        playerType = PlayerType.Neutral;
    }
    
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

    public GroundForceWaypoint[] GetGroundForceWaypoints()
    {
        return groundForceWaypoints;
    }
    public GroundForceWaypoint GetAvailableGroundForceWaypoint()
    {
        foreach(var waypoint in groundForceWaypoints)
        {
            if(waypoint.hasGroundForce == false)
            {
                return waypoint;
            }
            
        }
        return null;
    }

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
    public void SetOwner(PlayerType playerType)
    {
        this.playerType = playerType;
    }

    public int GetOreAmount()
    {
        return oreValue;
    }
    public int GetFuelAmount()
    {
        return fuelValue;
    }
}
