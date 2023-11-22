using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public static int damage = 1;

    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Bottle"))
        {
            
        }
        else if (hitInfo.CompareTag("MolotovE"))
        {

        }
        else if (hitInfo.CompareTag("MolotovSpread"))
        {
            
        }
        else if (hitInfo.GetComponent<BoxCollider2D>() != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        } 
        else if (hitInfo.GetComponent<CircleCollider2D>() != null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
