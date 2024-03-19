using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Pickup
{
    [SerializeField] float pushSpeed = 20f;
    [SerializeField] Vector3 Torque = new Vector3(2f, 2f, 2f);
    [SerializeField] float destroyDelay = 2f;
    protected override void PickedUpBy(GameObject picker)
    {
        GetMovementComponent().enabled = false;
        GetComponent<Collider>().enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce((transform.position - picker.transform.position).normalized * pushSpeed,ForceMode.VelocityChange);
        rb.AddTorque(Torque,ForceMode.VelocityChange);

        Invoke("DestroySelf", destroyDelay);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
