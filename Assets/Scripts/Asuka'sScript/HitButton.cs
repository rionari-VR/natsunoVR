using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitButton : MonoBehaviour
{
    public Slider HitTime;
    public SceneLoad sceneload;
    public string NextSceneName;
    bool HitTrigger;
    bool RightTrigger;
    bool LeftTrigger;
    // Start is called before the first frame update
    void Start()
    {
        sceneload = GameObject.Find("SceneManager").GetComponent<SceneLoad>();
        HitTime = GetComponent<Slider>();
        HitTime.value = 0;
        HitTrigger = false;
    }
    void FixedUpdate()
    {
        HitTrigger = false;
    }

    //Update is called once per frame
    void Update()
    {
        if (!HitTrigger)
        {
            HitTime.value -= 0.1f;
            LeftTrigger = false;
            RightTrigger = false;

            if (HitTime.value < 0)
            {
                HitTime.value = 0;
            }
        }
        else
        {
            HitTime.value += 0.1f;

            if (HitTime.value >= 1.0f)
            {
                if (LeftTrigger)
                {
                    sceneload.LoadLeftTrigger(NextSceneName);
                }
                else if (RightTrigger)
                {
                    sceneload.LoadRightTrigger(NextSceneName);
                }
            }
        }
    }
    public void FinishVal()
    {
        Debug.Log("Finish");
    }

    public void IsHitLeftTrigger(bool trigger)
    {
        LeftTrigger = trigger;
    }
    public void IsHitRightTrigger(bool trigger)
    {
        RightTrigger = trigger;
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log("接触");

        if (other.tag == "Laser")
        {
            HitTrigger = true;

            HitTime.value += 0.1f;
        }
    }
    //void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("離れた");
    //    HitTrigger = false;


    //}
}