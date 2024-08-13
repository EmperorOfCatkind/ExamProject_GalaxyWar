using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWaypoint : MonoBehaviour
{
    private GridPosition gridPosition;
    private bool hasUnit;
    // Start is called before the first frame update
    void Start()
    {
        gridPosition = MapController.Instance.GetHexGridPosition(transform.position);
        hasUnit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool GetHasUnit()
    {
        return hasUnit;
    }
    public void SetHasUnit(bool value)
    {
        hasUnit = value;
    }
}
