using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespornOpe : MonoBehaviour
{
    [SerializeField, Header("リスポーン中のアニメ再生時間"), Range(0.1f, 5.0f)]
    float RespornTime = 1.0f;

    Vector3 startPos = Vector3.zero;

    bool RespornFlg = true;

    // リスポーン時のアニメーション用
    float t = 0.0f;
    float startTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        RespornFlg = true;

        t = 0;

        startPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(RespornFlg)
        {
            startTime += Time.deltaTime;
            t = startTime / RespornTime;

            if (t >= 1.0f)
            {
                t = 1.0f;

                RespornFlg = !RespornFlg;
            }

            this.transform.localPosition = Vector3.Lerp(startPos, Vector3.zero, t);
        }
        else
        {
            this.GetComponent<Collider>().enabled = true;
            //GameObject.Destroy(this);
        }
    }
}
