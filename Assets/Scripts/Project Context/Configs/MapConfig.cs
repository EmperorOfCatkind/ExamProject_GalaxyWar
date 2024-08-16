using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MapConfig", menuName = "MyConfigs/MapConfig")]
public class MapConfig : ScriptableObject
{
    public MapData MapData;
}

[Serializable]
public struct MapData
{
    public int width;
    public int height;
    public float hexSize;
}
