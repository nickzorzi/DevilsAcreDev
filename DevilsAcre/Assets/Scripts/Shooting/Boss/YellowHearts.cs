using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHearts : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.down;
    private float moveSpeed = 4f;

    private void OnEnable()
    {
        Invoke("Destroy", 2f);
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
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
            gameObject.SetActive(false);
        }
    }
}
