using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiPaperColliderComponent : MonoBehaviour
{
    bool isHit;

    private void Start()
    {
        isHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Poi")
            isHit = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Poi")
            isHit = true;
    }
    //Getter
    public bool GetIsHitFlag()
    {
        return isHit;
    }

    //Setter
    public void SetIsHitFlag(bool flag)
    {
        isHit = flag;
    }
}
