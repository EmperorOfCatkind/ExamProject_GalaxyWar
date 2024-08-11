using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridViewSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private Material materialOnHover;  
    [SerializeField] private Material materialOnClick;

    public void Show()
    {
        meshRenderer.enabled = true;
    }
    public void Hide()
    {
        meshRenderer.enabled = false;
    }

    public void OnClicked() //TODO - make material change work
    {
        meshRenderer.material = materialOnClick;
        Invoke(nameof(OnClickedAfter), 0.5f);
    }

    private void OnClickedAfter()
    {
        Hide();
        meshRenderer.material = materialOnHover;
    }
}
