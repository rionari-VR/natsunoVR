﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoge : MonoBehaviour
{
    [SerializeField]
    GameObject game,game2,game3;

    KeihinCount end;

    // Start is called before the first frame update
    void Start()
    {
        end = GameObject.Find("KeihinCount_Ver2").GetComponent<KeihinCount>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(game,this.transform.position,Quaternion.identity);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Instantiate(game2, this.transform.position, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Instantiate(game3, this.transform.position, Quaternion.identity);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            end.GameEnd();
        }
    }
}
