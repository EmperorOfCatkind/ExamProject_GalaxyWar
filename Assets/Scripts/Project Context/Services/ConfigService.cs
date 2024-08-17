using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IConfigService
{
    MapData MapData {get;}
    PlayerData[] Players {get;} 
}
public class ConfigService : IConfigService
{
    public MapData MapData {get;}
    public PlayerData[] Players {get;}

    public ConfigService(MapConfig mapConfig, PlayerConfig playerConfig)
    {
        MapData = mapConfig.MapData;
        Players = playerConfig.players;
    }
}
