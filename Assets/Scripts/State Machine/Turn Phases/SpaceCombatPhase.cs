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
            
            PlayerTurnController.Instance.cameraController.transform.position = MapController.Instance.GetWorldPosition(combatGridObject.GetGridPosition());
            PlayerTurnController.Instance.cameraController.CombatMode();
            isActive = false;
        }
    }

    //void Trigger from state machine - isActive = true
    public void LaunchSpaceCombatPhase()
    {
        debugString = "This is " + phaseName + " of player " + player.GetName();
        isActive = true;
    }

    public void MakeCombatRolls(List<Ship> ships)
    {
        
        foreach(var ship in ships)
        {
            int roll = Random.Range(1,11);
            ship.SetRollText(roll);

            if(roll >= ship.combat)
            {
                hitsProduced[ship.GetPlayerType()]++;
            }
        }

        foreach (var kvp in hitsProduced)
        {
            Debug.Log(kvp.Key + " " + kvp.Value);
        }
    }

    public void ResetCounters()
    {
        hitsProduced = new Dictionary<PlayerType, int>
        {
            {PlayerType.PlayerOne, 0},
            {PlayerType.PlayerTwo, 0}
        };
    }
}
