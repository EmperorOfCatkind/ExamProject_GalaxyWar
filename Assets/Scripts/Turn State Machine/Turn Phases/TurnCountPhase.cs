using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCountPhase : BasePhase //display player name and what turn counted are they taking
{
    int count = 0;
    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        phaseName = "Turn Count";
    }

    // Update is called once per frame
    void Update()
    {
        while(isActive)
        {
            if(count < 3)
            {
                DoTurnCountPhase();
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
    public void DoTurnCountPhase()
    {
        debugString = "This is " + phaseName + " of player " + player.GetName();
        isActive = true;
    }
}