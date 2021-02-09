using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeihinCount : MonoBehaviour
{
    enum Result
    {
        FadeOut = 0,
        FadeIn,

        end
    }

    Result sts = Result.FadeOut;

    float FadeTime = 0.0f;
    float FadeOutTime = 1.0f, FadeInTime = 1.0f;


    [SerializeField, Header("足元の景品の最大数")]
    int KeihinNum = 10;

    bool getFlg = false;

    [SerializeField]
    List<GameObject> ObjList = new List<GameObject>() { };

    [SerializeField, Header("表示変更用  ゲーム中")]
    Text[] Texts;

    [SerializeField, Header("各輪っかの点数")]
    int[] Score;

    bool isEnd = false;


    // 合計スコア
    int ScoreSum = 0;
    public int ScoreCounter
    {
        get
        {
            return ScoreSum;
        }
    }

    GameObject getObj = null;
    public GameObject GetObj

    {
        set
        {
            getObj = value;
            getFlg = true;
        }
    }

    public class Keijou
    {
        GameObject obj;     // プレハブの通し番号
        public GameObject Obj
        {
            get
            {
                return obj;
            }
        }
        int count;      // 落とした個数
        public int Count
        {
            get
            {
                return count;
            }
        }

        public Keijou(GameObject obj)
        {
            this.obj = obj;
            this.count = 0;
        }

        public Keijou() { }

        public void AddCount()
        {
            this.count += 1;
        }
    }

    public static List<Keijou> keijou = new List<Keijou>() { };

    GameObject keiObj=null;
    int i = 0;

    // Start is called before the first frame update
    void Awake()
    {
        for (i = 0; i < ObjList.Count; i++)
        {
            keijou.Add(new Keijou(ObjList[i]));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(getFlg)
        {
            getFlg = !getFlg;

            // どの景品がゲットできたのか
            for (i = 0; i < keijou.Count; i++) 
            {
                // 景品の照合
                if (getObj == keijou[i].Obj)
                {
                    keiObj = keijou[i].Obj;

                    // 落ちた景品の個数をカウント
                    keijou[i].AddCount();

                    // ゲットした景品を出現
                    Instantiate(keiObj, this.transform.GetChild(0));

                    Texts[i].text = "\t×\t" + keijou[i].Count.ToString();

                    // スコア加算
                    ScoreSum += keiObj.GetComponent<S_Score>().Score;

                    break;
                }
            }
            
        }

        if(this.transform.childCount > KeihinNum)
        {
            Destroy(this.transform.GetChild(0).GetChild(0).gameObject);
        }

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

                            for(int i = 0;i<Texts.Length-1;i++)
                            {
                                Texts[i].color = color;
                            }
                        }
                        else
                        {
                            FadeTime = 0.0f;

                            for(int i=0;i<Texts.Length-1;i++)
                            {
                                Texts[i].text = "×" + keijou[i].Count.ToString() + "×" + Score[i].ToString() + "　＝　" +
                                    (keijou[i].Count * Score[i]).ToString();
                            }

                            Texts[Texts.Length-1].text = "Total\t" + ScoreSum + "p";

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

                            for (int i = 0; i < Texts.Length; i++)
                            {
                                Texts[i].color = color;
                            }
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
}
