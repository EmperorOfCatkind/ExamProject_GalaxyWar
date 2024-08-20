using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePhase : BasePhase
{
    int count = 0;
    private List<Planet> playerPlanets;
    private List<GameObject> playerShips;
    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        phaseName = "Resources";
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            return;
        }
    
        isActive = false;
    }

    //void Trigger from state machine - isActive = true
    public void DoResourcePhase()
    {
        playerPlanets = player.GetPlayerPlanets();

        foreach(var planet in playerPlanets)
        {
            player.AddOre(planet.GetOreAmount());
            player.AddFuel(planet.GetFuelAmount());
            //add ore value to SpaceDok building capacity
        }

        playerShips = player.GetPlayersShips();

        /*foreach(var ship in playerShips)
        {
            ship.GetComponent<Ship>().hasMoved = false;
        }*/
        //isActive = true;
    }
}
