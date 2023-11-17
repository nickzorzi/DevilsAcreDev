using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinchesterAnimation : MonoBehaviour
{
    private GunslingerBandit gunslingerBandit;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        gunslingerBandit = GetComponentInParent<GunslingerBandit>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (gunslingerBandit.isShooting)
        // {
        //     animator.SetBool("isShooting", true);
        // }
        // else
        // {
        //     animator.SetBool("isShooting", false);
        // }

        if (gunslingerBandit != null)
        {
            animator.SetBool("isShooting", gunslingerBandit.isShooting);
        }
        else
        {
            Debug.LogError("GunslingerBandit reference is null. Make sure it is assigned.");
        }
    }
}
