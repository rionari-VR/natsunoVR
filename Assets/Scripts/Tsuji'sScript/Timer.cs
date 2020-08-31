using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//状態フラグ
public enum Status{
    normal,
    plus10s,
    plus15s,
    slowdown_plus5s
}

public class Timer : MonoBehaviour
{
    public Text timerText;
 
    public float timeMax; //タイマーの最大値

	public float timeNow;//現在の秒数

    public bool isFeverTime;    //フィーバータイムのフラグ

    //int nowStatus;
    
    int seconds;    //実際に表示する数字

    // Start is called before the first frame update
    void Start()
    {
       isFeverTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //1/2 Countdown time
        if(Input.GetKey(KeyCode.Return)){
            Time.timeScale = 0.5f;
        }
        //Normal Countdown
        if(Input.GetKeyUp(KeyCode.Return)){
            Time.timeScale = 1;
        }
        if(true)
            timeNow += 10.0f; //+10s
        }
        */
        if(Input.GetKeyUp(KeyCode.Space)){
            Time.timeScale = 1;
        }

        timeNow -= Time.deltaTime;
        switch (Status)
        {
            case Status.normal:
                break;
            case Status.plus10s:
                timeNow += 10.0f; //+10s
                break;
            case Status.plus15s:
                timeNow += 15.0f; //+15s
                break;
            case Status.slowdown_plus5s:
                timeNow += 5.0f; //+5s
                //回転をゆっくりする
                break;
            default:
                break;
        }

        seconds = (int)timeNow;
		timerText.text= seconds.ToString();

        
    }

    public void addTimer()
    {
        //timeNow += 10;
        //timerText.text= seconds.ToString();
    }


}
