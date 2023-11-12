using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShotgun : MonoBehaviour
{
    public float speed;
    private Rigidbody2D bulletRB;
    private Transform shotgun;

    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    private void FixedUpdate()
    {
        transform.Translate(speed / 50, 0, 0);
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.GetComponent<CapsuleCollider2D>() != null)
        {
            Destroy(gameObject);
        }
    }
}
