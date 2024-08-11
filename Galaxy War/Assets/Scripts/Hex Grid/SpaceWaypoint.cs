using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWaypoint : MonoBehaviour
{
    [SerializeField] public PlayerType playerType;
    public bool hasUnit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string ToString()
    {
        return name;
    }
}
