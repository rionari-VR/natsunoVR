using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOpe : MonoBehaviour
{
    [SerializeField]
    List<GameObject> objList;

    GameObject child;

    [SerializeField,Range(0.1f,10.0f)]
    float spornTime = 1.0f;

    // 
    bool respornFlg  = false;

    float respornTime = 0.0f;

    [SerializeField]
    int listNum = 0;

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
                child = Instantiate(objList[listNum], this.transform.position + new Vector3(0,-1.0f), Quaternion.identity, this.transform);
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
            if(other.tag == "Item")
            {
                this.GetComponent<ItemOpe>().ItemFlg = true;
            }

            Destroy(child);

            respornFlg = !respornFlg;

            listNum++;
            if (objList.Count == listNum)
            {
                listNum = 0;
            }

            // ここで点数の加算の処理等を行う
        }
    }
}
