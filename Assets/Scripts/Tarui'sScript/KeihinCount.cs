using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeihinCount : MonoBehaviour
{
    [SerializeField,Header("足元の景品の最大数")]
    int KeihinNum = 10;

    public static List<Object> Keihin = new List<Object>() { };

    bool getFlg = false;

    GameObject getObj;
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
        int objNum;     // プレハブの通し番号
        public int ObjNum
        {
            get
            {
                return objNum;
            }
            set
            {
                objNum = value;
            }
        }
        int count;      // 落とした個数
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
            }
        }

        public Keijou(int num)
        {
            this.objNum = num;
            this.count = 1;
        }

        public Keijou() { }
    }

    public static List<Keijou> keijou = new List<Keijou>() { };

    // Start is called before the first frame update
    void Start()
    {
        Keihin.AddRange(Resources.LoadAll("Keihin"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(getFlg)
        {
            getFlg = !getFlg;

            // どの景品がゲットできたのか
            for (int i=0;i<Keihin.Count;i++)
            {
                // 景品の照合
                if(getObj == Keihin[i])
                {
                    bool Flg = false;

                    // 落ちた景品の個数をカウント
                    for(int j=0;j<keijou.Count;j++)
                    {
                        if(keijou[j].ObjNum == i)
                        {
                            keijou[j].Count += 1;

                            Flg = true;
                            break;
                        }
                    }

                    // もし新しい種類の景品ならそれのデータを追加

                    if(!Flg)
                    {
                        keijou.Add(new Keijou(i));
                    }

                    // ゲットした景品を出現
                    Instantiate(Keihin[i], this.transform);

                    break;
                }
            }
            
        }

        if(this.transform.childCount > KeihinNum)
        {
            Destroy(this.transform.GetChild(0).gameObject);
        }
    }
}
