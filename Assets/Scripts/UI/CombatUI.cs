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
    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void Hide()
    {
        phaseText.enabled = false;
        nextPhaseButton.gameObject.SetActive(false);
    }
    public void Show()
    {
        phaseText.enabled = true;
        nextPhaseButton.gameObject.SetActive(true);
    }
}
