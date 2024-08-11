using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public interface IConfigService
{
    MapData MapData {get;}
    Player[] Players {get;}
}
public class ConfigService : IConfigService
{
    public MapData MapData {get;}

    public Player[] Players {get;}

    public ConfigService(MapConfig mapConfig, PlayerConfig playerConfig)
    {
        MapData = mapConfig.MapData;
        Players = playerConfig.Players;
    }
}
