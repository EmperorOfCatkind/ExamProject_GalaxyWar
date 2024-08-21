using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine <TPhase, TTrigger>
{
    public TPhase currentPhase  {get; private set;}
    public Dictionary<TPhase, Transition<TTrigger, TPhase>> phaseTransition;
    public Action<PhaseTransitionData<TPhase, TTrigger>> OnPhaseChanged;

    public StateMachine(TPhase initialPhase)
    {
        currentPhase = initialPhase;
        phaseTransition = new Dictionary<TPhase, Transition<TTrigger, TPhase>>();
    }

    public void AddTransition(TPhase phase, TTrigger trigger, TPhase nextPhase)
    {
        if(!phaseTransition.ContainsKey(phase))
        {
            phaseTransition.Add(phase, new Transition<TTrigger, TPhase>());
        }

        /*var transition = phaseTransition[phase];
        if(transition.Trigger.Equals(trigger))
        {
            Debug.LogAssertion($"Trigger: {transition.Trigger.ToString()} is already used to transition to {transition.NextPhase.ToString()}");
            return;
        }*/
        phaseTransition[phase] = new Transition<TTrigger, TPhase>(trigger, nextPhase);

        //Debug.Log($"State Machine now contains the following transition: form {phase.ToString()} through {phaseTransition[phase].Trigger.ToString()} to {phaseTransition[phase].NextPhase.ToString()}");
    }

    public void SetOffTrigger(TTrigger trigger)
    {
        var transition = phaseTransition[currentPhase];

        var previousPhase = currentPhase;
        currentPhase = transition.NextPhase;
        OnPhaseChanged?.Invoke(new PhaseTransitionData<TPhase, TTrigger>(previousPhase, currentPhase, trigger));
        return;
    }
}

public struct Transition<TTrigger, TPhase>
{
    public TTrigger Trigger{get;}

    public TPhase NextPhase {get;}

    public Transition (TTrigger trigger, TPhase nextPhase)
    {
        Trigger = trigger;
        NextPhase = nextPhase;
    }
}
public struct PhaseTransitionData<TPhase, TTrigger>
{
    public TPhase PreviousPhase {get;}
    public TPhase NextPhase {get;}
    public TTrigger Trigger {get;}

    public PhaseTransitionData(TPhase previousPhase, TPhase nextPhase, TTrigger trigger)
    {
        PreviousPhase = previousPhase;
        NextPhase = nextPhase;
        Trigger = trigger;
    }
}
