using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    GameObject parent;

    ItemOpe item;

    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent.gameObject;

        item = parent.GetComponent<ItemOpe>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
