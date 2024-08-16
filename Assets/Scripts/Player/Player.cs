using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string Name;
    private PlayerType playerType;
    private GridObject homeSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName(string Name)
    {
        this.Name = Name;
    }
    public void SetPlayerType(PlayerType playerType)
    {
        this.playerType = playerType;
    }
    public void SetHomeSystem(GridObject gridObject)
    {
        homeSystem = gridObject;
    }
    public GridObject GetHomeSystem()
    {
        return homeSystem;
    }
}
