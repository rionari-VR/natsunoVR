using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiDurability : MonoBehaviour
{
    [SerializeField] int durability;  //耐久度

    GameObject paper;
    PoiPaperColliderComponent pPCC;
    
    // Start is called before the first frame update
    void Start()
    {
        paper = transform.GetChild(0).gameObject;
        pPCC = GetComponentInChildren<PoiPaperColliderComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        //当たってたらHP減少
        if (pPCC.GetIsHitFlag())
        {
            durability--;
            pPCC.SetIsHitFlag(false);
        }

        //耐久度0になったら紙が消える
        if(durability <= 0)
        {
            Destroy(paper);
        }
    }
}
