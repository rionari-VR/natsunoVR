using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOpe : MonoBehaviour
{
    [SerializeField]
    GameObject Prefab = null;

    [SerializeField,Header("ターゲットの個数")]
    int Count;

    [SerializeField]
    float R;

    [SerializeField]
    float Y;

    // Start is called before the first frame update
    void Start()
    {
        if (Count <= 0)
        {
            Debug.LogError("ターゲットがいないよ");
        }
        else
        {
            float x, y, z;
            float Rot;
            x = this.transform.position.x;
            z = this.transform.position.z;

            y = this.transform.position.y + Y;

            // 角度計算
            Rot = 360.0f / (float)Count;

            for (int i = 0; i < Count; i++)
            {
                float Theta = Rot * i;
                float ox, oz;

                ox = R * Mathf.Cos(Theta * Mathf.Deg2Rad) + x;
                oz = R * Mathf.Sin(Theta * Mathf.Deg2Rad) + z;

                Instantiate(Prefab, new Vector3(ox, y, oz), Quaternion.Euler(0.0f, -(Theta + 180.0f), 0.0f), this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
