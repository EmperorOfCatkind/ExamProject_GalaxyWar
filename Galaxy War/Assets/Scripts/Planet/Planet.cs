using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private int oreValue;
    [SerializeField] private int fuelValue;
    [SerializeField] private TextMeshPro oreText;
    [SerializeField] private TextMeshPro fuelText;
    // Start is called before the first frame update
    void Start()
    {
        SetValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValues()
    {
        oreText.text = oreValue.ToString();
        fuelText.text = fuelValue.ToString();
    }
}
