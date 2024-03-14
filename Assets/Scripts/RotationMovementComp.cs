using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMovementComp : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 15f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
