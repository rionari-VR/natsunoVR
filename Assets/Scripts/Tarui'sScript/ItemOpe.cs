using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOpe : MonoBehaviour
{
    public enum Item
    {
        Time15 = 0,
        Time10,
        Time5_SpinDown_Small,
        SpinDown_Middle,
        SpinDown_Large,

        end,
    };

    Item item = Item.end;

    [System.NonSerialized]
    public bool ItemFlg = false;

    List<GameObject> SpinObject = new List<GameObject>();

    GameObject Timer = null;

    private void Awake()
    {
        SpinObject.AddRange(GameObject.FindGameObjectsWithTag("SpinObj"));

        //Timer = GameObject.FindGameObjectWithTag("");
    }

    // Start is called before the first frame update
    void Start()
    {
        item = (Item)Random.Range(0, 4);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ItemFlg)
        {
            switch(item)
            {
                case Item.Time15:
                    {

                    }
                    break;

                case Item.Time10:
                    {

                    }
                    break;

                case Item.Time5_SpinDown_Small:
                    {


                        SpinChange(SpinField.SpinType.SmallDown);
                    }
                    break;

                case Item.SpinDown_Middle:
                    {
                        SpinChange(SpinField.SpinType.MiddleDown);
                    }
                    break;

                case Item.SpinDown_Large:
                    {
                        SpinChange(SpinField.SpinType.LargeDown);
                    }
                    break;

                default:
                    break;
            }

            Debug.Log(item);

            ItemFlg = false;

            item = (Item)Random.Range(0, 4);
        }
    }

    void SpinChange(SpinField.SpinType spinType)
    {
        for (int i = 0; i < SpinObject.Count; i++)
        {
            SpinField spin = SpinObject[i].GetComponent<SpinField>();

            spin.spinType = spinType;
            spin.TypeChange = true;
        }
    }
}
