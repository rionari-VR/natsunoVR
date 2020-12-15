using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindOpe : MonoBehaviour
{
    [SerializeField]
    float intensity;   // 数値が大きいほど影響が大きい
    [Header("開始時の風の向き")]
    public Vector3 velocity;    // 風の向きと強さ
    new Rigidbody rigidbody;

    [SerializeField, Header("風の最大値")]
    Vector3 windMax;
    [SerializeField, Header("風の最小値")]
    Vector3 windMin;

    [SerializeField,Header("風の開始時の変更時間")]
    float ChangeTimeLimit = 20.0f;
    float ChangeTime = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangeTime += Time.deltaTime;

        if (ChangeTime > ChangeTimeLimit)
        {
            ChangeTime = 0;
            ChangeTimeLimit = Random.Range(2.0f, 10.0f);

            Vector3 wind;
            wind.x = Random.Range(windMin.x, windMax.x);
            wind.y = Random.Range(windMin.y, windMax.y);
            wind.z = Random.Range(windMin.z, windMax.z);

            velocity = wind;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        rigidbody = collider.GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            rigidbody = collider.GetComponentInParent<Rigidbody>();
            if (rigidbody == null)
            {
                return;
            }
        }

        // 相対速度
        var relativeVelocity = velocity - rigidbody.velocity;

        // 空気抵抗を与える
        rigidbody.AddForce(intensity * velocity);
    }
}