using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOpe : MonoBehaviour
{
    public enum Item
    {
        Time15 = 0,
        Time10,
        Time5_SpinDown_Small,
        SpinDown_Middle,
        SpinDown_Large
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
