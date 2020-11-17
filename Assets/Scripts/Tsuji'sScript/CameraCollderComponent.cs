using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollderComponent : MonoBehaviour
{
    //食べ物にカメラが当たったかどうか
    private bool isEatFlag;
    private int timeToEat;

    private void Start()
    {
        isEatFlag = false;
        timeToEat = 0;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Food")
    //    {
    //        isEatFlag = true;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            isEatFlag = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            timeToEat++;
        }
    }

    //Getter
    public bool GetEatFlag()
    {
        return isEatFlag;
    }

    public int GetTimeToEat()
    {
        return timeToEat;
    }

    public void SetEatFlag(bool flag)
    {
        isEatFlag = flag;
    }
}
