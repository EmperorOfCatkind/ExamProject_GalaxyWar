using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IShip
{
    string Name {get; set;}
    GridPosition gridPosition {get; set;}
    SpaceWaypoint currentWaypoint {get; set;}
    string GetName();
    void HideSelectedVisual();
    void SetCurrentWaypoint(SpaceWaypoint currentWaypoint);
    SpaceWaypoint GetCurrentWaypoint();
    Vector3 GetCurrentWorldPosition();
    void SetCurrentWorldPosition(Vector3 position, float speed);
}
