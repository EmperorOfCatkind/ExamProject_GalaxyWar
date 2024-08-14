using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridViewSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private Material materialOnHover;  
    [SerializeField] private Material materialOnClick;

    private GridObject gridObject;  //not sure if I need this one

    [SerializeField] public SpaceWaypoint[] spaceWaypoints;
    void Start()
    {
        
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

    public SpaceWaypoint GetAvailableWaypoint()
    {
        for(int i = 0; i < spaceWaypoints.Length; i++)
        {
            if (spaceWaypoints[i].GetHasUnit() == false)
            {
                return spaceWaypoints[i];
            }
        }
        
        Debug.Log("All waypoints occupied");
        return null;
    }
}
