using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisterBoss : MonoBehaviour
{
    public float lineOfSight;
    private Transform player;
    public int health;
    public GameObject deathEffect;

    private bool hasEnteredLineOfSight = false;
    public bool canFireRed = false;
    public bool canFireBlue = false;
    public bool canFireYellow = false;

    public int scoreValueOnDeath;

    public Animator animator;

    public GameObject waveSB;
    public GameObject waveGB;

    [SerializeField] private HitFlash hitFlash;

    [SerializeField] private AudioClip enemyHitSoundEffect;
    [SerializeField] private AudioClip enemyDeathSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        canFireBlue = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight)
        {
            if (!hasEnteredLineOfSight)
            {
                animator.SetTrigger("Entry");
                StartCoroutine(FinishedAnimation());
                hasEnteredLineOfSight = true;
            }
        }

        //Waves Enabler
        if (hasEnteredLineOfSight == true)
        {
            waveSB.SetActive(true);
            waveGB.SetActive(true);
        }
    }

    private IEnumerator FinishedAnimation()
    {
        animator.SetTrigger("FinishedTransformation");
        yield return new WaitForSeconds(2);
        canFireRed = true;
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile" && canFireRed == true)
        {
            Debug.Log("Collision with Projectile detected!");
            Debug.Log("Collided with: " + other.gameObject.name);

            //TakeDamage(other.GetComponent<Projectile>().damage);
            TakeDamage(Projectile.damage);

            CinemachineShake.Instance.ShakeCamera(3f, .1f); //Camera Shake

            hitFlash.Flash();

            canFireBlue = true;

            SoundManager.Instance.PlaySound(enemyHitSoundEffect);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lineOfSight);
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

        if (health <= 35)
        {
            canFireRed = false;
            canFireYellow = true;
        }
    }
}
