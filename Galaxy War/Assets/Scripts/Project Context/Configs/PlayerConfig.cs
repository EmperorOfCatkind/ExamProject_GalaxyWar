using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerConfig", menuName = "MyConfigs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public Player[] Players;
}
