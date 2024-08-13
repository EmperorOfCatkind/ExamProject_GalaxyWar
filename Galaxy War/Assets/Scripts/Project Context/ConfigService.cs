using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IConfigService
{
    MapData MapData {get;}
}
public class ConfigService : IConfigService
{
    public MapData MapData {get;}

    public ConfigService(MapConfig mapConfig)
    {
        MapData = mapConfig.MapData;
    }
}
