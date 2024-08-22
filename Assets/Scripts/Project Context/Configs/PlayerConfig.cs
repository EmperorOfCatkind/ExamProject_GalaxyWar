using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "MyConfigs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public PlayerData[] Players;
}


[Serializable]
public struct PlayerData
{
    public string Name;
    public PlayerType playerType;

    public int gridX;
    public int gridZ;

    public GameObject[] startingFleet;

    public int startingGroundForces;

    public int oreAmount;
    public int fuelAmount;
}
