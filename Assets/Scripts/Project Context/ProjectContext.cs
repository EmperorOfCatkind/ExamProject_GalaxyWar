using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectContext
{
    private static ProjectContext instance;

    public static ProjectContext Instance
    {
        get{
            if(instance == null)
            instance = new ProjectContext();
            return instance;
        }
    }
    public IConfigService ConfigService {get; private set;}

    public IMapFunctionalService MapFunctionalService {get; private set;}
    public IMapVisualService MapVisualService {get; private set;}
    
    public IPlayerService PlayerService {get; private set;}

    private ProjectContext()
    {

    }

    public void Initialize(MapConfig mapConfig, PlayerConfig playerConfig)    //TODO - pass the configs through here
    {
        ConfigService = new ConfigService(mapConfig, playerConfig);

        MapFunctionalService = new MapFunctionalService(ConfigService);
        MapVisualService = new MapVisualService(MapFunctionalService);

        PlayerService = new PlayerService(ConfigService);
    }
}

