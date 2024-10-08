using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public static UnitController Instance;

    [SerializeField] public GameObject shipPrefab;
    [SerializeField] public GameObject frigatePrefab;
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

    public bool isBuilding;

    private int dockCost;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        shipYOffset = new Vector3(0, 3, 0);
        groundForceYOffset = new Vector3(0, .3f, 0);

        isBuilding = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        dockCost = 10;
    }

    // Update is called once per frame
    void Update()
    {
        switch(PlayerTurnController.Instance.GetCurrentPhase())
        {
            case Phase.Move:
            if(Input.GetMouseButtonDown(0))
            {
                if(TrySelectShip(PlayerTurnController.Instance.GetActivePlayer().GetPlayerType())) return;
                if(TrySelectDock(PlayerTurnController.Instance.GetActivePlayer().GetPlayerType())) return;
                if(TrySelectGroundForce(PlayerTurnController.Instance.GetActivePlayer().GetPlayerType())) return;
                MoveShip();
            }
            if(Input.GetMouseButtonDown(1) && selectedShip != null)
            {
                if(TrySelectGroundForce(PlayerTurnController.Instance.GetActivePlayer().GetPlayerType()))
                {
                    Embark();
                }
            }
            break;

            case Phase.Building:
            if(Input.GetMouseButtonDown(0) && isBuilding == true)
            {
                GridPosition positionToSpawn = MapController.Instance.GetHexGridPosition(MouseWorld.GetMouseWorldPosition());
                GridObject gridObjectToSpawn = MapController.Instance.GetGridObject(positionToSpawn);

                foreach(var planet in gridObjectToSpawn.GetPlanets())
                {
                    if(planet.GetOwner() != PlayerTurnController.Instance.GetActivePlayer().GetPlayerType() || planet.GetSpaceDockWaypoint().hasDock == true || PlayerTurnController.Instance.GetActivePlayer().GetOre() < dockCost)
                    {
                        MouseWorld.Instance.Reset();
                        isBuilding = false;
                        return;
                    }
                    else
                    {
                        PlayerTurnController.Instance.GetActivePlayer().GetComponent<Player>().AddOre(-dockCost);
                        SpawnDock(positionToSpawn, PlayerTurnController.Instance.GetActivePlayer().GetPlayerType());
                        isBuilding = false;
                        PlayerTurnController.Instance.buildDockButton.gameObject.SetActive(false);
                    }
                }
            }
            break;
        }
        
        
    }

    public void SpawnShip(GameObject ship, GridPosition gridPosition, PlayerType playerType) 
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
        PlayerTurnController.Instance.GetSpecificPlayer(playerType).AddShip(ship.GetComponent<Ship>());
    }

    public void SpawnDock(GridPosition gridPosition, PlayerType playerType)
    {
        if(!MapController.Instance.IsInBounds(gridPosition))
        {
            return;
        }

        GridObject gridObject = MapController.Instance.GetGridObject(gridPosition);

        Planet availablePlanet = gridObject.GetAvailablePlanetForSpaceDock();

        if(availablePlanet == null)
        {
            return;
        }

        dockPrefab.GetComponent<SpaceDock>().SetPlanet(availablePlanet);
        dockPrefab.GetComponent<SpaceDock>().SetPlayerType(playerType);
        
        SetMaterialForSpawn(playerType, dockPrefab);

        SpaceDockWaypoint spaceDockWaypoint = availablePlanet.GetSpaceDockWaypoint();

        availablePlanet.SetSpaceDock(Instantiate(dockPrefab, spaceDockWaypoint.transform.position + shipYOffset, Quaternion.identity).GetComponent<SpaceDock>());
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

    public bool TrySelectShip(PlayerType playerType)
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
                if(ship.GetPlayerType() != playerType)
                {
                    return false;
                }

                if(ship == selectedShip)
                {
                    selectedShip.Deselected();
                    selectedShip = null;
                    return true;
                }
                SetSelectedShip(ship);
                ship.Selected();
                return true;
            }
        }

        return false;
    }
    public bool TrySelectDock(PlayerType playerType)
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
                if(spaceDock.GetPlayerType() != playerType)
                {
                    return false;
                }

                if(spaceDock == selectedDock)
                {
                    selectedDock.Deselected();
                    selectedDock = null;
                    return true;
                }
                SetSelectedSpaceDock(spaceDock);
                spaceDock.Selected();
                return true;
            }
        }
        return false;
    }
    public bool TrySelectGroundForce(PlayerType playerType)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, groundForceLayerMask))
        {
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
                if(groundForce.GetPlayerType() != playerType)
                {
                    return false;
                }

                if(groundForce == selectedGroundForce)
                {
                    selectedGroundForce.Deselected();
                    selectedGroundForce = null;
                    return true;
                }
                SetSelectedGroundForce(groundForce);
                groundForce.Selected();
                return true;
            }
        }
        return false;
    }

    public void MoveShip()
    {
        Player player = PlayerTurnController.Instance.GetActivePlayer();
        if(selectedShip == null || player.GetFuel() == 0)
        {
            return;
        }

        GridPosition mouseGridPosition = MapController.Instance.GetHexGridPosition(MouseWorld.GetMouseWorldPosition());

        if(selectedShip.GetMoveAction().IsValidGridPosition(mouseGridPosition))
        {
            selectedShip.GetMoveAction().Move(mouseGridPosition);
        }
    }

    public void Embark()
    {
        if(selectedShip != null)
        {
            selectedShip.GetEmbarkAction().Embark(selectedGroundForce);
        }
    }

    public void DropAllSelections()
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
            selectedGroundForce = null;
        }
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
    public Ship GetSelectedShip()
    {
        return selectedShip;
    }

    public GroundForce GetSelectedGroundForce()
    {
        return selectedGroundForce;
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
