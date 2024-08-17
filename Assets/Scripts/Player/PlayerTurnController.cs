using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerTurnController : MonoBehaviour
{
    public static PlayerTurnController Instance;

    private IPlayerTurnService playerService;
    private PlayerData[] playerDatas;
    public TurnStateMachine<Phase, Trigger> turnStateMachine;


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

        playerService = ProjectContext.Instance.PlayerTurnService;
        playerDatas = playerService.players;
        turnStateMachine = playerService.turnStateMachine;
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
        InitializeFirstPlayer();        //don't work correctly
        

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
        switch(phaseTransitionData.NextPhase)
        {
            case Phase.Start:   //if Start is the next phase, change the active player
                //Debug.Log(activePlayerIndex);
                //Debug.Log(playersArray.Length);
                if(activePlayer != null)
                {
                    activePlayer.playerUI.Hide();
                }

                if(activePlayerIndex == playersArray.Length - 1)        //index out of bounds
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

                turnInfoUI.UpdateValues();
                break;

            case Phase.TurnCount:
                activePlayer.GetTurnCountPhase().DoTurnCountPhase();
                turnInfoUI.UpdateValues();
                break;

            case Phase.Replenish:
                activePlayer.GetResourcePhase().DoResourcePhase();
                turnInfoUI.UpdateValues();
                break;

            case Phase.Move:
                activePlayer.GetMovePhase().DoMovePhase();
                turnInfoUI.UpdateValues();
                break;

            case Phase.SpaceCombat:
                activePlayer.GetSpaceCombatPhase().DoSpaceCombatPhase();
                turnInfoUI.UpdateValues();
                break;
                
            case Phase.GroundCombat:
                activePlayer.GetGroundCombatPhase().DoGroundCombatPhase();
                turnInfoUI.UpdateValues();
                break;

            case Phase.Building:
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
                planet.SetOwner(playerDatas[i].playerType);
                UnitController.Instance.SpawnDock(gridPosition, playerDatas[i].playerType);
            }

            foreach(var ship in playerDatas[i].startingFleet)
            {
                UnitController.Instance.SpawnShip(ship, gridPosition, playerDatas[i].playerType);
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
}
