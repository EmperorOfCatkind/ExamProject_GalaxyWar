using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPhaseTrigger : MonoBehaviour
{
    private Trigger trigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trigger = PlayerTurnController.Instance.turnStateMachine.phaseTransition[PlayerTurnController.Instance.GetCurrentPhase()].Trigger;
    }

    public void Trigger()
    {
        PlayerTurnController.Instance.turnStateMachine.SetOffTrigger(trigger);
    }
}
