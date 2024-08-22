using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    
    [SerializeField] private MapConfig MapConfig;
    [SerializeField] private PlayerConfig PlayerConfig;
    [SerializeField] private UnitConfig UnitConfig;
    

    void Awake()
    {
        ProjectContext.Instance.Initialize(MapConfig, PlayerConfig, UnitConfig);
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
}
