using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private SelectedVisual selectedVisual;

    private GridPosition gridPosition;

    private MoveAction moveAction;

    void Awake()
    {
        moveAction = GetComponent<MoveAction>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gridPosition = MapController.Instance.GetHexGridPosition(transform.position);
        MapController.Instance.AddShipAtGridPosition(gridPosition, this);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Selected()
    {
        selectedVisual.Show();
    }
    public void Deselected()
    {
        selectedVisual.Hide();
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }
}
