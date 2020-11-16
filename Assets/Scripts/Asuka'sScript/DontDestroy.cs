using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using KanKikuchi.AudioManager;
public class DontDestroy : MonoBehaviour
{
    //static public GameObject instance;
    //private static bool created =false;
    // GameControllerインスタンスの実体
    private static DontDestroy instance = null;

    // GameControllerインスタンスのプロパティーは、実体が存在しないとき（＝初回参照時）実体を探して登録する
    public static DontDestroy Instance => instance
        ?? (instance = GameObject.FindWithTag("DontDestroy").GetComponent<DontDestroy>());

    // Start is called before the first frame update
    void Awake()
    {
        //if (instance == null||instance.name != gameObject.name)
        //if (!created)
        if (this != Instance)
        {
            Destroy(gameObject);

            //instance = gameObject;
            //created = true;
        }
        else
        {
            DontDestroyOnLoad(gameObject);

        }
        //SceneCom = GameObject.Find("SceneManager").GetComponent<SceneComponent>();
        //NowSelectObj = instance.currentSelectedGameObject;

    }
    private void OnDestroy()
    {
        // 破棄時に、登録した実体の解除を行う
        if (this == Instance) instance = null;
    }
    // Update is called once per frame
    void Update()
    {
        //if (instance.currentSelectedGameObject != NowSelectObj)
        //{
        //    Debug.Log("se");
        //    NowSelectObj = instance.currentSelectedGameObject;
        //    SEManager.Instance.Play(SceneCom.EnterClip);
        //}
        //else 
        //{
        //    instance.firstSelectedGameObject = NowSelectObj;
        //}
    }
}
