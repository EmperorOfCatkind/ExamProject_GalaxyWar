using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbarkAction : BaseAction
{
    protected override void Awake()
    {
        base.Awake();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            return;
        }
    }

    public void Embark(GroundForce groundForce)
    {
        ship.Embark(groundForce);
    }
}
