using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class oyaji_anim : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    AudioClip hit1, hit2;

    [SerializeField]
    AudioClip start, end;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();

        SEManager.Instance.Play(start, 1, 0, 0.85f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (animator.GetBool("oyaji") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            animator.SetBool("oyaji", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            animator.SetBool("oyaji", true);

            if (Random.Range(0, 1) == 0)
            {
                SEManager.Instance.Play(hit1, 1, 0, 0.85f);
            }
            else
            {
                SEManager.Instance.Play(hit2, 1, 0, 0.85f);
            }
        }
    }
}
