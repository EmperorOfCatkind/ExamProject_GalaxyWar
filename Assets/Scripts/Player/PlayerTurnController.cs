using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerTurnController : MonoBehaviour
{
    public static PlayerTurnController Instance;
    private IPlayerService playerService;
    private PlayerData[] playerDatas;


    [SerializeField] private Transform player;
    private Player[] playersArray;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerService = ProjectContext.Instance.PlayerService;
        playerDatas = playerService.players;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializePlayers();
        /*foreach(var player in playersArray)
        {
            Debug.Log(player.GetHomeSystem().ToString());
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializePlayers()
    {
        playersArray = new Player[playerDatas.Length];

        for(int i = 0; i < playerDatas.Length; i++)
        {
            Transform newPlayer = Instantiate(player, transform);
            newPlayer.GetComponent<Player>().SetName(playerDatas[i].Name);
            newPlayer.GetComponent<Player>().SetPlayerType(playerDatas[i].playerType);
            

            GridPosition gridPosition = new GridPosition(playerDatas[i].gridX, playerDatas[i].gridZ);
            GridObject homeSystem = MapController.Instance.GetGridObject(gridPosition);
            newPlayer.GetComponent<Player>().SetHomeSystem(homeSystem);

            foreach(var planet in homeSystem.GetPlanets())
            {
                planet.SetOwner(playerDatas[i].playerType);
                UnitController.Instance.SpawnDock(gridPosition, playerDatas[i].playerType);
            }

            foreach(var ship in playerDatas[i].startingFleet)
            {
                UnitController.Instance.SpawnShip(ship, gridPosition, playerDatas[i].playerType);
            }




            
            playersArray[i] = newPlayer.GetComponent<Player>();
        }
    }
}
