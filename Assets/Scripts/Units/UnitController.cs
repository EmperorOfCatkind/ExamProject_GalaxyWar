using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public static UnitController Instance;

    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private LayerMask shipLayerMask;

    private Ship selectedShip;
    private Vector3 shipYOffset;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        shipYOffset = new Vector3(0, 3, 0);

        SpawnShip(new GridPosition(0,1));       //debug purposes
        SpawnShip(new GridPosition(0,1));       //debug purposes

        SpawnShip(new GridPosition(1,2));       //debug purposes
        SpawnShip(new GridPosition(1,2));       //debug purposes

        SpawnShip(new GridPosition(3,1));       //debug purposes
        SpawnShip(new GridPosition(3,1));       //debug purposes

        SpawnShip(new GridPosition(0,0));       //debug purposes
        SpawnShip(new GridPosition(0,0));       //debug purposes
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if( TrySelectShip()) return;
            MoveShip();
        }
    }

    public void SpawnShip(GridPosition gridPosition)
    {
        if(!MapController.Instance.IsInBounds(gridPosition))
        {
            return;
        }

        GridObject gridObject = MapController.Instance.GetGridObject(gridPosition);

        /*if(gridObject == null)
        {
            return;
        }*/

        SpaceWaypoint availableWaypoint = gridObject.GetAvailableSpaceWaypoint();

        //Instantiate(shipPrefab, MapController.Instance.GetWorldPosition(gridPosition) + shipYOffset, Quaternion.identity);
        Instantiate(shipPrefab, availableWaypoint.transform.position + shipYOffset, Quaternion.identity);
        availableWaypoint.hasShip = true;
        //shipPrefab.GetComponent<Ship>().SetCurrentWaypoint(availableWaypoint);      //this line is useless
    }

    public bool TrySelectShip()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, shipLayerMask))
        {
            if(selectedShip != null)
            {
                selectedShip.Deselected();
            }
            
            if(raycastHit.transform.TryGetComponent<Ship>(out Ship ship))
            {
                SetSelectedShip(ship);
                ship.Selected();
                //Debug.Log(ship.GetCurrentWaypoint());
                return true;
            }
        }

        return false;
    }

    public void MoveShip()
    {
        if(selectedShip == null)
        {
            return;
        }

        GridPosition mouseGridPosition = MapController.Instance.GetHexGridPosition(MouseWorld.GetMouseWorldPosition());
        selectedShip.GetMoveAction().Move(mouseGridPosition);
    }

    public void SetSelectedShip(Ship ship)
    {
        selectedShip = ship;
    }
}
