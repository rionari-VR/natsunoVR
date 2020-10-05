using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startTre : MonoBehaviour
{
    ItemOpe item;

    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.parent.TryGetComponent<ItemOpe>(out item))
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
