using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IShip
{
    string Name {get; set;}
    GridPosition gridPosition {get; set;}
    string GetName();
    
}
