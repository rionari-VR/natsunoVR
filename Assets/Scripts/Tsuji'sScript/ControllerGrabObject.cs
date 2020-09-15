using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class ControllerGrabObject : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean grabAction;
    public SteamVR_Action_Boolean triggerAction;
    private GameObject collidingObject; // 1
    private GameObject objectInHand; // 2
    private GunController gunController;

    void Start()
    {
        gunController = GameObject.Find("Gun").GetComponent<GunController>();
        if (!gunController)
        {
            Debug.Log("null gunController. script name is 'ControllerGrabObject'");
        }
    }
    // Update is called once per frame
    void Update()
    {
        // 1
        if (grabAction.GetLastStateDown(handType))
        {
            if (collidingObject)
            {
                //つかむ処理(銃のみ)
                if (collidingObject.tag == "Gun" && objectInHand == null)   GrabObject();
                else if (objectInHand)  gunController.SetShootFlag(true);
            }
            else
            {
                Debug.Log("銃ないよ");
            }
        }

        if (grabAction.GetLastStateUp(handType))
        {
            if(objectInHand) gunController.SetShootFlag(false);
        }
        
        if (triggerAction.GetStateDown(handType))
        {
            if (objectInHand) gunController.MagReload();
        }

        // 2
        //if (grabAction.GetLastStateUp(handType))
        //{
        //    //離す処理
        //    if (objectInHand)
        //    {
        //        ReleaseObject();
        //    }
        //}

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

    //掴む処理
    private void GrabObject()
    {
        //一つ掴んだら増えないようにすること
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
}
