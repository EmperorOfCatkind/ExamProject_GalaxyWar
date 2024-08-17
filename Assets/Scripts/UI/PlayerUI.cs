using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI oreDisplay;
    [SerializeField] private TextMeshProUGUI fuelDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateResourceValues();
    }

    public void UpdateResourceValues()
    {
        oreDisplay.text = GetComponent<Player>().GetOre().ToString();
        fuelDisplay.text = GetComponent<Player>().GetFuel().ToString();
    }

    public void Hide()
    {
        oreDisplay.enabled = false;
        fuelDisplay.enabled = false;
    }
    public void Show()
    {
        oreDisplay.enabled = true;
        fuelDisplay.enabled = true;
    }
}
