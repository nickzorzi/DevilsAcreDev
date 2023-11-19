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

    public Animator animator;

    public bool isShooting = false;

    [SerializeField] private int multiShotArc = 20;
    [SerializeField] private int bulletsBeforeCooldown = 5;
    [SerializeField] private Transform bulletSpawnPoint;

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

        isShooting = false;

        animator.SetBool("isWalking", false);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", false);

        isShooting = false;

        //AIM WEAPON
        Vector3 displacement = shotgun.position - player.position;
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        shotgun.rotation = Quaternion.Euler(0f, 0f, angle + shotgunOffset);

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
            
            // Instantiate(shotgunBullet,shotgunShotPoint.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;

            isShooting = true;

            StartCoroutine(ShotgunFire());
        }
    }

    IEnumerator ShotgunFire()
    {

        yield return new WaitForSeconds(.4f);

        int centerArc = multiShotArc / 2;
            for (int i = 0; i < bulletsBeforeCooldown; i++)
            {

                float bulletOffset = i * (multiShotArc / bulletsBeforeCooldown) - centerArc;

                Quaternion newRot = Quaternion.Euler(shotgunShotPoint.eulerAngles.x, shotgunShotPoint.eulerAngles.y, shotgunShotPoint.eulerAngles.z + bulletOffset);

                Instantiate(shotgunBullet, shotgunShotPoint.transform.position, newRot);
            }

            SoundManager.Instance.PlaySound(enemyShootSoundEffect);

            nextFireTime = Time.time + fireRate;
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

            CinemachineShake.Instance.ShakeCamera(3f, .1f); //Camera Shake

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

            Destroy(gameObject);

            //SCORE VALUE SYSTEM
            Score.scoreValue += scoreValueOnDeath;
        }
    }
}
