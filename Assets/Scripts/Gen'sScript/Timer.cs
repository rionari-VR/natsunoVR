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

   //public float timeNow;//現在の秒数

   public bool isFeverTime;    //フィーバータイムのフラグ

   public Status nowStatus;//switch用フラグ

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
	   //for debugging (stop timer)
       if(Input.GetKeyUp(KeyCode.Space)){
           Time.timeScale = 1;
       }

       timeMax -= Time.deltaTime;	//秒数の減算処理（）
       switch (nowStatus)
       {
           case Status.normal:
               break;
           case Status.plus10s:
               timeMax += 10.0f; //+10s
               break;
           case Status.plus15s:
               timeMax += 15.0f; //+15s
               break;
           case Status.slowdown_plus5s:
               timeMax += 5.0f; //+5s
               //回転をゆっくりする
               break;
           default:
               break;
       }

       seconds = (int)timeMax;
	   timerText.text= seconds.ToString();//

        
   }
//for debugging
   public void addTimer()
   {
    //    timeNow += 10;
    //    timerText.text= seconds.ToString();
   }


}
