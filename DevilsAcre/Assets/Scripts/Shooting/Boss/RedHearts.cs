using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedHearts : MonoBehaviour
{
    [HideInInspector]
    public bool isMoving = false;

    private Vector2 moveDirection = Vector2.down;
    private float moveSpeed = 1;
    private float currentSpeed;
    private float maxSpeed = 10;

    private float accelerationTime = 2;
    private float time;

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
        isMoving = false;
        currentSpeed = 0f;
        time = 0;
    }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            Invoke("Destroy", 3f);
            currentSpeed = Mathf.SmoothStep(moveSpeed, maxSpeed, time / accelerationTime);
            transform.Translate(moveDirection * currentSpeed * Time.deltaTime);
            time += Time.deltaTime;
        }
    }
 


    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.GetComponent<CapsuleCollider2D>() != null)
        {
            if (hitInfo.tag == "MolotovSpread")
            {

            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log(hitInfo.name);
        }
    }
}
