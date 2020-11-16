using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class SceneLoad : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean triggerAction;
    public bool SceneTrigger;
    // Start is called before the first frame update
    void Start()
    {
        SceneTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadTrigger(string name)
    {
        Debug.Log("シーン遷移準備完了");
        if (SceneTrigger||triggerAction.GetLastStateDown(handType))
        {
            LoadScene(name);
        }
    }

}
