using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld Instance;


    [SerializeField] private LayerMask hexGridLayerMask;
    [SerializeField] private LayerMask shipLayerMask;
    [SerializeField] private LayerMask dockLayerMask;
    [SerializeField] private LayerMask groundForceLayerMask;
    [SerializeField] private LayerMask UILayerMask;

    [SerializeField] private MapController mapController;

    private MapGridViewSingle lastMapGridViewSingle;
    private GridSystem gridSystem;
    [SerializeField] public GameObject visual;

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
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        HighlightOnHover(GetLayerMask());
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
        
        if(!isOnGrid)
        {
            return;
        }

        GridPosition currentGridPosition = gridSystem.GetHexGridPosition(raycastHit.point);
        lastMapGridViewSingle = mapController.GetMapGridViewSingle(currentGridPosition);

        if(lastMapGridViewSingle != null)
        {
            lastMapGridViewSingle.OnClicked();
        }
        
    }
    public void HighlightHex()
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


    public void HighlightOnHover(LayerMask layerMask)
    {
        if(layerMask != hexGridLayerMask || layerMask == UILayerMask)
        {
            if(lastMapGridViewSingle != null)
            {
                lastMapGridViewSingle.Hide();
            }
        }
        else if(layerMask == hexGridLayerMask && layerMask != UILayerMask)
        {
            HighlightHex();
            if(Input.GetMouseButtonDown(0))
            {
                SelectHex();
            }
        }
    }

    public LayerMask GetLayerMask()
    {
        LayerMask hitLayerMask = default;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            int layer = hit.collider.gameObject.layer;
            hitLayerMask = 1 << layer;
        }

        return hitLayerMask;
    }

    public void Reset()
    {
        visual.GetComponent<Renderer>().material.color = Color.green;
    }
}
