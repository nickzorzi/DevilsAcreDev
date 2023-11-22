using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMolotov : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;

    public GameObject flames;

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        StartCoroutine(MolotovLand());
    }

    IEnumerator MolotovLand()
    {
        yield return new WaitForSeconds(2);

        Instantiate(flames, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if the collided object has a CapsuleCollider2D component
        if (hitInfo.GetComponent<CapsuleCollider2D>() != null)
        {
            Instantiate(flames, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
