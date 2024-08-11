using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMono : MonoBehaviour
{
    private string name;
    private PlayerType playerType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupPlayer(Player player)
    {
        name = player.Name;
        playerType = player.PlayerType;

    }
    public override string ToString()
    {
        return name;
    }
}
