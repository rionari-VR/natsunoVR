using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerGrabObject : MonoBehaviour
{
    [SerializeField] private SteamVR_Input_Sources  handType;           //オブジェクトの属性(左手or右手)
    [SerializeField] private SteamVR_Behaviour_Pose controllerPose;     
    [SerializeField] private SteamVR_Action_Boolean grabAction;
    [SerializeField] private SteamVR_Action_Boolean triggerAction;

    [SerializeField] private GameObject collidingObject;   // 1
    [SerializeField] private GameObject reverseHandObject; 
    [SerializeField] private GameObject handModel;
    [SerializeField] private AnimationClip handAnimClip;

    private GameObject    objectInHand; // 2
    private GameObject[]  gunModels;
    private Animator      handAnimator;
    private GunController gunController;
  //  private Collider      handCollider;
    private Collider reverseHandCollider;

    private float animationStartTime;
    private bool isAnimationFlag;
    private string tagGun;
    private string tagFood;
    private string tagPoi;
    private string tagRing;

    void Start()
    {
        gunModels = GameObject.FindGameObjectsWithTag("Gun");
        tagGun = "Gun";
        tagFood = "Food";
        tagPoi = "Poi";
        tagRing = "Ring";

        handAnimator = handModel.GetComponent<Animator>();
        handAnimator.enabled = false;
        animationStartTime = 0.5f;
        isAnimationFlag = false;

        handAnimator.enabled = true;
        isAnimationFlag = true;
        handAnimator.Play(handAnimClip.name, 0, 0);

        handAnimator.speed = 3;

        //当たり判定をget
       // handCollider = gameObject.GetComponent<Collider>();
        reverseHandCollider = reverseHandObject.GetComponent<Collider>();
    }
    // Update is called once per frame
    void Update()
    {
        //handanimation
        HandAnimationManagement(objectInHand, grabAction.GetLastStateUp(handType), grabAction.GetLastStateDown(handType));
        if (objectInHand)
        {
            //handCollider.enabled = false;
            reverseHandCollider.enabled = false;
        }

        // 1
        if (grabAction.GetLastStateDown(handType))
        {
            if (collidingObject)
            {
                //つかむ処理 :食べ物 & poi & ring
                if ((collidingObject.tag == tagFood 
                    || collidingObject.tag == tagRing 
                    || collidingObject.tag == tagPoi)
                    && objectInHand == null)
                {
                    GrabObject();
                }
                    
                //つかむ処理 : 銃
                else if (collidingObject.tag == tagGun && objectInHand == null)
                {
                    GrabGunObject();
                }
                else if (objectInHand.tag == tagGun && objectInHand)
                {
                    gunController.SetShootFlag(true);
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
                if(objectInHand.tag == tagFood || objectInHand.tag == tagRing)
                ReleaseObject();
            }
        }
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

        //掴んだ瞬間に、handのupをGunPrefabのUpに代入(銃の時のみ)
        objectInHand.transform.up = gameObject.transform.up;

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

    //掴む処理
    private void GrabObject()
    {
        // 1
        objectInHand = collidingObject;
        collidingObject = null;
        // 2　連結処理
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        //掴んだ瞬間にHandのupをobjectのLeftにいれる（upの逆ベクトルをRightに入れる）
        objectInHand.transform.right = -transform.up;

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

        if (objectInHand.tag == tagRing)
        {
            //TorusOpeをONにする
            var torus = objectInHand.GetComponentInChildren<TorusOpe>();
            torus.enabled = true;
        }
        // 4
        objectInHand = null;
    }

    //アニメーション管理関数
    private void HandAnimationManagement(GameObject grabObj, bool push,bool release)
    {
        if (grabObj == null)
        {
            //再生がおわれば再度再生可能にする
            if (handAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                handAnimator.enabled = false;
            }

            if (handAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= animationStartTime
                && isAnimationFlag)
            {
                animationStartTime = handAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                handAnimator.enabled = false;
                isAnimationFlag = false;
            }

            //離すアニメーション
            if (push)
            {
                if (handAnimator.enabled) return;

                handAnimator.enabled = true;
                isAnimationFlag = true;
                handAnimator.Play(handAnimClip.name, 0, 0);
            }
            //握るアニメーション
            if (release)
            {
                if (handAnimator.enabled) return;

                handAnimator.enabled = true;
                handAnimator.Play(handAnimClip.name, 0, animationStartTime);
            }
        }
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
