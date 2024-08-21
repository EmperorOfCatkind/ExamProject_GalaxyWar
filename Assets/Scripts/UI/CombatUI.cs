using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI phaseText;
    [SerializeField] Button nextPhaseButton;
    [SerializeField] TextMeshProUGUI hitsByPlayerOne;
    [SerializeField] TextMeshProUGUI hitsByPlayerTwo;
    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        phaseText.text = "Combat step " + PlayerTurnController.Instance.GetCurrentCombatPhase().ToString();
    }

     public void Hide()
    {
        phaseText.enabled = false;
        nextPhaseButton.gameObject.SetActive(false);
        hitsByPlayerOne.enabled = false;
        hitsByPlayerTwo.enabled = false;
    }
    public void Show()
    {
        phaseText.enabled = true;
        nextPhaseButton.gameObject.SetActive(true);
        hitsByPlayerOne.enabled = true;
        hitsByPlayerTwo.enabled = true;
    }
    
    public void UpdateHits(int p1_hits, int p2_hits)
    {
        hitsByPlayerOne.text = "Unassigned hits from Jim  " + p1_hits.ToString();
        hitsByPlayerTwo.text = "Unassigned hits from Arkturus " + p2_hits.ToString();
    }

}
