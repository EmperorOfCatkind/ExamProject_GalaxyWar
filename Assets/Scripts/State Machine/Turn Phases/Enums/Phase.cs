using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase
{
    Start,      //initial state, big button on the game scene to start a game and launch it all, no state leads back to it
    TurnCount,
    Replenish,
    Move,
    SpaceCombat,
    GroundCombat,
    Building,
    EndScreen
}
