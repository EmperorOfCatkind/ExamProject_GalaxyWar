using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private SelectedVisual selectedVisual;

    private GridPosition gridPosition;
    [SerializeField] private SpaceWaypoint currentWaypoint;

    private MoveAction moveAction;

    void Awake()
    {
        moveAction = GetComponent<MoveAction>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gridPosition = MapController.Instance.GetHexGridPosition(transform.position);
        MapController.Instance.AddShipAtGridPosition(gridPosition, this);
        getInitialWaypoint();     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Selected()
    {
        selectedVisual.Show();
    }
    public void Deselected()
    {
        selectedVisual.Hide();
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public void SetCurrentWaypoint(SpaceWaypoint waypoint)
    {
        currentWaypoint = waypoint;
    }

    public SpaceWaypoint GetCurrentWaypoint()
    {
        return currentWaypoint;
    }

    public void getInitialWaypoint()
    {
        GridObject gridObject = MapController.Instance.GetGridObject(gridPosition);

        SpaceWaypoint nearestWaypoint = gridObject.GetSpaceWaypointsList()[0];

        foreach(var spaceWaypoint in gridObject.GetSpaceWaypointsList())
        {
            if(Vector3.Distance(transform.position, nearestWaypoint.transform.position) > Vector3.Distance(transform.position, spaceWaypoint.transform.position))
            {
                nearestWaypoint = spaceWaypoint;
            }
        }
        SetCurrentWaypoint(nearestWaypoint);
    }
}
