using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentPlayer;
    [SerializeField] private TextMeshProUGUI currentPhase;
    [SerializeField] private TextMeshProUGUI turnCounter;

    private PlayerTurnController playerTurnController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateValues()
    {
        playerTurnController = GetComponent<PlayerTurnController>();

        currentPlayer.text = playerTurnController.GetActivePlayer().GetName();
        currentPhase.text = playerTurnController.GetCurrentPhase().ToString();

        turnCounter.text = "Turn: " + ProjectContext.Instance.PlayerTurnService.GetTurnCounter(playerTurnController.GetActivePlayer().GetPlayerType()).ToString();
    }
}
