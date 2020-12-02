using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class GunController : MonoBehaviour
{
    private enum GunType
    {
        HandGun,
        MachineGun,
        Beam
    }

    [SerializeField] private GunType gunType = GunType.HandGun;     //銃のタイプ
    [SerializeField] private GameObject bulletPrefab;               //銃の弾丸
    [SerializeField] private GameObject beamBulletPrefab;           //ビーム銃用の弾丸(いらなさそうなら消せる)
    [SerializeField] private float bulletSpeed;                     //弾速
    [SerializeField] private int magMax = 30;                       //弾の最大値
    [SerializeField] private float scaleChange;                     //サイズを変える値
    [SerializeField] private float scaleMax;                        //サイズの最大値

    [SerializeField] private int mag;                               //現在の総弾数
    private int  interval;                                          //弾を放つ間隔
    private bool isShoot;                                           //銃を撃ったかどうかのフラグ
    private bool isBeamBullet;                                      //ビーム銃管理フラグ
    private GameObject bulletObj;                                   //銃prefab生成用
    private Vector3 beambulletScale;                                //ビーム銃用弾丸のサイズ

    void Start()
    {
        mag = magMax;
        interval = 0;
        isShoot = false;
        isBeamBullet = false;
        beambulletScale = Vector3.zero;
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
            case GunType.Beam:
                if (Input.GetKey(KeyCode.Z))
                {
                    BeamBulletCharge();
                }
                else if (Input.GetKeyUp(KeyCode.Z))
                {
                    BeamBulletFiring();
                }
                else if (Input.GetKey(KeyCode.X))
                {
                    mag = magMax;
                }
                break;
        }
        //VRでtestするときはオンにしてください
        switch (gunType)
        {
            case GunType.HandGun:
                if (isShoot)
                {
                    isShoot = false;
                    HandGunBullet();
                }
                break;
            case GunType.MachineGun:
                if (isShoot)
                {
                    MachineGunBullet();
                }
                break;
            case GunType.Beam:
                if (isShoot)
                {
                    BeamBulletCharge();
                }
                else
                {
                    BeamBulletFiring();
                }
                break;
        }
    }

    //ハンドガン
    void HandGunBullet()
    {
        if(mag > 0)
        {
            mag--;
            GameObject r_bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody bulletRb = r_bulletObj.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * bulletSpeed);
            Destroy(r_bulletObj, 3.0f);
        }
    }

    //マシンガン
    void MachineGunBullet()
    {
        interval++;
        if(interval % 5 == 0 && mag > 0)
        {
            mag--;
            GameObject r_bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody bulletRb = r_bulletObj.GetComponent<Rigidbody>();
            SEManager.Instance.Play(SEPath.HANDGUNFIRING1);
            bulletRb.AddForce(transform.forward * bulletSpeed);
            Destroy(r_bulletObj, 3.0f);
            interval = 0;
        }
    }

    //ビーム
    void BeamBulletCharge()
    {
        //総弾数がゼロでなく
        if (mag > 0)
        {
            Vector3 pos = transform.position;
            if (!isBeamBullet)
            {
                isBeamBullet = true;
                SEManager.Instance.Play(SEPath.BEAMGUNCHARGE1);
                bulletObj = Instantiate(beamBulletPrefab, pos, Quaternion.identity);
            }
            //ポジションの動機
            bulletObj.transform.position = pos;
            //スケール操作
            if (beambulletScale.x <= scaleMax)
            {
                bulletObj.transform.localScale = new Vector3(beambulletScale.x += scaleChange,
                                                             beambulletScale.y += scaleChange,
                                                             beambulletScale.z += scaleChange);
            }
        }
    }

    //発射
    void BeamBulletFiring()
    {     
        if (bulletObj != null)
        {
            if (isBeamBullet)
            {
                isBeamBullet = false;
                SEManager.Instance.Stop(SEPath.BEAMGUNCHARGE1);
                SEManager.Instance.Play(SEPath.BEAMGUN1);
                mag--;
            }
            GameObject r_bulletObj = Instantiate(beamBulletPrefab, bulletObj.transform.position, Quaternion.identity);
            Rigidbody bulletRb = r_bulletObj.GetComponent<Rigidbody>();

            r_bulletObj.transform.localScale = beambulletScale;
            beambulletScale = Vector3.zero;
            bulletRb.AddForce(gameObject.transform.forward * bulletSpeed);
            Destroy(bulletObj);
            Destroy(r_bulletObj, 3.0f);
        }
    }

    //リロード関数
    public void MagReload()
    {
        if(mag <= 0)
        {
            isShoot = false;
            mag = magMax;
        }
    }

    //setter
    public void SetShootFlag(bool flg)
    {
        isShoot = flg;
    }
    
}
