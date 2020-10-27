using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollderComponent : MonoBehaviour
{
    //食べ物にカメラが当たったかどうか
     private bool isEatFlag;

    private void Start()
    {
        isEatFlag = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Food")
        {
            isEatFlag = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            isEatFlag = true;
        }
    }

    //Getter
    public bool GetEatFlag()
    {
        return isEatFlag;
    }
}
