using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wa_Haitta : MonoBehaviour
{
    [SerializeField]
    int RingInCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void RingCount(bool Flg)
    {
        if(Flg)
        {
            RingInCount += 1;
        }
        else
        {
            RingInCount -= 1;
        }
    }
}
