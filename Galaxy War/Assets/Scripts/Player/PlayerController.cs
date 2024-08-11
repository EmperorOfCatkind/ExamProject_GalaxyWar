using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private PlayerMono prefab;

    void Awake()
    {
        var players = ProjectContext.Instance.ConfigService.Players;
        foreach (var player in players)
        {
            var playerMono = Instantiate(prefab, parent);
            playerMono.SetupPlayer(player);
            Debug.Log(playerMono.ToString());
        }
    }
}
