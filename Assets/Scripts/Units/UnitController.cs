using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public static UnitController Instance;

    //[SerializeField] private GameObject shipPrefab;
    [SerializeField] private GameObject dockPrefab;
    [SerializeField] private LayerMask shipLayerMask;
    [SerializeField] private LayerMask dockLayerMask;

    [SerializeField] private Material playerOneMaterial;
    [SerializeField] private Material playerTwoMaterial;

    private Ship selectedShip;
    private SpaceDock selectedDock;
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

        /*SpawnShip(new GridPosition(0,1));       //debug purposes
        SpawnShip(new GridPosition(0,1));       //debug purposes

        SpawnShip(new GridPosition(1,2));       //debug purposes
        SpawnShip(new GridPosition(1,2));       //debug purposes

        SpawnShip(new GridPosition(3,1));       //debug purposes
        SpawnShip(new GridPosition(3,1));       //debug purposes

        SpawnShip(new GridPosition(0,0));       //debug purposes
        SpawnShip(new GridPosition(0,0));       //debug purposes

        SpawnDock(new GridPosition(0,0));*/       //debug purposes

        //SpawnDock(new GridPosition(2,2));       //debug purposes
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(TrySelectShip()) return;
            if(TrySelectDock()) return;
            MoveShip();
        }
    }

    public void SpawnShip(GameObject ship, GridPosition gridPosition, PlayerType playerType)        //add player type
    {
        if(!MapController.Instance.IsInBounds(gridPosition))
        {
            return;
        }

        GridObject gridObject = MapController.Instance.GetGridObject(gridPosition);

        SpaceWaypoint availableWaypoint = gridObject.GetAvailableSpaceWaypoint();

        if(availableWaypoint == null)
        {
            return;
        }

        ship.GetComponent<Ship>().SetCurrentWaypoint(availableWaypoint);
        ship.GetComponent<Ship>().SetPlayerType(playerType);

        SetMaterialForSpawn(playerType, ship);

        Instantiate(ship, availableWaypoint.transform.position + shipYOffset, Quaternion.identity);
        availableWaypoint.hasShip = true;
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
            if(selectedDock != null)
            {
                selectedDock.Deselected();
                selectedDock = null;
            }
            
            if(raycastHit.transform.TryGetComponent<Ship>(out Ship ship))
            {
                SetSelectedShip(ship);
                ship.Selected();
                Debug.Log(ship.GetCurrentWaypoint());
                return true;
            }
        }

        return false;
    }
    public bool TrySelectDock()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, dockLayerMask))
        {
            if(selectedShip != null)
            {
                selectedShip.Deselected();
                selectedShip = null;
            }
            if(selectedDock != null)
            {
                selectedDock.Deselected();
            }

            if(raycastHit.transform.TryGetComponent<SpaceDock>(out SpaceDock spaceDock))
            {
                SetSelectedSpaceDock(spaceDock);
                spaceDock.Selected();
                Debug.Log(spaceDock.GetPlanet() + " " + spaceDock.GetPlanet().GetSpaceDockWaypoint().hasDock);
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
    public void SetSelectedSpaceDock(SpaceDock spaceDock)
    {
        selectedDock = spaceDock;
    }

    public void SpawnDock(GridPosition gridPosition, PlayerType playerType)
    {
        if(!MapController.Instance.IsInBounds(gridPosition))
        {
            return;
        }

        GridObject gridObject = MapController.Instance.GetGridObject(gridPosition);

        //SpaceDockWaypoint availableDockPoint = gridObject.GetAvailableSpaceDockWaypoint().GetSpaceDockWaypoint();
        Planet availablePlanet = gridObject.GetAvailablePlanetForSpaceDock();

        if(availablePlanet == null)
        {
            return;
        }

        dockPrefab.GetComponent<SpaceDock>().SetPlanet(availablePlanet);
        dockPrefab.GetComponent<SpaceDock>().SetPlayerType(playerType);
        
        SetMaterialForSpawn(playerType, dockPrefab);

        SpaceDockWaypoint spaceDockWaypoint = availablePlanet.GetSpaceDockWaypoint();

        Instantiate(dockPrefab, spaceDockWaypoint.transform.position + shipYOffset, Quaternion.identity);
        spaceDockWaypoint.hasDock = true;
    }

    public void SetMaterialForSpawn(PlayerType playerType, GameObject prefab)
    {
        Renderer[] childRenderers = prefab.GetComponentsInChildren<Renderer>();

        Material materialToSpawn = null;
        if(playerType == PlayerType.PlayerOne)
        {
            materialToSpawn = playerOneMaterial;
        }
        else if(playerType == PlayerType.PlayerTwo)
        {
            materialToSpawn = playerTwoMaterial;
        }

        foreach (Renderer renderer in childRenderers)
        {
            renderer.material = materialToSpawn;
        }
    }
}
