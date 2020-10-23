using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Vector2 trackPad;
    public SteamVR_Input input;
    public Camera camera;

    [SerializeField]
    private int moveSpeedLimits;
    private Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        trackPad = SteamVR_Actions.default_TrackPad;
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = trackPad.GetLastAxis(handType);
        if(pos.y > 0)
        {
            //プレイヤーの向いている方向に動く
            var temp = camera.transform.forward;
            temp.y = 0.0f;
            transform.localPosition += temp / moveSpeedLimits;
        }
        else if(pos.y < 0){
            //プレイヤーのむいている反対側に動く
            var temp = camera.transform.forward;
            temp.y = 0.0f;
            transform.localPosition -= temp / moveSpeedLimits;
        }
    }
}
