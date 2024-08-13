using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private LayerMask shipLayerMask;
    private Vector3 shipYOffset;
    private IShip selectedShip;
    // Start is called before the first frame update
    void Start()
    {
        shipYOffset = new Vector3(0, 3, 0);

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
        availableWaypoint.SetHasUnit(true);
    }

    public bool TrySelectShip()
    {
        /*if(selectedShip != null)
        {
            selectedShip.GetComponent<SelectedVisual>().Hide();
        }*/
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
            /*GameObject selectedObject = raycastHit.collider.gameObject;

            IShip ship = selectedObject.GetComponent<IShip>();

            if(ship != null)
            {
                Debug.Log("this is " + selectedObject.name);
                ship.DisplayName();
                return true;
            }*/
        }

        return false;
    }

    public void SetSelectedShip(IShip ship)
    {
        selectedShip = ship;
    }
}
