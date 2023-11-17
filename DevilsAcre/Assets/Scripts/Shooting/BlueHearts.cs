using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueHearts : MonoBehaviour
{
    public float speed = 5;
    Transform player;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(this.gameObject,6);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if the collided object has a CapsuleCollider2D component
        if (hitInfo.GetComponent<CapsuleCollider2D>() != null)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
