using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private SelectedVisual selectedVisual;

    private GridPosition gridPosition;
    private GridObject gridObject;
    [SerializeField] private SpaceWaypoint currentWaypoint;
    [SerializeField] private PlayerType playerType;

    //Stats//
    [SerializeField] public int cost;
    [SerializeField] public int move;
    [SerializeField] public int combat;
    [SerializeField] public int capacity;
    //Stats//

    private MoveAction moveAction;



    void Awake()
    {
        moveAction = GetComponent<MoveAction>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gridPosition = MapController.Instance.GetHexGridPosition(transform.position);
        MapController.Instance.AddShipToGridObject(gridPosition, this);
        gridObject = MapController.Instance.GetGridObject(gridPosition);
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

    public GridObject GetGridObject()
    {
        return gridObject;
    }
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
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

    public PlayerType GetPlayerType()
    {
        return playerType;
    }
    public void SetPlayerType(PlayerType playerType)
    {
        this.playerType = playerType;
    }

}
