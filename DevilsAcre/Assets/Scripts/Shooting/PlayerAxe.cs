using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxe : MonoBehaviour
{
    public float speed;
    public static int damage = 1;

    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,6);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
