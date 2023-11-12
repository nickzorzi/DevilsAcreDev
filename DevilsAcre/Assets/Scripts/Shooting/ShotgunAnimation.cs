using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAnimation : MonoBehaviour
{
    private ShotgunBandit shotgunBandit;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        shotgunBandit = GetComponentInParent<ShotgunBandit>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (shotgunBandit.isShooting)
        // {
        //     animator.SetBool("isShooting", true);
        // }
        // else
        // {
        //     animator.SetBool("isShooting", false);
        // }

        if (shotgunBandit != null)
        {
            animator.SetBool("isShooting", shotgunBandit.isShooting);
        }
        else
        {
            Debug.LogError("ShotgunBandit reference is null. Make sure it is assigned.");
        }
    }
}
