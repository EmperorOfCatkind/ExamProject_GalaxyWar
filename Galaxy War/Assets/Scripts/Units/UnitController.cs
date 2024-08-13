using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] GameObject shipPrefab;
    Vector3 shipYOffset;
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
        SpawnShip(shipPrefab, new GridPosition(0, 0));  //debug purposes
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
