using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComp : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 moveDir = Vector3.forward;

    Vector3 Destination;
    void Start()
    {
        
    }

    public void SetDestination(Vector3 newDestination)
    {
        Destination = newDestination;
    }
    public void SetMoveDir (Vector3 dir)
    {
        moveDir = dir;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        if (Vector3.Dot((Destination-transform.position).normalized,moveDir) < 0 )
        {
            Destroy(gameObject);
        }
    }

    internal void SetMoveSpeed(float envMoveSpeed)
    {
        moveSpeed = envMoveSpeed;
    }
}
