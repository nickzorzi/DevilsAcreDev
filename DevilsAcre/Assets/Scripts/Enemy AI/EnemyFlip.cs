using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlip : MonoBehaviour
{
    private Transform player;

    [SerializeField] SpriteRenderer enemySprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.position;

        // Flip the sprite based on player position
        if (playerPosition.x < transform.position.x)
        {
            // Player is to the left of the enemy
            enemySprite.flipX = true;
        }
        else
        {
            // Player is to the right of the enemy
            enemySprite.flipX = false;
        }
    }
}
