using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPhaseTrigger : MonoBehaviour
{
    private Trigger trigger;
    private CombatTrigger combatTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Trigger()
    {
        trigger = PlayerTurnController.Instance.turnStateMachine.phaseTransition[PlayerTurnController.Instance.GetCurrentPhase()].Trigger;
        PlayerTurnController.Instance.turnStateMachine.SetOffTrigger(trigger);
    }
    public void CombatTrigger()
    {
        combatTrigger = PlayerTurnController.Instance.combatStateMachine.phaseTransition[PlayerTurnController.Instance.GetCurrentCombatPhase()].Trigger;
        PlayerTurnController.Instance.combatStateMachine.SetOffTrigger(combatTrigger);
    }
}
