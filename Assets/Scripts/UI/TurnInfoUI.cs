using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CurrentPlayer;
    [SerializeField] private TextMeshProUGUI CurrentPhase;

    private PlayerTurnController playerTurnController;
    // Start is called before the first frame update
    void Start()
    {
        playerTurnController = GetComponent<PlayerTurnController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateValues()
    {
        CurrentPlayer.text = playerTurnController.GetActivePlayer().GetName();
        CurrentPhase.text = playerTurnController.GetCurrentPhase().ToString();
    }
}
