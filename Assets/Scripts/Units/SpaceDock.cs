using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDock : MonoBehaviour
{
    [SerializeField] private SelectedVisual selectedVisual;

    private GridPosition gridPosition;

    //Planet
    // Start is called before the first frame update
    void Start()
    {
        gridPosition = MapController.Instance.GetHexGridPosition(transform.position);
        //Planet
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Selected()
    {
        selectedVisual.Show();
    }
    public void Deselected()
    {
        selectedVisual.Hide();
    }
}
