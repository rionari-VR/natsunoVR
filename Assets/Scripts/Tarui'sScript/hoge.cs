using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoge : MonoBehaviour
{
    [SerializeField]
    GameObject game,game2,game3;

    // Start is called before the first frame update
    void Start()
    {

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
    }
}
