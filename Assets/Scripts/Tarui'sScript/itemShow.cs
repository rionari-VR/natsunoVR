using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemShow : MonoBehaviour
{
    ItemOpe item;

    Image image;

    [SerializeField]
    Sprite sprite1;
    [SerializeField]
    Sprite sprite2;
    [SerializeField]
    Sprite sprite3;
    [SerializeField]
    Sprite sprite4;
    [SerializeField]
    Sprite sprite5;

    // Start is called before the first frame update
    void Start()
    {
        item = this.transform.parent.GetComponent<ItemOpe>();

        image = this.GetComponent<Image>();

        switch(item.itemGet)
        {
            case ItemOpe.Item.Time15:
                {
                    image.sprite = sprite1;
                }
                break;

            case ItemOpe.Item.Time10:
                {
                    image.sprite = sprite2;
                }
                break;

            case ItemOpe.Item.Time5_SpinDown_Small:
                {
                    image.sprite = sprite3;
                }
                break;

            case ItemOpe.Item.SpinDown_Middle:
                {
                    image.sprite = sprite4;
                }
                break;

            case ItemOpe.Item.SpinDown_Large:
                {
                    image.sprite = sprite5;
                }
                break;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
