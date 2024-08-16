using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "MyConfigs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public PlayerData[] players;
}


[Serializable]
public struct PlayerData
{
    public string Name;
    public PlayerType playerType;

    public int gridX;
    public int gridZ;

    public GameObject[] startingFleet;
}
