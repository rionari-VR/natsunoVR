using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGame : MonoBehaviour
{
    Animator ani;
    public TimerDraw time;
    bool panel;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        panel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (time != null)
        {
            if (time.TimerEnd == true && panel == false)
            {
                panel = true;
                ani.SetBool("HitPlayer", true);

            }
        }
    }
}
