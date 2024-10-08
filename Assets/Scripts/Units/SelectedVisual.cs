using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedVisual : MonoBehaviour
{   
    [SerializeField] public MeshRenderer meshRenderer;
    [SerializeField] public Material selectedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
        meshRenderer.material = selectedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Show()
    {
        meshRenderer.enabled = true;
    }
    public void Hide()
    {
        meshRenderer.enabled = false;
    }
}
