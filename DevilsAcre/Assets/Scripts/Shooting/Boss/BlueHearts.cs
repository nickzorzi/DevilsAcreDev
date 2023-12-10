using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlueHearts : MonoBehaviour
{
    public float speed = 5;
    Transform player;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lookAtPlayer();
        Destroy(this.gameObject,6);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if the collided object has a CapsuleCollider2D component
        if (hitInfo.GetComponent<CapsuleCollider2D>() != null)
        {
            if (hitInfo.tag == "MolotovSpread")
            {

            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {


        lookAtPlayer();

        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void lookAtPlayer()
    {
        Vector3 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle+90, Vector3.forward);
    }
}
