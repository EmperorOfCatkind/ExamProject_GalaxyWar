using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCombatPhase : BasePhase
{
    private GridObject combatGridObject;
    private Dictionary<PlayerType, int> hitsProduced;
    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        phaseName = "Space Combat";
        hitsProduced = new Dictionary<PlayerType, int>
        {
            {PlayerType.PlayerOne, 0},
            {PlayerType.PlayerTwo, 0}
        };
    }

    // Update is called once per frame
    void Update()
    {
        while(isActive)
        {
            combatGridObject = PlayerTurnController.Instance.GetCombatGridObject();
            
            /*if(!(combatGridObject.GetShipListByPlayerType().ContainsKey(PlayerType.PlayerOne) && combatGridObject.GetShipListByPlayerType().ContainsKey(PlayerType.PlayerTwo)))
            {
                isActive = false;
                return;
            }*/
            
            PlayerTurnController.Instance.cameraController.transform.position = MapController.Instance.GetWorldPosition(combatGridObject.GetGridPosition());
            PlayerTurnController.Instance.cameraController.CombatMode();
            isActive = false;
        }
    }

    //void Trigger from state machine - isActive = true
    public void DoSpaceCombatPhase()
    {
        debugString = "This is " + phaseName + " of player " + player.GetName();
        isActive = true;
    }

    public int RollCombat(Ship ship)
    {
        int attack = Random.Range(1,11);
        if(attack >= ship.combat)
        {
            hitsProduced[ship.GetPlayerType()]++;
        }
        return attack;
    }
}
