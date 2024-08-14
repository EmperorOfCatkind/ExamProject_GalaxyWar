using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeShip : MonoBehaviour, IShip
{
    [SerializeField] public SelectedVisual selectedVisual;

    public GridPosition gridPosition { get; set; }
    public string Name { get; set; }

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
}
