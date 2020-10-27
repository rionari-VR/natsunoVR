using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerGrabObject : MonoBehaviour
{
    [SerializeField] private SteamVR_Input_Sources  handType;
    [SerializeField] private SteamVR_Behaviour_Pose controllerPose;
    [SerializeField] private SteamVR_Action_Boolean grabAction;
    [SerializeField] private SteamVR_Action_Boolean triggerAction;
    [SerializeField] private GameObject collidingObject; // 1

    private GameObject    objectInHand; // 2
    private GunController gunController;
    private GameObject[]  gunModels;

    private string tagGun;
    private string tagFood;

    void Start()
    {
        gunModels = GameObject.FindGameObjectsWithTag("Gun");
        tagGun = "Gun";
        tagFood = "Food";
    }
    // Update is called once per frame
    void Update()
    {
        // 1
        if (grabAction.GetLastStateDown(handType))
        {
            if (collidingObject)
            {
                //つかむ処理 : 銃
                if (collidingObject.tag == tagGun && objectInHand == null)
                {
                    GrabGunObject();
                }
                else if (objectInHand)
                {
                    if(objectInHand.tag == tagGun)
                      gunController.SetShootFlag(true);
                }
                //食べ物
                if(collidingObject.tag == tagFood && objectInHand == null)
                {
                    GrabFoodObject();
                }
            }
        }

        if (grabAction.GetLastStateUp(handType))
        {
            if (objectInHand)
            {
                if (objectInHand.tag == tagGun)
                   gunController.SetShootFlag(false);
            }
        }
        
        if (triggerAction.GetStateDown(handType))
        {
            if (objectInHand)
            {
                if (objectInHand.tag == tagGun)
                    gunController.MagReload();
            }
        }

        // 2
        if (grabAction.GetLastStateUp(handType))
        {
            //離す処理
            if (objectInHand)
            {
                if(objectInHand.tag == tagFood)
                ReleaseObject();
            }
        }

        Debug.Log(objectInHand);

    }

    //コントローラーオブジェクトに当たっているオブジェクトは掴める
    private void SetCollidingObject(Collider col)
    {
        // 1
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
           // collidingObject = col.gameObject;
            return;
        }
        // 2
        collidingObject = col.gameObject;
    }

    //当たり判定関連
    // 1
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // 3
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    //掴む処理(銃)
    private void GrabGunObject()
    {
        //一つ掴んだら増えないようにすること
        // 1
        objectInHand = collidingObject;
        collidingObject = null;
        // 2　連結処理
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        
        //gunControllerScriptを取得
        gunController = objectInHand.GetComponent<GunController>();
        if (!gunController)
        {
            Debug.Log("null gunController. script name is 'ControllerGrabObject'");
        }

        //掴めなかった他GunObjのコンポーネントをoffに。
        for(int i = 0; i < gunModels.Length; i++)
        {
            if(gunModels[i].gameObject.name != objectInHand.name)
            {
                gunModels[i].SetActive(false);
            }
        }
    }

    //掴む処理(食べ物)
    private void GrabFoodObject()
    {
        // 1
        objectInHand = collidingObject;
        collidingObject = null;
        // 2　連結処理
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // 3
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    //離す処理
    private void ReleaseObject()
    {
        // 1
        if (GetComponent<FixedJoint>())
        {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3
            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
        }
        // 4
        objectInHand = null;
    }

    //Getter
    public GameObject GetInHandObject()
    {
        if (!objectInHand)
        {
            return null;
        }
        return objectInHand;
    }
}
