using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapGridCoordinates : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    private GridObject gridObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = gridObject.ToString();
    }

    public void SetCoordinates(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
}
