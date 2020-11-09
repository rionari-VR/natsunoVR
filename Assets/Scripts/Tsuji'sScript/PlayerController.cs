using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SteamVR_Input_Sources leftHandType;
    [SerializeField] private SteamVR_Input_Sources rightHandType;
    [SerializeField] private SteamVR_Action_Vector2 trackPad;
    [SerializeField] private SteamVR_Input input;
    [SerializeField] private Camera camera;
    [SerializeField] private int moveSpeedLimits;

    private ControllerGrabObject leftGrabScript;
    private ControllerGrabObject rightGrabScript;
    private CameraCollderComponent ccc;

    private GameObject leftHand;
    private GameObject rightHand;

    private GameObject foodObj1;
    private GameObject foodObj2;

    private Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        leftHand = GameObject.Find("Controller (left)");
        rightHand = GameObject.Find("Controller (right)");

        leftGrabScript = leftHand.GetComponent<ControllerGrabObject>();
        rightGrabScript = rightHand.GetComponent<ControllerGrabObject>();

        ccc = camera.GetComponent<CameraCollderComponent>();

        trackPad = SteamVR_Actions.default_TrackPad;
    }

    // Update is called once per frame
    void Update()
    {
        pos = trackPad.GetLastAxis(leftHandType);
        PayerMove();

        foodObj1 = leftGrabScript.GetInHandObject();
        foodObj2 = rightGrabScript.GetInHandObject();

        if(!foodObj1 || !foodObj2)
        {
            FoodEat();
        }

    }

    //プレイヤーの移動処理
    void PayerMove()
    {
        if (pos.y > 0)
        {
            //プレイヤーの向いている方向に動く
            var temp = camera.transform.forward;
            temp.y = 0.0f;
            transform.localPosition += temp / moveSpeedLimits;
        }
        else if (pos.y < 0)
        {
            //プレイヤーのむいている反対側に動く
            var temp = camera.transform.forward;
            temp.y = 0.0f;
            transform.localPosition -= temp / moveSpeedLimits;
        }
    }

    //食べ物を食べる
    void FoodEat()
    {
        if (ccc.GetEatFlag())
        {
            if (foodObj1) Destroy(foodObj1);
            if (foodObj2) Destroy(foodObj2);
        }
    }
}
