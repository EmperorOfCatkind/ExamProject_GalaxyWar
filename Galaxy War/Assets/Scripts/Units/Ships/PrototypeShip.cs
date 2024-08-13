using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeShip : MonoBehaviour, IShip
{
    public GridPosition gridPosition { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        gridPosition = MapController.Instance.GetHexGridPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
