using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "MyConfigs/UnitConfig")]
public class UnitConfig : ScriptableObject
{
    public UnitData[] Units;
}


[Serializable]
public struct UnitData
{
    public string unitClass;
    public GameObject prefab;
}