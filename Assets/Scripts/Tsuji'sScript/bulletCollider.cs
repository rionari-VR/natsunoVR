using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollider : MonoBehaviour
{
    [SerializeField] private AudioClip bulletSE;
    private AudioSource audioSource;
    private bool isHit;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isHit = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        audioSource.PlayOneShot(bulletSE);
        isHit = true;
    }

    public bool GetHitFlag()
    {
        return isHit;
    }

    public void SetHitFlag(bool flg)
    {
        isHit = flg;
    }
}
