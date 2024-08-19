using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] float movingSpeed;
    [SerializeField] float stoppingDistance;
    [SerializeField] float rotatingSpeed;
    [SerializeField] Vector3 shipYOffset;
    
    [SerializeField] int shipMove;

    private Vector3 destination;


    protected override void Awake()
    {
        base.Awake();
        destination = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        movingSpeed = 8f;
        stoppingDistance = 0.1f;
        rotatingSpeed = 10f;
        shipYOffset = new Vector3(0,3,0);

        shipMove = ship.move;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            return;
        }

        Vector3 moveDirection = (destination - transform.position).normalized;
        if(Vector3.Distance(transform.position, destination) > stoppingDistance)
        {
            transform.position += moveDirection * Time.deltaTime * movingSpeed;
        }
        else
        {
            isActive = false;
        }
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotatingSpeed);
    }

    public void Move(GridPosition gridPosition)
    {
        GridObject previousGridObject = ship.GetGridObject();
        
        
        GridObject newGridObject = MapController.Instance.GetGridObject(gridPosition);

        if(!newGridObject.GridObjectIsAvailable(ship.GetPlayerType()))
        {
            return;
        }

        SpaceWaypoint newWaypoint = newGridObject.GetAvailableSpaceWaypoint();

        if(newWaypoint == null)
        {
            return;
        }

        previousGridObject.RemoveShip(ship);
        newGridObject.AddShip(ship);
        ship.SetGridObject(newGridObject);
        ship.SetGridPosition(newGridObject.GetGridPosition());

        GetComponent<Ship>().GetCurrentWaypoint().hasShip = false;
        GetComponent<Ship>().SetCurrentWaypoint(newWaypoint);
        newWaypoint.hasShip = true;

        destination = newWaypoint.transform.position + shipYOffset;

        isActive = true;
    }

    public List<GridPosition> GetValidGridPositionList()        //update with getting neighbours list of gridposition
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();

        GridPosition currentPosition = ship.GetGridPosition();

        for(int x = -shipMove; x <= shipMove; x++)
        {
            for(int z = -shipMove; z <= shipMove; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x,z);
                GridPosition testGridPosition = currentPosition + offsetGridPosition;

                if(!MapController.Instance.IsInBounds(testGridPosition))
                {
                    //gridPosition is not in grid system
                    continue;
                }

                if(currentPosition == testGridPosition)
                {
                    //it is the current position of the unit
                    continue;
                }

                if(!MapController.Instance.GetGridObject(testGridPosition).GridObjectIsAvailable(ship.GetPlayerType()))
                {
                    //no free space for ship of this player
                    continue;
                }

                validGridPositions.Add(testGridPosition);
            }
        }
        return validGridPositions;
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositions = GetValidGridPositionList();
        return validGridPositions.Contains(gridPosition);
    }
}
