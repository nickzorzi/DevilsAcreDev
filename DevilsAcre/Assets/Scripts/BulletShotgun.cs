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
        shotgun = GameObject.FindGameObjectWithTag("ShotgunShotPoint").transform;
        bulletRB = GetComponent<Rigidbody2D>();
        Vector2 moveDir = shotgun.right * speed;
        bulletRB.velocity = moveDir;
        Destroy(this.gameObject, 6);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.GetComponent<CapsuleCollider2D>() != null)
        {
            Destroy(gameObject);
        }
    }
}
