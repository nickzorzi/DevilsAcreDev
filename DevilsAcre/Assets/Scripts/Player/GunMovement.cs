using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{

    [SerializeField] SpriteRenderer sprite;

    [SerializeField] private bool flipped;

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.right = direction;


       

        if (transform.rotation.eulerAngles.z < 90 || transform.rotation.eulerAngles.z > 270)
        {
            sprite.flipY = false;
        }
        else
        {
            sprite.flipY = true;
        }

    }
}