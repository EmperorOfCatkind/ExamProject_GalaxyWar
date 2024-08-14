using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeShip : MonoBehaviour, IShip
{
    [SerializeField] public SelectedVisual selectedVisual;

    public GridPosition gridPosition { get; set; }
    public string Name { get; set; }
    public SpaceWaypoint currentWaypoint { get; set; }

    public string GetName()
    {
        return Name;
    }

    // Start is called before the first frame update
    void Start()
    {
        gridPosition = MapController.Instance.GetHexGridPosition(transform.position);
        Name = "Prototype";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideSelectedVisual()
    {
        selectedVisual.Hide();
    }

    public void SetCurrentWaypoint(SpaceWaypoint currentWaypoint)
    {
        this.currentWaypoint = currentWaypoint;
    }
    public SpaceWaypoint GetCurrentWaypoint()
    {
        return currentWaypoint;
    }
    public Vector3 GetCurrentWorldPosition()
    {
        return transform.position;
    }
    public void SetCurrentWorldPosition(Vector3 position, float speed)
    {
        transform.forward = Vector3.Lerp(transform.forward, position, Time.deltaTime * speed);
        transform.position += position * speed * Time.deltaTime;
    }
}
