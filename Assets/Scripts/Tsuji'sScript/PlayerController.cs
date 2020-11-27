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
    [SerializeField] private int eatTime;

    [SerializeField] private float jumpPow;
    [SerializeField] private float mass;
    [SerializeField] private Vector3 gravity;

    private ControllerGrabObject leftGrabScript;
    private ControllerGrabObject rightGrabScript;
    private CameraCollderComponent ccc;

    private GameObject leftHand;
    private GameObject rightHand;

    private GameObject foodObj1;
    private GameObject foodObj2;

    private Vector2 pos;
    private Vector3 startPos;

    private float time;
    private float startPosY;

    private bool isButtonDown;

    // Start is called before the first frame update
    void Start()
    {
        leftHand = GameObject.Find("Controller (left)");
        rightHand = GameObject.Find("Controller (right)");

        leftGrabScript = leftHand.GetComponent<ControllerGrabObject>();
        rightGrabScript = rightHand.GetComponent<ControllerGrabObject>();

        ccc = camera.GetComponent<CameraCollderComponent>();

        trackPad = SteamVR_Actions.default_TrackPad;

        isButtonDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        pos = trackPad.GetLastAxis(leftHandType);

        PayerMove();

        foodObj1 = leftGrabScript.GetInHandObject();
        foodObj2 = rightGrabScript.GetInHandObject();

        if (foodObj1)
        {
            if (foodObj1.tag == "Food")
            {
                FoodEat();
            }

        }else if (foodObj2)
        {
            if (foodObj2.tag == "Food")
            {
                FoodEat();
            }
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
            startPos = transform.localPosition;

            if (!isButtonDown)
            {
                isButtonDown = true;
                startPos.y = 0.0f;
            }
        }
        else if (pos.y < 0)
        {
            //プレイヤーのむいている反対側に動く
            var temp = camera.transform.forward;
            temp.y = 0.0f;
            transform.localPosition -= temp / moveSpeedLimits;
            startPos = transform.localPosition;

            if (!isButtonDown)
            {
                isButtonDown = true;
                startPos.y = 0.0f;
            }
        }

        //ちょっとジャンプする
        if (isButtonDown)
        {
            time += Time.deltaTime;
            float force = CalcPositionFromForce(time, mass, startPos, Vector3.up * jumpPow, gravity).y;
            transform.position = new Vector3(transform.position.x, force, transform.position.z);
            if (transform.position.y <= 0.0f)
            {
                isButtonDown = false;
                var temp = transform.position;
                temp.y = 0.0f;

                transform.position = temp;
                time = 0;
            }
          
        }
         
    }

    //食べ物を食べる
    void FoodEat()
    {
        if (ccc.GetEatFlag())
        {
            if(eatTime < ccc.GetTimeToEat())
            {
                if (foodObj1) Destroy(foodObj1);
                if (foodObj2) Destroy(foodObj2);
            }
        }
    }

    //プレイヤーの揺れ？（ジャンプ処理）
    Vector3 CalcPositionFromForce(float time, float mass,Vector3 startPos,Vector3 force,Vector3 gravity)
    {
        Vector3 speed = (force / mass) * Time.fixedDeltaTime;
        Vector3 pos = (speed * time) + (gravity * 0.5f * Mathf.Pow(time,2)) ;

        return startPos + pos; 
    }
}
