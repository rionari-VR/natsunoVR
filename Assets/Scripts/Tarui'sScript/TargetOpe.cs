using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOpe : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    GameObject child;

    [SerializeField,Range(0.1f,10.0f)]
    float spornTime = 1.0f;

    // 
    bool respornFlg  = false;

    float respornTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        child = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(respornFlg)
        {
            if (respornTime >= spornTime)
            {
                respornFlg = !respornFlg;
                respornTime = 0.0f;

                // リスポーンの処理
                child = Instantiate(obj, this.transform.position + new Vector3(0,-1.0f), Quaternion.identity, this.transform);
                child.AddComponent<RespornOpe>();
                child.GetComponent<Collider>().enabled = false;
            }
            else
            {
                respornTime += Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == child)
        {
            Destroy(child);

            respornFlg = !respornFlg;

            // ここで点数の加算の処理等を行う
        }
    }
}
