using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteImpact : MonoBehaviour
{

    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource hitSound;

    // Start is called before the first frame update
    void Start()
    {
        if (deathSound != null && hitSound != null)
        {
            deathSound.Play();
            hitSound.Play();
        }
    }

    public void destroyEffect ()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
