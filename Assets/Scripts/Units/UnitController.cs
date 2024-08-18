using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public static UnitController Instance;

    [SerializeField] private GameObject shipPrefab; //debug purposes
    [SerializeField] private GameObject dockPrefab;
    [SerializeField] private GameObject groundForcePrefab;
    [SerializeField] private LayerMask shipLayerMask;
    [SerializeField] private LayerMask dockLayerMask;
    [SerializeField] private LayerMask groundForceLayerMask;

    [SerializeField] private Material playerOneMaterial;
    [SerializeField] private Material playerTwoMaterial;

    private Ship selectedShip;
    private SpaceDock selectedDock;
    private GroundForce selectedGroundForce;
    private Vector3 shipYOffset;
    private Vector3 groundForceYOffset;

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
        groundForceYOffset = new Vector3(0, .3f, 0);

        SpawnShip(shipPrefab, new GridPosition(0,1), PlayerType.PlayerOne); //debug purposes
        SpawnShip(shipPrefab, new GridPosition(0,1), PlayerType.PlayerOne); //debug purposes
        SpawnShip(shipPrefab, new GridPosition(0,1), PlayerType.PlayerOne); //debug purposes

        SpawnShip(shipPrefab, new GridPosition(0,1), PlayerType.PlayerTwo); //debug purposes
        SpawnShip(shipPrefab, new GridPosition(0,1), PlayerType.PlayerTwo); //debug purposes
        SpawnShip(shipPrefab, new GridPosition(0,1), PlayerType.PlayerTwo); //debug purposes
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(TrySelectShip()) return;
            if(TrySelectDock()) return;
            if(TrySelectGroundForce()) return;
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

    public void SpawnGroundForce(GridPosition gridPosition, PlayerType playerType)
    {
        if(!MapController.Instance.IsInBounds(gridPosition))
        {
            return;
        }

        GridObject gridObject = MapController.Instance.GetGridObject(gridPosition);

        Planet availablePlanet = gridObject.GetAvailablePlanetForGroundForce();

        if(availablePlanet == null)
        {
            return;
        }

        GroundForceWaypoint availableWaypoint = availablePlanet.GetAvailableGroundForceWaypoint();
        
        if(availableWaypoint == null)
        {
            return;
        }

        groundForcePrefab.GetComponent<GroundForce>().SetPlanet(availablePlanet);
        groundForcePrefab.GetComponent<GroundForce>().SetPlayerType(playerType);

        SetMaterialForSpawn(playerType, groundForcePrefab);

        Instantiate(groundForcePrefab, availableWaypoint.transform.position + groundForceYOffset, Quaternion.identity);
        availableWaypoint.hasGroundForce = true;

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
            if(selectedGroundForce != null)
            {
                selectedGroundForce.Deselected();
                selectedGroundForce = null;
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
            if(selectedGroundForce != null)
            {
                selectedGroundForce.Deselected();
                selectedGroundForce = null;
            }

            if(raycastHit.transform.TryGetComponent<SpaceDock>(out SpaceDock spaceDock))
            {
                SetSelectedSpaceDock(spaceDock);
                spaceDock.Selected();
                //Debug.Log(spaceDock.GetPlanet() + " " + spaceDock.GetPlanet().GetSpaceDockWaypoint().hasDock);
                return true;
            }
        }
        return false;
    }
    public bool TrySelectGroundForce()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, groundForceLayerMask))
        {
            if(selectedShip != null)
            {
                selectedShip.Deselected();
                selectedShip = null;
            }
            if(selectedDock != null)
            {
                selectedDock.Deselected();
                selectedDock = null;
            }
            if(selectedGroundForce != null)
            {
                selectedGroundForce.Deselected();
            }

            if(raycastHit.transform.TryGetComponent<GroundForce>(out GroundForce groundForce))
            {
                SetSelectedGroundForce(groundForce);
                groundForce.Selected();
                //Debug.Log(groundForce.GetPlanet());
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
    public void SetSelectedGroundForce(GroundForce groundForce)
    {
        selectedGroundForce = groundForce;
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
