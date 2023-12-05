using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class YellowCross : MonoBehaviour
{

    [SerializeField] private Sprite groundedCross;
    [SerializeField] private GameObject childPos;
    [SerializeField] private float speed = 10f;
    public float dropTime = 2f;

    private CircleCollider2D col;
    private bool drop = false;
    private void Start()
    {
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
        Invoke("coolDownDone", dropTime);
        Destroy(this.gameObject, dropTime + 3);
    }


    private void FixedUpdate()
    {
        if (drop)
        {
            float distance = Vector3.Distance(childPos.transform.position, transform.position);
            if (distance > .2f)
            {
                childPos.transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            else
            {
                childPos.GetComponent<Animator>().enabled = false;
                childPos.GetComponent<SpriteRenderer>().sprite = groundedCross;
                col.enabled = true;
            }
        }
        else // 
        {
            childPos.transform.Translate((Vector3.up / 2) * Time.deltaTime);
        }
        
    
    }

    private void coolDownDone()
    {
        drop = true;
    }
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

}
