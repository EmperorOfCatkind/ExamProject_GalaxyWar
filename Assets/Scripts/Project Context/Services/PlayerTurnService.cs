using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPlayerTurnService
{
    PlayerData[] players {get;}

    StateMachine<Phase, Trigger> turnStateMachine {get;}
    StateMachine<CombatPhase, CombatTrigger> combatStateMachine {get;}

    Player winner {get; set;}

    public void IncrementTurnCounter(PlayerType playerType);
    public int GetTurnCounter(PlayerType playerType);
    public void SetWinner(Player player);
}
public class PlayerTurnService : IPlayerTurnService
{
    public PlayerData[] players {get; private set;}
    public Dictionary<PlayerType, int> turnCounter;

    public Player winner {get; set;}

    public StateMachine<Phase, Trigger> turnStateMachine {get;}
    public StateMachine<CombatPhase, CombatTrigger> combatStateMachine {get;}

    public PlayerTurnService(IConfigService configService)
    {
        players = configService.Players;

        turnStateMachine = new StateMachine<Phase, Trigger>(Phase.Start);
        turnStateMachine.AddTransition(Phase.Start, Trigger.ToTurnCount, Phase.TurnCount);
        turnStateMachine.AddTransition(Phase.TurnCount, Trigger.ToReplenish, Phase.Replenish);
        turnStateMachine.AddTransition(Phase.Replenish, Trigger.ToMove, Phase.Move);
        turnStateMachine.AddTransition(Phase.Move, Trigger.ToSpaceCombat, Phase.SpaceCombat);
        turnStateMachine.AddTransition(Phase.SpaceCombat, Trigger.ToGroundCombat, Phase.GroundCombat);
        turnStateMachine.AddTransition(Phase.GroundCombat, Trigger.ToBuilding, Phase.Building);
        turnStateMachine.AddTransition(Phase.Building, Trigger.ToStart, Phase.Start);
        turnStateMachine.AddTransition(Phase.Building, Trigger.EndGame, Phase.EndScreen);
        turnStateMachine.AddTransition(Phase.EndScreen, Trigger.ToStart, Phase.Start);


        turnCounter = new Dictionary<PlayerType, int>();
        foreach(var playerData in players)
        {
            turnCounter.Add(playerData.playerType, 0);
        }

        combatStateMachine = new StateMachine<CombatPhase, CombatTrigger>(CombatPhase.Off);
        combatStateMachine.AddTransition(CombatPhase.Off, CombatTrigger.ToNextRound, CombatPhase.Start);
        combatStateMachine.AddTransition(CombatPhase.Start, CombatTrigger.ToRoll, CombatPhase.Roll);
        combatStateMachine.AddTransition(CombatPhase.Roll, CombatTrigger.ToAssign, CombatPhase.Assign);
        combatStateMachine.AddTransition(CombatPhase.Assign, CombatTrigger.ToDestroy, CombatPhase.Destroy);
        combatStateMachine.AddTransition(CombatPhase.Destroy, CombatTrigger.ToEnd, CombatPhase.End);
        combatStateMachine.AddTransition(CombatPhase.End, CombatTrigger.ToNextRound, CombatPhase.Start);
        combatStateMachine.AddTransition(CombatPhase.End, CombatTrigger.Finish, CombatPhase.Off);
    }

    public void IncrementTurnCounter(PlayerType playerType)
    {
        turnCounter[playerType]++;
    }

    public int GetTurnCounter(PlayerType playerType)
    {
        return turnCounter[playerType];
    }

    public void SetWinner(Player player)
    {
        winner = player;
    }
}

