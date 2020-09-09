using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private enum GunType
    {
        HandGun,
        MachineGun,
        Magnum
    }

    [SerializeField] private GunType gunType = GunType.HandGun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject magnumBulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int magMax = 30;

    private int  interval;
    [SerializeField] private int  mag;
    private bool isShoot;

    void Start()
    {
        mag = magMax;
        interval = 0;
        isShoot = false;
    }
    // Update is called once per frame
    void Update()
    {
        //VRでtestするときはコメントアウトしてください
        switch (gunType)
        {
            case GunType.HandGun:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    HandGunBullet();
                }
                else if (Input.GetKey(KeyCode.X))
                {
                    mag = magMax;
                }
                break;
            case GunType.MachineGun:
                if (Input.GetKey(KeyCode.Z))
                {
                    MachineGunBullet();
                }
                else if (Input.GetKey(KeyCode.X))
                {
                    mag = magMax;
                }
                break;
            case GunType.Magnum:
                if (Input.GetKey(KeyCode.Z))
                {
                    MagnumBullet();
                }
                else if (Input.GetKey(KeyCode.X))
                {
                    mag = magMax;
                }
                break;
        }
        //VRでtestするときはオンにしてください
        //switch (gunType)
        //{
        //    case GunType.HandGun:
        //         if (isShoot)
        //        {
        //            isShoot = false;
        //            HandGunBullet();
        //        }
        //        break;
        //    case GunType.MachineGun:
        //        if (isShoot)
        //        {
        //            MachineGunBullet();
        //        }
        //        break;
        //    case GunType.Magnum:
        //        if (isShoot)
        //        {
        //            MagnumBullet();
        //        }
        //        break;
        //}
    }

    //ハンドガン
    void HandGunBullet()
    {
        if(mag > 0)
        {
            mag--;
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody bulletRb = bulletObj.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * bulletSpeed);
            Destroy(bulletObj, 3.0f);
        }
    }

    //マシンガン
    void MachineGunBullet()
    {
        interval++;
        if(interval % 5 == 0 && mag > 0)
        {
            mag--;
            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody bulletRb = bulletObj.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * bulletSpeed);
            Destroy(bulletObj, 3.0f);
            interval = 0;
        }
    }

    //マグナム
    void MagnumBullet()
    {
        interval++;
        if (interval % 100 == 0 && mag > 0)
        {
            mag--;
            GameObject bulletObj = Instantiate(magnumBulletPrefab, transform.position, Quaternion.identity);
            Rigidbody bulletRb = bulletObj.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * bulletSpeed);
            Destroy(bulletObj, 3.0f);
            interval = 0;
        }
    }

    public void SetShootFlag()
    {
        isShoot = true;
    }
}
