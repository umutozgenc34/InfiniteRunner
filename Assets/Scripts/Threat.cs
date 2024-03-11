using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threat : MonoBehaviour
{
    [SerializeField] float _SpawnInterval = 2f;
    [SerializeField] MovementComp movementComp;
   
    public float SpawnInterval
    {
        get { return _SpawnInterval; }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public MovementComp GetMovementComponent()
    {
        return movementComp;

    }
}
