using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOpe : MonoBehaviour
{
    [SerializeField]
    List<GameObject> objList;

    [SerializeField,Header("角度がおかしい奴の修正用")]
    GameObject obj;

    GameObject child;

    [SerializeField,Range(0.1f,10.0f)]
    float spornTime = 1.0f;

    // 
    bool respornFlg  = false;

    float respornTime = 0.0f;

    [SerializeField]
    int listNum = 0;

    // 落とした景品を計上させるやつ
    GameObject countObj = null;

    // Start is called before the first frame update
    void Start()
    {
        //child = this.transform.GetChild(0).gameObject;

        child = Instantiate(objList[listNum], this.transform.position, Quaternion.identity, this.transform);

        listNum++;

        child.transform.localEulerAngles = new Vector3(0.0f, 90.0f);

        countObj = GameObject.FindGameObjectWithTag("Count");
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
                if(objList[listNum] == obj)
                {
                    child = Instantiate(objList[listNum], this.transform.position + new Vector3(0, -1.0f), Quaternion.Euler(-90.0f, 0, 0), this.transform);
                }
                else
                {
                    child = Instantiate(objList[listNum], this.transform.position + new Vector3(0, -1.0f), Quaternion.identity, this.transform);

                }
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

            // 落とした景品を計上させる
            countObj.GetComponent<KeihinCount>().GetObj = objList[listNum - 1];

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
