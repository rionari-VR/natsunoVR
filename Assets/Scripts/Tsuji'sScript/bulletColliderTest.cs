using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletColliderTest : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private void OnCollisionEnter(Collision collision)
    {
        obj = collision.gameObject;
        Debug.Log("当たったよ");
    }
}
