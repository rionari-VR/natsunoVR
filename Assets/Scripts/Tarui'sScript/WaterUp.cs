using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterUp : MonoBehaviour
{
    [SerializeField,Header("水の流れ")]
    Vector3 WavePower = Vector3.zero;

    private void Awake()
    {
        WavePower *= 0.01f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ball")
        {
            //other.transform.position += WavePower;
            other.GetComponent<Rigidbody>().velocity += WavePower * this.transform.lossyScale.x;
        }
    }
}
