using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseAction : MonoBehaviour
{
    protected Ship ship;
    protected bool isActive;

    protected virtual void Awake()
    {
        ship = GetComponent<Ship>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
