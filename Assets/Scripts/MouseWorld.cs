using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld Instance;
    [SerializeField] private LayerMask hexGridLayerMask;
    [SerializeField] private LayerMask shipLayerMask;
    [SerializeField] private MapController mapController;

    private MapGridViewSingle lastMapGridViewSingle;
    //private MapGridView mapGridView;
    private GridSystem gridSystem;
    [SerializeField] GameObject visual;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gridSystem = ProjectContext.Instance.MapFunctionalService.GridSystem;
    }

    // Update is called once per frame
    void Update()
    {   
        HighlightOnHover();
        if(Input.GetMouseButtonDown(0))
        {
            SelectHex();
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, Instance.hexGridLayerMask);
        return raycastHit.point;
    }

    public void SelectHex()
    {    
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isOnGrid = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, hexGridLayerMask);

        if(isOnGrid)
        {
            GridPosition currentGridPosition = gridSystem.GetHexGridPosition(raycastHit.point);
            lastMapGridViewSingle = mapController.GetMapGridViewSingle(currentGridPosition);

            if(lastMapGridViewSingle != null)
            {
                lastMapGridViewSingle.OnClicked();
                /*Debug.Log("----------");
                Debug.Log(currentGridPosition);
                foreach(var neighbourHex in gridSystem.GetNeighbourHexesList())
                {
                    Debug.Log(neighbourHex);
                }*/
                /*foreach(var waypoint in lastMapGridViewSingle.spaceWaypoints)
                {
                    if(waypoint.GetHasUnit() == true)
                    {
                        Debug.Log(waypoint.GetHasUnit());
                    }
                }*/
                /*foreach(var SpaceWaypoint in lastMapGridViewSingle.GetGridObject().GetSpaceWaypointsList())
                {
                    Debug.Log(SpaceWaypoint.hasShip);
                }*/
            }
        }
    }
    public void HighlightOnHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isOnGrid = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, hexGridLayerMask);
        //Debug.Log(isOnGrid);
        if(isOnGrid)
        {
            visual.SetActive(true);
            transform.position = raycastHit.point;
            GridPosition currentGridPosition = gridSystem.GetHexGridPosition(raycastHit.point);
            if(gridSystem.IsInBounds(currentGridPosition))
            {
                if(lastMapGridViewSingle != null)
                {
                    lastMapGridViewSingle.Hide();
                }
                lastMapGridViewSingle = mapController.GetMapGridViewSingle(currentGridPosition);

                if(lastMapGridViewSingle != null)
                {
                    lastMapGridViewSingle.Show();
                }
            
                gridSystem.GetHexGridPosition(raycastHit.point);
            }
            
        }
        else
        {
            if(lastMapGridViewSingle != null)
            {
                lastMapGridViewSingle.Hide();
            }
            visual.SetActive(false);
        }
    }
}
