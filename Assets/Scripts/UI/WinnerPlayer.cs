using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText;
    // Start is called before the first frame update
    void Start()
    {
        SetWinnerText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWinnerText()
    {
        winnerText.text = "Player " + ProjectContext.Instance.PlayerTurnService.winner.GetName() + " is the winner!";
    }
}
