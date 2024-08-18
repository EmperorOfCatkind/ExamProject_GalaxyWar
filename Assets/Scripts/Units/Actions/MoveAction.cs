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

        GetComponent<Ship>().GetCurrentWaypoint().hasShip = false;
        GetComponent<Ship>().SetCurrentWaypoint(newWaypoint);
        newWaypoint.hasShip = true;


        //Debugging
        Debug.Log("Previous");
        foreach(KeyValuePair<PlayerType, List<Ship>> kvp in previousGridObject.GetShipListByPlayerType())
        {
            Debug.Log(kvp.Key);
            foreach(var ship in kvp.Value)
            {
                Debug.Log(ship);
            }
        }
        Debug.Log("New");
        foreach(KeyValuePair<PlayerType, List<Ship>> kvp in newGridObject.GetShipListByPlayerType())
        {
            Debug.Log(kvp.Key);
            foreach(var ship in kvp.Value)
            {
                Debug.Log(ship);
            }
        }
        //Debugging

        destination = newWaypoint.transform.position + shipYOffset;

        isActive = true;
    }
}
