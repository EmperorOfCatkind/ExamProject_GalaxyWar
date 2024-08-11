using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Player
{
    [field: SerializeField] public string Name {get; private set;}
    [field: SerializeField] public PlayerType PlayerType {get; private set;}

}
