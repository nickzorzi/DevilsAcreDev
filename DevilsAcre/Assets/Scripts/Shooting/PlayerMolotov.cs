using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMolotov : MonoBehaviour
{
    public float speed;
    public static int damage = 1;

    public Rigidbody2D rb;
    public GameObject impactEffect;

    public GameObject flames;

    [SerializeField] private AudioClip shatterEffect;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MolotovLand());
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
            Instantiate(flames, transform.position, Quaternion.identity);

            SoundManager.Instance.PlaySound(shatterEffect);

            Destroy(gameObject);
        } 
        else if (hitInfo.GetComponent<CircleCollider2D>() != null)
        {
            Instantiate(flames, transform.position, Quaternion.identity);

            SoundManager.Instance.PlaySound(shatterEffect);

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    IEnumerator MolotovLand()
    {
        yield return new WaitForSeconds(2);

        Instantiate(flames, transform.position, Quaternion.identity);

        SoundManager.Instance.PlaySound(shatterEffect);

        Destroy(this.gameObject);
    }
}
