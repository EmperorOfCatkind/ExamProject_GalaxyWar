using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedVisual : MonoBehaviour
{
    [SerializeField] MeshRenderer selectedVisual;
    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        selectedVisual.enabled = true;
    }
    public void Hide()
    {
        selectedVisual.enabled = false;
    }
}
