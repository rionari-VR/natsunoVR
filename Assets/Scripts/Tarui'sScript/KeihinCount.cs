using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeihinCount : MonoBehaviour
{
    [SerializeField, Header("足元の景品の最大数")]
    int KeihinNum = 10;

    bool getFlg = false;

    // 合計スコア
    int Score = 0;
    public int ScoreCounter
    {
        get
        {
            return Score;
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
        List<GameObject> ObjList = new List<GameObject>() { };
        ObjList.AddRange((GameObject[])Resources.LoadAll("Keihin"));
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
                    Instantiate(keiObj, this.transform);

                    // スコア加算
                    Score += keiObj.GetComponent<S_Score>().Score;

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
