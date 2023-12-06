using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMolotov : MonoBehaviour
{
    public float speed;
    public static int damage = 1;

    [Header("Add Objects")]
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject flames;
    [Header("Audio")]
    [SerializeField] private AudioClip shatterEffect;


    private Vector2 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        var screenPoint = (Vector2)Input.mousePosition;
        targetPos = Camera.main.ScreenToWorldPoint(screenPoint);
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
        
        float tarDistance = Vector2.Distance(transform.position, targetPos);
        if (tarDistance == 0) 
        {
            MolotovLand();
        }
    }

    private void MolotovLand()
    {
       

        Instantiate(flames, transform.position, Quaternion.identity);

        SoundManager.Instance.PlaySound(shatterEffect);

        Destroy(this.gameObject);
    }
}
