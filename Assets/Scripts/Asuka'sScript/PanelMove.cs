using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMove : MonoBehaviour
{
    public BoxCollider PanelCol;
    Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"||
            other.name==PanelCol.name)
        {
            ani.SetBool("HitPlayer", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
            ani.SetBool("HitPlayer", false);
    }
}
