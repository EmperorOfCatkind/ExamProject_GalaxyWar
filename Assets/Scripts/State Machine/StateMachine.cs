using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine <TPhase, TTrigger>
{
    public TPhase currentPhase  {get; private set;}
    public Dictionary<TPhase, List<Transition<TTrigger, TPhase>>> phaseTransitions;
    public Action<PhaseTransitionData<TPhase, TTrigger>> OnPhaseChanged;

    public StateMachine(TPhase initialPhase)
    {
        currentPhase = initialPhase;
        phaseTransitions = new Dictionary<TPhase, List<Transition<TTrigger, TPhase>>>();
    }

    public void AddTransition(TPhase phase, TTrigger trigger, TPhase nextPhase)
    {
        if(!phaseTransitions.ContainsKey(phase))
        {
            phaseTransitions.Add(phase, new List<Transition<TTrigger, TPhase>>());
        }

        var transitions = phaseTransitions[phase];
        for (int i = 0; i < transitions.Count; i++)
        {
            if(transitions[i].Trigger.Equals(trigger))
            {
                Debug.LogAssertion($"Trigger: {transitions[i].Trigger.ToString()} is already used to transition to {transitions[i].NextPhase.ToString()}");
                return;
            }
        }
        transitions.Add(new Transition<TTrigger, TPhase>(trigger, nextPhase));
    }

    public void SetOffTrigger(TTrigger trigger)
    {
       var transitions = phaseTransitions[currentPhase];

        foreach (var transition in transitions)
        {
            if(transition.Trigger.Equals(trigger))
            {
                var previousPhase = currentPhase;
                currentPhase = transition.NextPhase;
                OnPhaseChanged?.Invoke(new PhaseTransitionData<TPhase, TTrigger>(previousPhase, currentPhase, trigger));
                return;
            }
        }
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
