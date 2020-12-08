using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusOpe : MonoBehaviour
{
    // 入っているか(個数重複防止)
    bool isInRing = false;

    Wa_Haitta CountObject;

    // Start is called before the first frame update
    void Start()
    {
        CountObject = GameObject.Find("Counter").GetComponent<Wa_Haitta>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ring_Bar" && 
            !isInRing)
        {
            isInRing = true;

            CountObject.RingCount(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ring_Bar" &&
            isInRing)
        {
            isInRing = false;

            CountObject.RingCount(false);
        }
    }
}
