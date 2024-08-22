using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildDockButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBuilding()
    {
        MouseWorld.Instance.visual.GameObject().GetComponent<Renderer>().material.color = Color.red;
        UnitController.Instance.isBuilding = true;
    }
}
