using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPlayerTurnService
{
    PlayerData[] players {get;}

    TurnStateMachine<Phase, Trigger> turnStateMachine {get;}

    public void IncrementTurnCounter(PlayerType playerType);

    public int GetTurnCounter(PlayerType playerType);
}
public class PlayerTurnService : IPlayerTurnService
{
    public PlayerData[] players {get; private set;}
    public Dictionary<PlayerType, int> turnCounter;

    public TurnStateMachine<Phase, Trigger> turnStateMachine {get;}

    public PlayerTurnService(IConfigService configService)
    {
        players = configService.Players;

        turnStateMachine = new TurnStateMachine<Phase, Trigger>(Phase.Start);

        turnStateMachine.AddTransition(Phase.Start, Trigger.ToTurnCount, Phase.TurnCount);

        turnStateMachine.AddTransition(Phase.TurnCount, Trigger.ToReplenish, Phase.Replenish);

        turnStateMachine.AddTransition(Phase.Replenish, Trigger.ToMove, Phase.Move);

        turnStateMachine.AddTransition(Phase.Move, Trigger.ToSpaceCombat, Phase.SpaceCombat);

        turnStateMachine.AddTransition(Phase.SpaceCombat, Trigger.ToGroundCombat, Phase.GroundCombat);

        turnStateMachine.AddTransition(Phase.GroundCombat, Trigger.ToBuilding, Phase.Building);

        turnStateMachine.AddTransition(Phase.Building, Trigger.EndTurn, Phase.Start);

        turnCounter = new Dictionary<PlayerType, int>();
        foreach(var playerData in players)
        {
            turnCounter.Add(playerData.playerType, 0);
        }
    }

    public void IncrementTurnCounter(PlayerType playerType)
    {
        turnCounter[playerType]++;
    }

    public int GetTurnCounter(PlayerType playerType)
    {
        return turnCounter[playerType];
    }
}

