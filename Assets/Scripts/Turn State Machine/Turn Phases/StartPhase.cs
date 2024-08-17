using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPhase : BasePhase
{
    int count = 0;
    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        phaseName = "Start";
    }

    // Update is called once per frame
    void Update()
    {
        while(isActive)
        {
            if(count < 3)
            {
                DoStartPhase();
                count++;
                Debug.Log(debugString);   
            }
            else{
                count = 0;
                isActive = false;
            }
        }
    }

    //void Trigger from state machine - isActive = true
    public void DoStartPhase()
    {
        debugString = "This is " + phaseName + " of player " + player.GetName();
        isActive = true;
    }
}
