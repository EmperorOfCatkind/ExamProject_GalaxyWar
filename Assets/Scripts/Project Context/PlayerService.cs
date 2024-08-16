using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPlayerService
{
    PlayerData[] players {get;}
}
public class PlayerService : IPlayerService
{
    public PlayerData[] players {get; private set;}
    public PlayerService(IConfigService configService)
    {
        players = configService.Players;
    }
}
