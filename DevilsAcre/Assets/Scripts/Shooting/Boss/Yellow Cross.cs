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
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private bool redVersion = false;

    [Space(10)]
    [SerializeField] private AudioClip Startclip;

    private Transform player;
    private CircleCollider2D col;
    private bool drop = false;
    private bool isFading;
    private void Start()
    {
        if(redVersion)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
        
        SoundManager.Instance.PlaySound(Startclip);

        Invoke("coolDownDone", dropTime);
        
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
                Destroy(gameObject,3f);
                StartCoroutine(fade());
                col.enabled = true;
            }
        }
        else // 
        {
            childPos.transform.Translate((Vector3.up / 2) * Time.deltaTime);
            if(redVersion)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }
        
    
    }

    IEnumerator fade()
    {
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(.5f);
            Color temp = gameObject.GetComponent<SpriteRenderer>().color;
            temp.a -= .1f;
            gameObject.GetComponent<SpriteRenderer>().color = temp;
            temp = childPos.GetComponent<SpriteRenderer>().color;
            temp.a -= .1f;
            childPos.GetComponent<SpriteRenderer>().color = temp;
            
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
