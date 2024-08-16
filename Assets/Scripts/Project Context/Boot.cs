using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    //TODO - Add Configs
    [SerializeField] private MapConfig MapConfig;
    [SerializeField] private PlayerConfig PlayerConfig;
    //TODO - Add Configs

    void Awake()
    {
        ProjectContext.Instance.Initialize(MapConfig, PlayerConfig);
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
}
