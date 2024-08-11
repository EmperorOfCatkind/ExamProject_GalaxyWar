using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsController : MonoBehaviour
{
    Dictionary<SpaceWaypoint, PlayerType> spaceWaypoints;
    [SerializeField] SpaceWaypoint[] spaceWaypointsOnHex;
    void Awake()
    {
               
    }
    // Start is called before the first frame update
    void Start()
    {
        spaceWaypoints = new Dictionary<SpaceWaypoint, PlayerType>();

        var players = ProjectContext.Instance.ConfigService.Players;
        foreach(var player in players)
        {
            for(int i = 0; i < spaceWaypointsOnHex.Length; i++)
            {
                if(spaceWaypointsOnHex[i].playerType == player.PlayerType)
                {
                    spaceWaypoints.Add(spaceWaypointsOnHex[i], player.PlayerType);
                }
            }
        }

        /*foreach (KeyValuePair<SpaceWaypoint, PlayerType> pair in spaceWaypoints)
        {
            Debug.Log("Player: " + pair.Key + "; Waypoint " + pair.Value);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
