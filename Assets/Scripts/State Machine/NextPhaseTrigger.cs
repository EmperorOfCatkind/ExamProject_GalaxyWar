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
        trigger = PlayerTurnController.Instance.GetTrigger();
        PlayerTurnController.Instance.turnStateMachine.SetOffTrigger(trigger);
    }
    public void CombatTrigger()
    {
        combatTrigger = PlayerTurnController.Instance.GetCombatTrigger();
        PlayerTurnController.Instance.combatStateMachine.SetOffTrigger(combatTrigger);
    }
}
