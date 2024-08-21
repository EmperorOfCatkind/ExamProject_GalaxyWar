using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePhase : MonoBehaviour
{
    protected Player player;
    protected bool isActive;
    public string phaseName;
    protected string debugString;

    protected virtual void Awake()
    {
        player = GetComponent<Player>();
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
