using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBandit : MonoBehaviour
{
    private Transform player;

    public Transform shotgun;
    public float shotgunOffset;

    public Transform shotgunShotPoint;

    private SpriteRenderer render;
    public SpriteRenderer sprite;

    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject shotgunBullet;
    public int health;
    public GameObject deathEffect;

    [SerializeField] private int multiShotArc = 10;
    [SerializeField] private int bulletsBeforeCooldown = 5;
    [SerializeField] private Transform bulletSpawnPoint;

    public int scoreValueOnDeath;

    [SerializeField] private AudioSource enemyHitSoundEffect;
    [SerializeField] private AudioSource enemyShootSoundEffect;
    [SerializeField] private AudioSource enemyDeathSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //AIM WEAPON
        Vector3 displacement = shotgun.position - player.position;
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        shotgun.rotation = Quaternion.Euler(0f, 0f, angle + shotgunOffset);

        //MOVE TOWARDS PLAYER (AND FIRE)
        float distanceFromPlayer = Vector2.Distance(player.position,transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer>shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime <Time.time)
        {
            // Instantiate(shotgunBullet,shotgunShotPoint.transform.position, Quaternion.identity);
            // nextFireTime = Time.time + fireRate;

            int centerArc = multiShotArc / 2;
            for (int i = 0; i < bulletsBeforeCooldown; i++)
            {

                float bulletOffset = i * (multiShotArc / bulletsBeforeCooldown) - centerArc;

                Quaternion newRot = Quaternion.Euler(shotgunShotPoint.eulerAngles.x, shotgunShotPoint.eulerAngles.y, shotgunShotPoint.eulerAngles.z + bulletOffset);

                GameObject bullet = Instantiate(shotgunBullet, shotgunShotPoint.position, newRot);
            }

            enemyShootSoundEffect.Play();
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

            enemyHitSoundEffect.Play();
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
            enemyDeathSoundEffect.Play();

            Instantiate(deathEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);

            //SCORE VALUE SYSTEM
            Score.scoreValue += scoreValueOnDeath;
        }
    }
}
