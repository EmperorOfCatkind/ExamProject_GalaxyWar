using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private LayerMask shipLayerMask;
    

    [SerializeField] private float stoppingDistance;
    [SerializeField] private float speed;

    private Vector3 shipYOffset;
    private IShip selectedShip;
    private bool move;
    

    // Start is called before the first frame update
    void Start()
    {
        shipYOffset = new Vector3(0, 3, 0);
        stoppingDistance = .5f;
        speed = 5f;
        move = false;

        SpawnShip(shipPrefab, new GridPosition(0, 0));  //debug purposes
        SpawnShip(shipPrefab, new GridPosition(0, 0));  //debug purposes
        SpawnShip(shipPrefab, new GridPosition(0, 0));  //debug purposes
        SpawnShip(shipPrefab, new GridPosition(0, 0));  //debug purposes
        SpawnShip(shipPrefab, new GridPosition(0, 0));  //debug purposes
        SpawnShip(shipPrefab, new GridPosition(0, 0));  //debug purposes
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(TrySelectShip()) return;
            while(move == false)
            {
                MoveShip();
                move = MoveShip();
            }
        }
    }

    public void SpawnShip(GameObject prefab, GridPosition gridPosition)
    {
        MapGridViewSingle hexToSpawn = MapController.Instance.GetMapGridViewSingle(gridPosition);

        SpaceWaypoint availableWaypoint = hexToSpawn.GetAvailableWaypoint();

        if(availableWaypoint == null)
        {
            return;
        }

        Instantiate(prefab, availableWaypoint.transform.position + shipYOffset, Quaternion.identity);
        availableWaypoint.GotOccupiedByShip();
        prefab.GetComponent<IShip>().SetCurrentWaypoint(availableWaypoint);
    }

    public bool TrySelectShip()
    {
        if(selectedShip != null)
        {
            selectedShip.HideSelectedVisual();
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool OnShip = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, shipLayerMask);

        if(OnShip)
        {
            if(raycastHit.transform.TryGetComponent<IShip>(out IShip ship))
            {
                SetSelectedShip(ship);
                Debug.Log(selectedShip.GetName());
                raycastHit.transform.GetComponent<SelectedVisual>().Show();
                return true;
            }
        }

        return false;
    }

    public void SetSelectedShip(IShip ship)
    {
        selectedShip = ship;
    }
    
    public bool MoveShip()
    {
        if(selectedShip == null)
        {
            return true;
        }

        GridPosition gridPosition = MapController.Instance.GetHexGridPosition(MouseWorld.GetMouseWorldPosition());

        if(selectedShip.gridPosition == gridPosition)
        {
            return true;
        }

        MapGridViewSingle targetHex = MapController.Instance.GetMapGridViewSingle(gridPosition);
        SpaceWaypoint availableWaypoint = targetHex.GetAvailableWaypoint();

        if(availableWaypoint == null)
        {
            return true;
        }

        SpaceWaypoint previousWaypoint = selectedShip.GetCurrentWaypoint();
        Vector3 start = selectedShip.GetCurrentWorldPosition();
        Vector3 end = availableWaypoint.transform.position;

        Vector3 moveDirection = (end - start).normalized;

        if(Vector3.Distance(start, end) > stoppingDistance)
        {
            selectedShip.SetCurrentWorldPosition(moveDirection, speed);
            return false;
        }

        else
        {
            selectedShip.SetCurrentWorldPosition(selectedShip.GetCurrentWorldPosition() + shipYOffset, speed);
            previousWaypoint.GotFree();
            availableWaypoint.GotOccupiedByShip();
            selectedShip.SetCurrentWaypoint(availableWaypoint);
            return true;
        }
    }
}
