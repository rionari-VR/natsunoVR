using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinField : MonoBehaviour
{
    [SerializeField]
    float SpinSpeed = 1.0f;

    [SerializeField]
    float SpinChangeTime = 10.0f;

    const float speed = 1.0f;

    public enum SpinType
    {
        SmallDown=0,
        MiddleDown,
        LargeDown,

        normal,

        end
    };

    SpinType type = SpinType.normal;
    public SpinType spinType
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

    bool typeChange = false;
    public bool TypeChange
    {
        set
        {
            typeChange = value;
        }
    }

    SpinType oldType;

    bool speedFlg = false;

    float countDown = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        oldType = type;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.eulerAngles += new Vector3(0.0f, SpinSpeed, 0.0f);

        if (oldType != type)
        {
            oldType = type;

            switch (type)
            {
                case SpinType.SmallDown:
                    {
                        SpinSpeed = speed * 0.9f;
                    }
                    break;


                case SpinType.MiddleDown:
                    {
                        SpinSpeed = speed * 0.8f;
                    }
                    break;


                case SpinType.LargeDown:
                    {
                        SpinSpeed = speed * 0.7f;
                    }
                    break;


                case SpinType.normal:
                    {
                        SpinSpeed = speed;
                    }
                    break;

                default:
                    break;
            }
        }

        if(typeChange)
        {
            countDown = 0.0f;
            typeChange = false;
        }

        if(type != SpinType.normal)
        {
            countDown += Time.deltaTime;

            if(countDown >= SpinChangeTime)
            {
                type = SpinType.normal;
            }
        }
    }
}
