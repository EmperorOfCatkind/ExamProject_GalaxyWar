using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundForce : MonoBehaviour
{
    [SerializeField] private SelectedVisual selectedVisual;

    private GridPosition gridPosition;
    private GridObject gridObject;
    [SerializeField] private PlayerType playerType;

    [SerializeField] private Planet planet;
    // Start is called before the first frame update
    void Start()
    {
        gridPosition = MapController.Instance.GetHexGridPosition(transform.position);
        
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

    public void SetPlanet(Planet planet)
    {
        this.planet = planet;
    }
    public Planet GetPlanet()
    {
        return planet;
    }

    public PlayerType GetPlayerType()
    {
        return playerType;
    }
    public void SetPlayerType(PlayerType playerType)
    {
        this.playerType = playerType;
    }
    
}
