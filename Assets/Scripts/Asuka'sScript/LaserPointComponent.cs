using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointComponent : MonoBehaviour
{

    [SerializeField]
    private GameObject LeftPointerObj;

    [SerializeField]
    private GameObject RightPointerObj;

    [SerializeField]
    private Transform RightHandAnchor;

    [SerializeField]
    private Transform LeftHandAnchor;

    [SerializeField]
    private Transform EyeAnchor;

    [SerializeField]
    private float MaxDistance = 100.0f;

    [SerializeField]
    private LineRenderer LaserPointLeftRenderer;

    [SerializeField]
    private LineRenderer LaserPointRightRenderer;

    [SerializeField]
    private HitButton SceneManager;

    bool HitUI;

    // Start is called before the first frame update
    void Start()
    {
        HitUI = false;
        RightPointerObj.SetActive(false);
        LeftPointerObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HitUI = false;

        for (int i = 0; i < 2; i++)
        {
            int layerMask = 5;
            layerMask = ~layerMask;

            Ray pointerRay = GetLine(i);
            RaycastHit hitInfo;
            Physics.Raycast(pointerRay, out hitInfo, MaxDistance, layerMask);
            if (hitInfo.collider != null && hitInfo.collider.tag == "UI") 
            {
                Debug.Log("A");
                HitUI = true;
                LazerDorw(i, pointerRay, hitInfo);
            }
            else
            {
                LazerOff(i);
            }
        }

        if (HitUI)
        {

        }
    }

    void LazerDorw(int i,Ray pointerRay,RaycastHit hitInfo)
    {
        switch(i)
        {
            case 0:
                LaserPointRightRenderer.SetPosition(0, pointerRay.origin);
                LaserPointRightRenderer.SetPosition(1, hitInfo.point);
                RightPointerObj.SetActive(true);
                RightPointerObj.transform.position = hitInfo.point;
                hitInfo.collider.gameObject.GetComponent<HitButton>().IsHitRightTrigger(true);
                break;
            case 1:
                LaserPointLeftRenderer.SetPosition(0, pointerRay.origin);
                LaserPointLeftRenderer.SetPosition(1, hitInfo.point);
                LeftPointerObj.SetActive(true);
                LeftPointerObj.transform.position = hitInfo.point;
                hitInfo.collider.gameObject.GetComponent<HitButton>().IsHitLeftTrigger(true);
                break;
        }
    }

    void LazerOff(int i)
    {
        switch (i)
        {
            case 0:
                LaserPointRightRenderer.SetPosition(0, Vector3.zero);
                LaserPointRightRenderer.SetPosition(1, Vector3.zero);
                RightPointerObj.SetActive(false);
                break;
            case 1:
                LaserPointLeftRenderer.SetPosition(0, Vector3.zero);
                LaserPointLeftRenderer.SetPosition(1, Vector3.zero);
                LeftPointerObj.SetActive(false);
                break;
        }
    }
    Ray GetLine(int i)
    {
        Ray point = new Ray(Vector3.zero, Vector3.forward);

        switch (i)
        {
            case 0:
                point = new Ray(RightHandAnchor.position, RightHandAnchor.forward);
                break;
            case 1:
                point = new Ray(LeftHandAnchor.position, LeftHandAnchor.forward);
                break;
        }

        return point;
    }

}
