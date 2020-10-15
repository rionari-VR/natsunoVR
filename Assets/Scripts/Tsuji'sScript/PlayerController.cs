using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Vector2 trackPad;

    Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        trackPad = SteamVR_Actions.default_TrackPad;
    }

    // Update is called once per frame
    void Update()
    {
        pos = trackPad.GetLastAxis(handType);
        transform.localPosition = new Vector3(pos.x, 0.0f, pos.y);
    }
}
