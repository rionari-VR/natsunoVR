using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterUp : MonoBehaviour
{
    [SerializeField,Header("水の浮力")]
    Vector3 UpPower = Vector3.zero;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            rb.velocity += UpPower;
            
        }
    }
}
