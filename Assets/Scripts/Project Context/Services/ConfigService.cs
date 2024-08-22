using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IConfigService
{
    MapData MapData {get;}
    PlayerData[] Players {get;} 
    UnitData[] Units{get;}
}
public class ConfigService : IConfigService
{
    public MapData MapData {get;}
    public PlayerData[] Players {get;}
    public UnitData[] Units{get;}

    public ConfigService(MapConfig mapConfig, PlayerConfig playerConfig, UnitConfig unitConfig)
    {
        MapData = mapConfig.MapData;
        Players = playerConfig.Players;
        Units = unitConfig.Units;
    }
}
