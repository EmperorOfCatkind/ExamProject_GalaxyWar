using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public interface IMapFunctionalService
{
    GridSystem GridSystem {get;}
}
public class MapFunctionalService : IMapFunctionalService
{
    public GridSystem GridSystem {get; private set;}

    public MapFunctionalService(IConfigService configService)
    {
        var MapData = configService.MapData;
        GridSystem = new GridSystem(MapData.width, MapData.height, MapData.hexSize);
    }
}
