using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private SelectedVisual selectedVisual;
    [SerializeField] private Material spentSelectedVisual;

    private GridPosition gridPosition;
    private GridObject gridObject;
    [SerializeField] private SpaceWaypoint currentWaypoint;
    [SerializeField] private PlayerType playerType;
    [SerializeField] private TextMeshPro embarkedCounter;
    [SerializeField] private TextMeshPro combatRoll;

    //Default Stats//
    [SerializeField] public int cost;
    [SerializeField] public int move;
    [SerializeField] public int combat;
    [SerializeField] public int capacity;
    //Default Stats//

    //Temporary Stats//
    public bool isSelected;
    private List<GroundForce> embarkedForces;
    //Temporary Stats//

    private MoveAction moveAction;
    private EmbarkAction embarkAction;
    private DisembarkAction disembarkAction;
    private List<GridPosition> availablePostionsToMove;

    [SerializeField] private MeshRenderer monitor;
    [SerializeField] private Material monitorMaterial;


    void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        embarkAction = GetComponent<EmbarkAction>();
        disembarkAction = GetComponent<DisembarkAction>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gridPosition = MapController.Instance.GetHexGridPosition(transform.position);
        MapController.Instance.AddShipToGridObject(gridPosition, this);
        gridObject = MapController.Instance.GetGridObject(gridPosition);

        availablePostionsToMove = new List<GridPosition>();
        embarkedForces = new List<GroundForce>();

        combatRoll.text = "";
        monitor.material = monitorMaterial;
    }

    // Update is called once per frame
    void Update()
    {

    }

   
    public void SelectedToDestroy()
    {
        isSelected = true;
        selectedVisual.Show();
    }
    public void Selected()
    {
        isSelected = true;
        selectedVisual.Show();
        if(PlayerTurnController.Instance.GetActivePlayer().GetFuel() > 0)
        {
            ShowHexesForMove();
        }
        
    }
    public void Deselected()
    {
        isSelected = false;
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
    public EmbarkAction GetEmbarkAction()
    {
        return embarkAction;
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
            
        }
    }

    public void HideHexesForMove()
    {
        foreach(var gridPosition in availablePostionsToMove)
        {
            MapController.Instance.GetMapGridViewSingle(gridPosition).HideAsAvailable();
        }
        availablePostionsToMove.Clear();
        
    }

    public void Embark(GroundForce groundForce)
    {
        Debug.Log(embarkedForces.Count);
        Debug.Log(capacity);
        if(embarkedForces.Count == capacity)
        {
            return;
        }

        embarkedForces.Add(groundForce);
        
        int count = embarkedForces.Count;
        embarkedCounter.text = count.ToString();

        Destroy(groundForce.GameObject());
    }

    public void SetRollText(int roll)
    {
        if(roll < combat)
        {
            combatRoll.color = Color.red;
        }
        else if(roll >= combat)
        {
            combatRoll.color = Color.green;
        }
        
        combatRoll.text = roll.ToString();
    }

    public void ResetRollText()
    {
        combatRoll.text = "";
    }
}
