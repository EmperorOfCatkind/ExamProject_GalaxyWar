using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridViewSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private Material materialOnHover;  
    [SerializeField] private Material materialOnClick;

    [SerializeField] private SpaceWaypoint[] spaceWaypoints; 

    private GridObject gridObject;  

    void Start()
    {
        foreach(var spaceWaypoint in spaceWaypoints)
        {
            spaceWaypoint.SetGridObject(gridObject);
        }
    }
    public void Show()
    {
        meshRenderer.enabled = true;
    }
    public void Hide()
    {
        meshRenderer.enabled = false;
    }

    public void OnClicked() //TODO - make material change work
    {
        meshRenderer.material = materialOnClick;
        Invoke(nameof(OnClickedAfter), 0.5f);
    }

    private void OnClickedAfter()
    {
        Hide();
        meshRenderer.material = materialOnHover;
    }

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
    public GridObject GetGridObject()
    {
        return gridObject;
    }

    public SpaceWaypoint[] GetSpaceWaypoints()
    {
        return spaceWaypoints;
    }
}
