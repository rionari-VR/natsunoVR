using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyaji_anim : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
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
        }
    }
}
