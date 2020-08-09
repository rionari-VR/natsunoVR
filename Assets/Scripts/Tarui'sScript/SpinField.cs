using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinField : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.eulerAngles += new Vector3(0.0f, 1.0f, 0.0f);
    }
}
