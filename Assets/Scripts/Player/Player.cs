using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string Name;
    private PlayerType playerType;
    private GridObject homeSystem;
    
    private int oreAmount = 0;
    private int fuelAmount = 0;
    private List<Planet> playersPlanets;

    private List<GameObject> playersShips;

    public PlayerUI playerUI;

    private StartPhase startPhase;
    private TurnCountPhase turnCountPhase;
    private ResourcePhase resourcePhase;
    private MovePhase movePhase;
    private SpaceCombatPhase spaceCombatPhase;
    private GroundCombatPhase groundCombatPhase;
    private BuildingPhase buildingPhase;

    void Awake()
    {
        playerUI = GetComponent<PlayerUI>();

        startPhase = GetComponent<StartPhase>();
        turnCountPhase = GetComponent<TurnCountPhase>();
        resourcePhase = GetComponent<ResourcePhase>();
        movePhase = GetComponent<MovePhase>();
        spaceCombatPhase = GetComponent<SpaceCombatPhase>();
        groundCombatPhase = GetComponent<GroundCombatPhase>();
        buildingPhase = GetComponent<BuildingPhase>();

        playersShips = new List<GameObject>();
        playersPlanets = new List<Planet>();

        playerUI.Hide();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName(string Name)
    {
        this.Name = Name;
    }
    public string GetName()
    {
        return Name;
    }
    public void SetPlayerType(PlayerType playerType)
    {
        this.playerType = playerType;
    }
    public PlayerType GetPlayerType()
    {
        return playerType;
    }

    public void SetHomeSystem(GridObject gridObject)
    {
        homeSystem = gridObject;
    }
    public GridObject GetHomeSystem()
    {
        return homeSystem;
    }

    public int GetOre()
    {
        return oreAmount;
    }
    public int GetFuel()
    {
        return fuelAmount;
    }

    public void AddShip(GameObject ship)
    {
        playersShips.Add(ship);
    }
    public List<GameObject> GetPlayersShips()
    {
        return playersShips;
    }

    public void AddPlanet(Planet planet)
    {
        playersPlanets.Add(planet);
        planet.SetOwner(playerType);
    }
    public List<Planet> GetPlayerPlanets()
    {
        return playersPlanets;
    }

    public void AddOre(int amount)
    {
        oreAmount += amount;
    }
    public void AddFuel(int amount)
    {
        fuelAmount += amount;
    }

    public StartPhase GetStartPhase()
    {
        return startPhase;
    }
    public TurnCountPhase GetTurnCountPhase()
    {
        return turnCountPhase;
    }
    public ResourcePhase GetResourcePhase()
    {
        return resourcePhase;
    }
    public MovePhase GetMovePhase()
    {
        return movePhase;
    }
    public SpaceCombatPhase GetSpaceCombatPhase()
    {
        return spaceCombatPhase;
    }
    public GroundCombatPhase GetGroundCombatPhase()
    {
        return groundCombatPhase;
    }
    public BuildingPhase GetBuildingPhase()
    {
        return buildingPhase;
    }

}
