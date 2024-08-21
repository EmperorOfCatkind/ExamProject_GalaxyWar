using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDockWaypoint : MonoBehaviour
{
    private GridPosition gridPosition;
    private GridObject gridObject;
    [SerializeField] private MeshRenderer meshRenderer;
    public bool hasDock;
    void Awake()
    {
        hasDock = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public MeshRenderer GetMesh()
    {
        return meshRenderer;
    }
}
