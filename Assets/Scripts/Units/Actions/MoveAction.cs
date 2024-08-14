using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] float movingSpeed;
    [SerializeField] float stoppingDistance;
    [SerializeField] float rotatingSpeed;
    [SerializeField] Vector3 shipYOffset;
    private Vector3 destination;


    protected override void Awake()
    {
        base.Awake();
        destination = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        movingSpeed = 8f;
        stoppingDistance = 0.1f;
        rotatingSpeed = 10f;
        shipYOffset = new Vector3(0,3,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            return;
        }

        Vector3 moveDirection = (destination - transform.position).normalized;
        if(Vector3.Distance(transform.position, destination) > stoppingDistance)
        {
            transform.position += moveDirection * Time.deltaTime * movingSpeed;
        }
        else
        {
            isActive = false;
        }
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotatingSpeed);
    }

    public void Move(GridPosition gridPosition)
    {
        destination = MapController.Instance.GetWorldPosition(gridPosition) + shipYOffset;
        isActive = true;
    }
}
