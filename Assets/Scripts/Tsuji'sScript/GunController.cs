using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed;
    private bool isShoot;

    void Start()
    {
        isShoot = false;
    }
    // Update is called once per frame
    void Update()
    {
        //VRでテストする時はOFFにしてね
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    MakeBullet();
        //}

        //VRでテストする時はONにしてね
        if (isShoot)
        {
            isShoot = false;
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

    public void SetShootFlag()
    {
        isShoot = true;
    }
}
