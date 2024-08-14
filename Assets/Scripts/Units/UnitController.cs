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

        SpawnShip(new GridPosition(0,1));
        SpawnShip(new GridPosition(1,2));
        SpawnShip(new GridPosition(3,1));
        SpawnShip(new GridPosition(0,0));
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

        Instantiate(shipPrefab, MapController.Instance.GetWorldPosition(gridPosition) + shipYOffset, Quaternion.identity);
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
