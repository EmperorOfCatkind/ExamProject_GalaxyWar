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
    private List<GridPosition> availablePostionsToMove;


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

        availablePostionsToMove = new List<GridPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Selected()
    {
        selectedVisual.Show();
        ShowHexesForMove();
    }
    public void Deselected()
    {
        selectedVisual.Hide();
        HideHexesForMove();
    }

    public GridObject GetGridObject()
    {
        return gridObject;
    }
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    public void SetGridPosition(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
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


    public void ShowHexesForMove()
    {
        availablePostionsToMove = moveAction.GetValidGridPositionList();
        
        foreach(var gridPosition in availablePostionsToMove)
        {
            MapController.Instance.GetMapGridViewSingle(gridPosition).ShowAsAvailable();
            Debug.Log(gridPosition.ToString());
        }
    }

    public void HideHexesForMove()
    {
        foreach(var gridPosition in availablePostionsToMove)
        {
            MapController.Instance.GetMapGridViewSingle(gridPosition).HideAsAvailable();
        }
        availablePostionsToMove.Clear();
        Debug.Log(availablePostionsToMove);
    }
}
