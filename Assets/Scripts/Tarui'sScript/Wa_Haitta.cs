using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wa_Haitta : MonoBehaviour
{
    enum TextSTS
    {
        S=0,
        M,
        L
    }

    enum Result
    {
        FadeOut = 0,
        FadeIn,

        end
    }

    Result sts = Result.FadeOut;

    float FadeTime = 0.0f;
    float FadeOutTime = 1.0f, FadeInTime = 1.0f;

    bool isEnd = false;

    [SerializeField]
    int RingInCount_S = 0, RingInCount_M = 0, RingInCount_L = 0;

    [SerializeField, Header("表示変更用  ゲーム中")]
    Text TextS;
    [SerializeField]
    Text TextM, TextL;

    [SerializeField]
    GameObject Text_Now;

    [SerializeField,Header("表示変更用  リザルト")]
    Text TextS_re;
    [SerializeField]
    Text TextM_re, TextL_re, TextTotal;

    [SerializeField]
    GameObject Text_Result;

    [SerializeField, Header("各輪っかの点数")]
    int ScoreS;
    [SerializeField]
    int ScoreM, ScoreL;

    [SerializeField]
    int MyScore = 0;
    
    public int Score
    {
        get
        {
            return MyScore;
        }
    }

    private void Start()
    {
        Text_Now.SetActive(true);
        Text_Result.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (isEnd)
        {
            switch (sts)
            {
                case Result.FadeOut:
                    {
                        if (FadeTime < FadeOutTime)
                        {
                            FadeTime += Time.deltaTime;

                            Color color = Color.black;
                            color.a = 1.0f - FadeTime / FadeOutTime;

                            TextS.color = TextM.color = TextL.color = color;
                        }
                        else
                        {
                            FadeTime = 0.0f;
                            Text_Now.SetActive(false);
                            Text_Result.SetActive(true);

                            TextChange(TextSTS.L, true);
                            TextChange(TextSTS.M, true);
                            TextChange(TextSTS.S, true);

                            TextTotal.text = "Total\t" + MyScore + "p";

                            sts = Result.FadeIn;
                        }
                    }
                    break;
                case Result.FadeIn:
                    {
                        if (FadeTime < FadeInTime)
                        {
                            FadeTime += Time.deltaTime;

                            Color color = Color.black;
                            color.a = FadeTime / FadeInTime;

                            TextS_re.color = TextM_re.color = TextL_re.color = TextTotal.color = color;
                        }
                        else
                        {
                            isEnd = false;
                        }
                    }
                    break;
            }
        }
    }

    public void GameEnd()
    {
        isEnd = true;
    }

    public void RingCount(bool Flg, RingScale ring)
    {
        if (Flg)
        {
            switch (ring)
            {
                case RingScale.大:
                    RingInCount_L += 1;
                    TextChange(TextSTS.L, false);
                    MyScore += ScoreL;
                    break;
                case RingScale.中:
                    RingInCount_M += 1;
                    TextChange(TextSTS.M, false);
                    MyScore += ScoreM;
                    break;
                case RingScale.小:
                    RingInCount_S += 1;
                    TextChange(TextSTS.S, false);
                    MyScore += ScoreS;
                    break;
            }
        }
        else
        {
            switch (ring)
            {
                case RingScale.大:
                    RingInCount_L -= 1;
                    TextChange(TextSTS.L, false);
                    MyScore -= ScoreL;
                    break;
                case RingScale.中:
                    RingInCount_M -= 1;
                    TextChange(TextSTS.M, false);
                    MyScore -= ScoreM;
                    break;
                case RingScale.小:
                    RingInCount_S -= 1;
                    TextChange(TextSTS.S, false);
                    MyScore -= ScoreS;
                    break;
            }
        }
    }

    void TextChange(TextSTS sts, bool isResult)
    {
        if (isResult)
        {
            switch (sts)
            {
                case TextSTS.S:
                    TextS_re.text =
                        "×" + RingInCount_S.ToString() + "×" + ScoreS.ToString() +
                        "p=" + (RingInCount_S * ScoreS).ToString() + "p";
                    break;
                case TextSTS.M:
                    TextM_re.text =
                        "×" + RingInCount_M.ToString() + "×" + ScoreM.ToString() +
                        "p=" + (RingInCount_M * ScoreM).ToString() + "p";
                    break;
                case TextSTS.L:
                    TextL_re.text =
                        "×" + RingInCount_L.ToString() + "×" + ScoreL.ToString() +
                        "p=" + (RingInCount_L * ScoreL).ToString() + "p";
                    break;
            }
        }
        else
        {
            switch (sts)
            {
                case TextSTS.S:
                    TextS.text =
                        "　　×　　" + RingInCount_S.ToString();
                    break;
                case TextSTS.M:
                    TextM.text =
                        "　　×　　" + RingInCount_M.ToString();
                    break;
                case TextSTS.L:
                    TextL.text =
                        "　　×　　" + RingInCount_L.ToString();
                    break;
            }
        }
    }
}
