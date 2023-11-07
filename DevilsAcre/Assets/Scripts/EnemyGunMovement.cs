using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunMovement : MonoBehaviour
{

    private Transform player;

    [SerializeField] SpriteRenderer enemySprite;

    [SerializeField] private bool enemyFlipped;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 mousePosition = Input.mousePosition;
        // mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 playerPosition = player.position;
        Vector2 direction = playerPosition - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the gun to point at the player
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Flip the sprite based on the angle
        if (angle > 90 || angle < -90)
        {
            enemySprite.flipY = true;
        }
        else
        {
            enemySprite.flipY = false;
        }
    }
}
