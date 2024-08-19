using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerTurnController : MonoBehaviour
{
    public static PlayerTurnController Instance;

    private IPlayerTurnService playerTurnService;
    private PlayerData[] playerDatas;
    public TurnStateMachine<Phase, Trigger> turnStateMachine;
    public PhaseTransitionData<Phase, Trigger> phaseTransitionData;


    [SerializeField] private Transform player;
    private Player[] playersArray;

    private int activePlayerIndex;
    private Player activePlayer;

    private TurnInfoUI turnInfoUI;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerTurnService = ProjectContext.Instance.PlayerTurnService;
        playerDatas = playerTurnService.players;
        turnStateMachine = playerTurnService.turnStateMachine;
        turnInfoUI = GetComponent<TurnInfoUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializePlayers();
        /*foreach(var player in playersArray)
        {
            Debug.Log(player.GetHomeSystem().ToString());
        }*/
        turnStateMachine.OnPhaseChanged += OnPhaseChanged;
        InitializeFirstPlayer();       
        

        /*for(int i = 0; i < playersArray.Length; i++)
        {
            Debug.Log(playersArray[i].GetName() + " " + i);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPhaseChanged(PhaseTransitionData<Phase, Trigger> phaseTransitionData)        //Get a phase class from an active player, activate it and go through it.
    {
        this.phaseTransitionData = phaseTransitionData;
        switch(phaseTransitionData.NextPhase)
        {
            case Phase.Start:   //if Start is the next phase, change the active player
                StartPhase();
                turnInfoUI.UpdateValues();
                break;

            case Phase.TurnCount:
                TurnCountPhase();
                turnInfoUI.UpdateValues();
                break;

            case Phase.Replenish:
                ReplenishPhase();
                turnInfoUI.UpdateValues();
                break;

            case Phase.Move:
                //get ships move only once per turn and no further than their Move stat
                //get ground forces to embark and disembark
                //by the end if there is a Hex (gridObject) ships belonging to both players add it to the List for the next phase to use
                //if there is a planet with ground forces of both players add it to the list for the next next phase to use
                activePlayer.GetMovePhase().DoMovePhase();
                turnInfoUI.UpdateValues();
                break;

            case Phase.SpaceCombat:
                //create a combat feature
                activePlayer.GetSpaceCombatPhase().DoSpaceCombatPhase();
                turnInfoUI.UpdateValues();
                break;
                
            case Phase.GroundCombat:
                //create a combat feature
                activePlayer.GetGroundCombatPhase().DoGroundCombatPhase();
                turnInfoUI.UpdateValues();
                break;

            case Phase.Building:
                //Space Dock UI where player can choose a ship or ground force to build
                //button to build a new dock (one per turn)
                activePlayer.GetBuildingPhase().DoBuildingPhase();
                turnInfoUI.UpdateValues();
                break;
        }
    }

    public void InitializePlayers()
    {
        playersArray = new Player[playerDatas.Length];

        for(int i = 0; i < playerDatas.Length; i++)
        {
            Transform newPlayer = Instantiate(player, transform);

            newPlayer.GetComponent<Player>().SetName(playerDatas[i].Name);
            newPlayer.GetComponent<Player>().SetPlayerType(playerDatas[i].playerType);

            newPlayer.GetComponent<Player>().AddOre(playerDatas[i].oreAmount);
            newPlayer.GetComponent<Player>().AddFuel(playerDatas[i].fuelAmount);

            GridPosition gridPosition = new GridPosition(playerDatas[i].gridX, playerDatas[i].gridZ);
            GridObject homeSystem = MapController.Instance.GetGridObject(gridPosition);
            newPlayer.GetComponent<Player>().SetHomeSystem(homeSystem);

            foreach(var planet in homeSystem.GetPlanets())
            {
                newPlayer.GetComponent<Player>().AddPlanet(planet);
                UnitController.Instance.SpawnDock(gridPosition, playerDatas[i].playerType);
            }

            foreach(var ship in playerDatas[i].startingFleet)
            {
                UnitController.Instance.SpawnShip(ship, gridPosition, playerDatas[i].playerType);
            }

            for(int j = 0; j < playerDatas[i].startingGroundForces; j++)
            {
                UnitController.Instance.SpawnGroundForce(gridPosition, playerDatas[i].playerType);
            }


            
            playersArray[i] = newPlayer.GetComponent<Player>();
        }
    }
    public void InitializeFirstPlayer()
    {
        activePlayerIndex = 0;
        activePlayer = playersArray[activePlayerIndex];
        activePlayer.playerUI.Show();
        turnInfoUI.UpdateValues();
        //Debug.Log(activePlayerIndex);
    }

    public Player GetActivePlayer()
    {
        return activePlayer;
    }
    
    public Phase GetCurrentPhase()
    {
        return turnStateMachine.currentPhase;
    }

    public void StartPhase()
    {
        if(activePlayer != null)
        {
            activePlayer.playerUI.Hide();
        }

        if(activePlayerIndex == playersArray.Length - 1)        
        {
            activePlayerIndex = 0;
        }
        else
        {
            activePlayerIndex++;
        }
                

        activePlayer = playersArray[activePlayerIndex];
        activePlayer.playerUI.Show();

        activePlayer.GetStartPhase().DoStartPhase();
    }

    public void TurnCountPhase()
    {
        playerTurnService.IncrementTurnCounter(activePlayer.GetPlayerType());
        activePlayer.GetTurnCountPhase().DoTurnCountPhase();
    }

    public void ReplenishPhase()
    {
        activePlayer.GetResourcePhase().DoResourcePhase();
    }
}
