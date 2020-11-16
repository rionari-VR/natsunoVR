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
    // Start is called before the first frame update
    void Start()
    {
        sceneload = GameObject.Find("SceneManager").GetComponent<SceneLoad>();
        HitTime = GetComponent<Slider>();
        HitTime.value = 0;
        HitTrigger = false;
    }

    //Update is called once per frame
    void Update()
    {
        if (!HitTrigger)
        {
            HitTime.value -= 0.1f;
        }
        else if (HitTime.value >= 1.0f){
            sceneload.LoadTrigger(NextSceneName);
        }
    }
    public void FinishVal()
    {
        Debug.Log("Finish");
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
    void OnTriggerExit(Collider other)
    {
        Debug.Log("離れた");
        HitTrigger = false;


    }
}