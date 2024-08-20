using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_GameMaster : MonoBehaviour
{
    private MapController mapController;
    private UnitController unitController;
    private PlayerTurnController playerTurnController;
    // Start is called before the first frame update
    void Start()
    {
        mapController = MapController.Instance;
        unitController = UnitController.Instance;
        playerTurnController = PlayerTurnController.Instance;

        playerTurnController.InitializePlayers();
        playerTurnController.turnStateMachine.OnPhaseChanged += playerTurnController.OnPhaseChanged;
        playerTurnController.InitializePlayersFleets();
        playerTurnController.InitializeFirstPlayer();

        /*unitController.SpawnShip(unitController.shipPrefab, new GridPosition(0,1), PlayerType.PlayerOne); //debug purposes
        unitController.SpawnShip(unitController.shipPrefab, new GridPosition(0,1), PlayerType.PlayerOne); //debug purposes
        unitController.SpawnShip(unitController.shipPrefab, new GridPosition(0,1), PlayerType.PlayerOne); //debug purposes*/

        /*unitController.SpawnShip(unitController.shipPrefab, new GridPosition(0,1), PlayerType.PlayerTwo); //debug purposes
        unitController.SpawnShip(unitController.shipPrefab, new GridPosition(0,1), PlayerType.PlayerTwo); //debug purposes
        unitController.SpawnShip(unitController.shipPrefab, new GridPosition(0,1), PlayerType.PlayerTwo); //debug purposes*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
