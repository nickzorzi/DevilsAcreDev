using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovBandit : MonoBehaviour
{
    private Transform player;

    public Transform molotov;
    public float molotovOffset;

    public Transform molotovShotPoint;

    private SpriteRenderer render;
    public SpriteRenderer sprite;

    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject molotovBottle;
    public int health;
    public GameObject deathEffect;

    public Animator animator;

    // [SerializeField] private int multiShotArc = 20;
    // [SerializeField] private int bulletsBeforeCooldown = 5;
    // [SerializeField] private Transform bulletSpawnPoint;

    public int scoreValueOnDeath;

    [SerializeField] private HitFlash hitFlash;

    [SerializeField] private AudioClip enemyHitSoundEffect;
    [SerializeField] private AudioClip enemyShootSoundEffect;
    [SerializeField] private AudioClip enemyDeathSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        render = GetComponent<SpriteRenderer>();

        animator.SetBool("isWalking", false);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", false);

        //MOVE TOWARDS PLAYER (AND FIRE)
        float distanceFromPlayer = Vector2.Distance(player.position,transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer>shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);

            animator.SetBool("isWalking", true);
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime <Time.time)
        {
            animator.SetBool("isWalking", false);

            Instantiate(molotovBottle, transform.position, Quaternion.identity);
            
            nextFireTime = Time.time + fireRate;

        }
    }

    void FixedUpdate()
    {
        render.flipX = sprite.flipY;
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            Debug.Log("Collision with Projectile detected!");
            Debug.Log("Collided with: " + other.gameObject.name);

            //TakeDamage(other.GetComponent<Projectile>().damage);
            TakeDamage(Projectile.damage);

            hitFlash.Flash();

            SoundManager.Instance.PlaySound(enemyHitSoundEffect);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,lineOfSite);
        Gizmos.DrawWireSphere(transform.position,shootingRange);
    }

    void TakeDamage(int damageAmount)
    {
        health -= damageAmount; 

        if (health <= 0)
        {
            SoundManager.Instance.PlaySound(enemyDeathSoundEffect);

            Instantiate(deathEffect, transform.position, Quaternion.identity);

            CinemachineShake.Instance.ShakeCamera(3f, .1f); //Camera Shake

            Destroy(gameObject);

            //SCORE VALUE SYSTEM
            Score.scoreValue += scoreValueOnDeath;
        }
    }
}
