using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDraw : MonoBehaviour
{
    [SerializeField]
    private int minute;
    [SerializeField]
    private float seconds;
    private float oldSecond;
    private Text TimerText;
    [SerializeField, Header("制限時間(秒)")]
    public int Limitsecond;

    [SerializeField, Header("trueにすると制限時間リセット")]
    public bool TimerReset;
    [SerializeField, Header("制限時間が経過するとtrue")]
    public bool TimerEnd;
    // Start is called before the first frame update
    void Start()
    {
        minute = Limitsecond / 60;
        seconds = Limitsecond % 60;
        oldSecond = 0.0f;
        TimerText = GetComponent<Text>();
        TimerEnd = false;
        TimerReset = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (minute >= 0 && seconds >= 0 && TimerEnd == false) 
        {
            seconds -= Time.deltaTime;
        }else
        {
            seconds = 0.0f;
            TimerEnd = true;
        }

        if (seconds < 0 && minute > 0) 
        {
            minute--;
            seconds = seconds + 60;
        }

        if ((int)seconds != (int)oldSecond)
        {
            TimerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldSecond = seconds;

        if (TimerReset)
        {
            Reset();
        }
    }

    void Reset()
    {
        minute = Limitsecond / 60;
        seconds = Limitsecond % 60;
        oldSecond = 0.0f;
        TimerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        TimerReset = false;
        TimerEnd = false;
    }
}
