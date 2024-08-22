using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerTurnController : MonoBehaviour
{
    public static PlayerTurnController Instance;

    private IPlayerTurnService playerTurnService;
    private PlayerData[] playerDatas;
    public StateMachine<Phase, Trigger> turnStateMachine;
    public StateMachine<CombatPhase, CombatTrigger> combatStateMachine;
    public PhaseTransitionData<Phase, Trigger> phaseTransitionData;
    public PhaseTransitionData<CombatPhase, CombatTrigger> combatPhaseTransitionData;

    private Trigger phaseTrigger;
    private CombatTrigger combatPhaseTrigger;


    [SerializeField] private Transform player;
    private Player[] playersArray;

    private int activePlayerIndex;
    private Player activePlayer;

    private TurnInfoUI turnInfoUI;
    [SerializeField] CombatUI combatUI;
    [SerializeField] public Button buildDockButton;

    private Dictionary<PlayerType, Material> playerMaterials;
    
    [SerializeField] private Material playerOneMaterial;
    [SerializeField] private Material playerTwoMaterial;
    [SerializeField] public CameraController cameraController;

    private GridObject combatGridObject;
    private List<Ship> shipsToDestroy;

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
        combatStateMachine = playerTurnService.combatStateMachine;

        turnInfoUI = GetComponent<TurnInfoUI>();

        playerMaterials = new Dictionary<PlayerType, Material>
        {
            {PlayerType.PlayerOne, playerOneMaterial},
            {PlayerType.PlayerTwo, playerTwoMaterial},
        };

        shipsToDestroy = new List<Ship>();
        buildDockButton.gameObject.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        /*InitializePlayers();
        turnStateMachine.OnPhaseChanged += OnPhaseChanged;
        InitializeFirstPlayer();*/     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhaseChanged(PhaseTransitionData<Phase, Trigger> phaseTransitionData)        //Get a phase class from an active player, activate it and go through it.
    {
        this.phaseTransitionData = phaseTransitionData;
        switch(phaseTransitionData.NextPhase)
        {
            case Phase.Start:   //if Start is the next phase, change the active player
                buildDockButton.gameObject.SetActive(false);
                StartPhase();
                turnInfoUI.UpdateValues();
                SetTrigger(Trigger.ToTurnCount);
                break;

            case Phase.TurnCount:
                TurnCountPhase();
                turnInfoUI.UpdateValues();
                SetTrigger(Trigger.ToReplenish);
                break;

            case Phase.Replenish:
                ReplenishPhase();
                turnInfoUI.UpdateValues();
                SetTrigger(Trigger.ToMove);
                break;

            case Phase.Move:
                //get ships move only once per turn
                //get ground forces to embark and disembark
                //by the end if there is a Hex (gridObject) ships belonging to both players add it to the List for the next phase to use
                //if there is a planet with ground forces of both players add it to the list for the next next phase to use
                activePlayer.GetMovePhase().DoMovePhase();
                turnInfoUI.UpdateValues();
                SetTrigger(Trigger.ToSpaceCombat);
                break;

            case Phase.SpaceCombat:
                UnitController.Instance.DropAllSelections();
                InitializeSpaceCombat();
                
                
                turnInfoUI.UpdateValues();
                SetTrigger(Trigger.ToGroundCombat);
                break;
                
            case Phase.GroundCombat:
                //create a combat feature
                foreach(var gridObject in MapController.Instance.GetAllGridObjects())
                {
                    if(!gridObject.GetShipListByPlayerType().ContainsKey(activePlayer.GetPlayerType()))
                    {
                        continue;
                    }

                    foreach(var planet in gridObject.GetPlanets())
                    {
                        if(planet.GetSpaceDock() != null && planet.GetSpaceDock().GetPlayerType() != activePlayer.GetPlayerType())
                        {
                            SpaceDock spaceDockToDestroy = planet.GetSpaceDock();
                            planet.RemoveSpaceDock();
                            Destroy(spaceDockToDestroy.GameObject());
                            planet.SetSpaceDock(null);
                        }
                        if(planet.GetOwner() != activePlayer.GetPlayerType())
                        {
                            CaptureHex(gridObject, activePlayer.GetPlayerType());
                        }
                    }
                }
                turnInfoUI.UpdateValues();
                SetTrigger(Trigger.ToBuilding);
                break;

            case Phase.Building:
                //Space Dock UI where player can choose a ship or ground force to build
                //buildDockButton.gameObject.SetActive(true);
                
                
                turnInfoUI.UpdateValues();
                if(CheckWinCondition())
                {
                    playerTurnService.SetWinner(activePlayer);
                    SetTrigger(Trigger.EndGame);
                }
                else
                {
                    SetTrigger(Trigger.ToStart);
                }
                break;

            case Phase.EndScreen:
                SceneManager.LoadScene("EndGame");
                break;    
        }
    }
    

    public void OnCombatPhaseChanged(PhaseTransitionData<CombatPhase, CombatTrigger> phaseTransitionData)
    {
        combatPhaseTransitionData = phaseTransitionData;
        switch(phaseTransitionData.NextPhase)
        {
            case CombatPhase.Start:
            turnInfoUI.Hide();
            combatUI.Show();
            SpaceCombatPhase();
            SetCombatTrigger(CombatTrigger.ToRoll);
            break;

            case CombatPhase.Roll:
            foreach(var ships in combatGridObject.GetShipListByPlayerType())
            {
                activePlayer.GetSpaceCombatPhase().MakeCombatRolls(ships.Value);
            }
            combatUI.UpdateHits(activePlayer.GetSpaceCombatPhase().GetHits()[PlayerType.PlayerOne], activePlayer.GetSpaceCombatPhase().GetHits()[PlayerType.PlayerTwo]);
            SetCombatTrigger(CombatTrigger.ToAssign);
            break;

            case CombatPhase.Assign:
            AssignHits();
            SetCombatTrigger(CombatTrigger.ToDestroy);
            break;

            case CombatPhase.Destroy:
            DestroyShips();
            SetCombatTrigger(CombatTrigger.ToEnd);
            break;

            case CombatPhase.End:
            foreach(var ships in combatGridObject.GetShipListByPlayerType())
            {
                foreach(var ship in ships.Value)
                {
                    ship.ResetRollText();
                }
            } 
            if(combatGridObject.GetShipListByPlayerType().ContainsKey(PlayerType.PlayerOne) && combatGridObject.GetShipListByPlayerType().ContainsKey(PlayerType.PlayerTwo))
            {
                SetCombatTrigger(CombatTrigger.ToNextRound);
            }
            else
            {
                cameraController.EndCombatMode();
                turnInfoUI.Show();
                combatUI.Hide();
                activePlayer.GetSpaceCombatPhase().ResetCounters();
                turnStateMachine.SetOffTrigger(Trigger.ToGroundCombat);                       
                SetCombatTrigger(CombatTrigger.Finish);

                if(DefineWinner(combatGridObject) != null)
                {
                    CaptureHex(combatGridObject, DefineWinner(combatGridObject).GetPlayerType());
                }

                if(MapController.Instance.GatherHexesForCombat().Count != 0)
                {
                    InitializeSpaceCombat();
                }
            }
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
                planet.GetSpaceDockWaypoint().GetMesh().material = playerMaterials[playerDatas[i].playerType];
            }


            playersArray[i] = newPlayer.GetComponent<Player>();
        }
    }

    public void InitializePlayersFleets()
    {
        for(int i = 0; i < playerDatas.Length; i++)
        {
            GridPosition gridPosition = new GridPosition(playerDatas[i].gridX, playerDatas[i].gridZ);
            foreach(var ship in playerDatas[i].startingFleet)
            {
                UnitController.Instance.SpawnShip(ship, gridPosition, playerDatas[i].playerType);
            }

            for(int j = 0; j < playerDatas[i].startingGroundForces; j++)
            {
                UnitController.Instance.SpawnGroundForce(gridPosition, playerDatas[i].playerType);
            }
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
    public Player GetSpecificPlayer(PlayerType playerType)
    {
        Player temp = new Player();
        foreach(var player in playersArray)
        {
            if (player.GetPlayerType() == playerType)
            temp = player;
        }
        return temp;
    }
    public Player GetActivePlayer()
    {
        return activePlayer;
    }
    public Trigger GetTrigger()
    {
        return phaseTrigger;
    }
    public void SetTrigger(Trigger trigger)
    {
        phaseTrigger = trigger;
    }
    public CombatTrigger GetCombatTrigger()
    {
        return combatPhaseTrigger;
    }
    public void SetCombatTrigger(CombatTrigger combatTrigger)
    {
        combatPhaseTrigger = combatTrigger;
    }

    public Phase GetCurrentPhase()
    {
        return turnStateMachine.currentPhase;
    }
    public CombatPhase GetCurrentCombatPhase()
    {
        return combatStateMachine.currentPhase;
    }
    public GridObject GetCombatGridObject()
    {
        return combatGridObject;
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

    public void InitializeSpaceCombat()
    {
        if(MapController.Instance.GatherHexesForCombat().Count > 0)
        {
            combatStateMachine.SetOffTrigger(CombatTrigger.ToNextRound);
        }
    }
    public void SpaceCombatPhase()
    {
        combatGridObject = MapController.Instance.GatherHexesForCombat()[0];
        activePlayer.GetSpaceCombatPhase().LaunchSpaceCombatPhase();       
    }
    public void AssignHits()
    {
        Dictionary<PlayerType, int> hits = activePlayer.GetSpaceCombatPhase().GetHits();

        foreach(var player in playersArray)
        {
            Player oppositePlayer = null;
            
            for(int i = 0; i < playersArray.Length; i++)
            {
                if(playersArray[i].GetPlayerType() != player.GetPlayerType())
                {
                    oppositePlayer = playersArray[i];
                }
            }
            
            List<Ship> oppositePlayerShips = combatGridObject.GetShipListByPlayerType()[oppositePlayer.GetPlayerType()];
            if(hits[player.GetPlayerType()] > 0)
            {
                foreach(var ship in oppositePlayerShips)
                {
                    if(ship.isSelected == false)
                    {
                        ship.SelectedToDestroy();
                        shipsToDestroy.Add(ship);
                        hits[player.GetPlayerType()]--;
                        if(hits[player.GetPlayerType()] == 0)
                        {
                            break;
                        }
                    }
                }
            }   
        }
    }

    public void DestroyShips()
    {
        foreach(var ship in shipsToDestroy)
        {
            Player owner = GetSpecificPlayer(ship.GetPlayerType());
            owner.RemoveShip(ship);
            combatGridObject.RemoveShip(ship);
            Destroy(ship.GameObject());
        }
        shipsToDestroy.Clear();
    }

    public Player DefineWinner(GridObject gridObject)
    {
        if (gridObject.GetShipListByPlayerType().ContainsKey(PlayerType.PlayerOne))
        {
            return GetSpecificPlayer(PlayerType.PlayerOne);
        }
        else if (gridObject.GetShipListByPlayerType().ContainsKey(PlayerType.PlayerTwo))
        {
            return GetSpecificPlayer(PlayerType.PlayerTwo);
        }
        else
        {
            return null;
        }
    }

    public void CaptureHex(GridObject gridObject, PlayerType playerType)
    {
        foreach(var planet in gridObject.GetPlanets())
        {
            DefineWinner(gridObject).AddPlanet(planet);
            planet.GetSpaceDockWaypoint().GetMesh().material = playerMaterials[playerType];
        }
    }

    public bool CheckWinCondition()
    {
        bool activePlayerWon = false;
        Player oppositePlayer = null;

        foreach(var player in playersArray)
        {
            if(activePlayer.GetPlayerType() != player.GetPlayerType())
            {
                oppositePlayer = player;
            }
        }

        foreach(var planet in oppositePlayer.GetHomeSystem().GetPlanets())
        {
            if(planet.GetOwner() == activePlayer.GetPlayerType())
            {
                activePlayerWon = true;
            }

            else if (planet.GetOwner() == oppositePlayer.GetPlayerType())
            {
                activePlayerWon = false;
                break;
            }
        }
        return activePlayerWon;
    }
}
