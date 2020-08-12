using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed;

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            MakeBullet();
        }
    }

    void MakeBullet()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody bulletRb = bulletObj.GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * bulletSpeed);
        Destroy(bulletObj, 3.0f);
    }
}
